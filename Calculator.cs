using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtilleryHelper
{

    enum CALC_ERROR
    {
        BAD_INPUT_TARGET,
        BAD_INPUT_SOURCE,
        BAD_INPUT_SHIFT,
        BAD_INPUT_PROJECTILE,
        BAD_INPUT_WIND,
        UNKNOWN
    }

    public partial class Calculator : Form
    {

        private string GunName = string.Empty;
        private int WeatherIndex = 0;
        private int ProjectileIndex = 0;

        public Calculator(string GunName)
        {
            this.GunName = GunName;
            InitializeComponent();
            this.GenTxt.Text += GunName;
            if (GunList.GetGun(GunName).isScaleNATO)
                resultHorizontal.Text = "3200";
            // Заполняем ComboBox именами снарядов
            foreach (var projectile in GunList.GetGun(GunName).projectiles)
                this.recom_proj.Items.Add(projectile.Name);
            this.Weather.SelectedIndex = WeatherIndex;
            this.minGunRange.Text = GunList.GetGun(GunName).MinRange.ToString();
            this.maxGunRange.Text = GunList.GetGun(GunName).MaxRange.ToString();

        }

        private bool CheckDigidTextBoxInput(char ch)
        {
            if (!Char.IsDigit(ch) && ch != 1 && ch != 3 && ch != 8 && ch != 22)
                return true;
            return false;
        }

        private void DigitOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (CheckDigidTextBoxInput(number))
            {
                e.Handled = true;
            }
        }
        private void DigitSignOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            // Можно писать "-"
            if (CheckDigidTextBoxInput(number) && number != 45)
            {
                e.Handled = true;
            }
        }

        private void DecimalOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            // Можно писать ","
            if (CheckDigidTextBoxInput(number) && number != 44)
            {
                e.Handled = true;
            }
        }

        private void DecimalSignOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            // Можно писать "," и "-"
            if (CheckDigidTextBoxInput(number) && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void CalculateEvent(object sender, EventArgs e)
        {
            // Просто вызываем т.к. все расчеты проходят в той функции
            Calculate();
        }

        private void recom_proj_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectileIndex = ((ComboBox)sender).SelectedIndex;
            ProjectileBase projectile = GunList.GetGun(GunName).projectiles[ProjectileIndex];

            if (projectile == null)
                return;

            minRange.Text = projectile.MinRange.ToString();
            maxRange.Text = projectile.MaxRange.ToString();
            // Максимально 2 символа прсле ","
            if (projectile.TimeAverage != 0)
                avgTime.Text = projectile.TimeAverage.ToString().Substring(0, 5);
            else
                avgTime.Text = "0";

            if (projectile.IsArc())
            {
                canArcShoot.Text = "Есть";
                minArcRange.Text = projectile.MinRangeArc.ToString();
                maxArcRange.Text = projectile.MaxRangeArc.ToString();
            }
            else
            {
                canArcShoot.Text = "Нет";
                minArcRange.Text = "0";
                maxArcRange.Text = "0";
            }
        }

        // Выбор уровня погодных условий
        private void Weather_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeatherIndex = ((ComboBox)sender).SelectedIndex;
        }

        private int TransformToNATO(int input)
        {
            return (input * 6400 / 6000);
        }

        private int TransformToUSSR(int input)
        {
            return (input * 6000 / 6400);
        }

        public void Calculate()
        {
            // Не думаю что надо делать много проверок сложных.
            // Если не умеет стрелять с арты - прога не поможет.
            CALC_ERROR ErrorCode = CALC_ERROR.UNKNOWN;
            string ErrorDescription = "";

            // Дальность пусть указывают всегда иначе прога не имеет смысла
            if (range.Text == "")
            {
                ErrorCode = CALC_ERROR.BAD_INPUT_TARGET;
                ErrorDescription = "Дальность до цели заполнять обязательно!";
                goto Error;
            }

            // Используемая система координат
            bool isNATO = GunList.GetGun(GunName).isScaleNATO;

            // Азимут до цели
            int TargetAzimuth = 0;

            // Наши значения в прицеле
            int AimAzimuth = 0;
            int ScopeValue = 0;

            // double переменные для поправок.
            // Сначала в них забиваем поправки, а потом уже суммируем с ними.
            double TempAzimuth = 0;
            double TempScope = 0;

            // Время прилета (+- 0.2 сек.)
            double time = 0;

            int Distance = 0;

            Distance = Int32.Parse(range.Text);

            // Для начала проверим заполнение АЦ
            if (azimuthTargetUSSR.Text != "")
            {
                if (isNATO)
                    TargetAzimuth = TransformToUSSR(Int32.Parse(azimuthTargetUSSR.Text));
                else
                    TargetAzimuth = Int32.Parse(azimuthTargetUSSR.Text);

                if (TargetAzimuth > 6000 || TargetAzimuth < 0)
                {
                    ErrorCode = CALC_ERROR.BAD_INPUT_TARGET;
                    ErrorDescription = "Значения в советской системе не могут превышать 6000!";
                    goto Error;
                }
            }
            else if (azimuthTargetNATO.Text != "")
            {
                if (!isNATO)
                    TargetAzimuth = TransformToNATO(Int32.Parse(azimuthAimingNATO.Text));
                else
                    TargetAzimuth = Int32.Parse(azimuthAimingNATO.Text);

                if (TargetAzimuth > 6400)
                {
                    ErrorCode = CALC_ERROR.BAD_INPUT_TARGET;
                    ErrorDescription = "Значения в системе NATO не могут превышать 6400!";
                    goto Error;
                }
            }
            else
            {
                // Думаю что не стоит выдавать ошибку при отсутствии горизонтальной наводки
                // Человек может просто самостоятельно все сделать
                /*
                ErrorCode = CALC_ERROR.BAD_INPUT_TARGET;
                ErrorDescription = "Пустые поля АЦ!\n" +
                    "Требуется заполнять хотя-бы в одной системе координат.";
                goto Error;
                */

                TargetAzimuth = 0;
            }

            if (azimuthBussolUSSR.Text != "")
            {
                if (!isNATO)
                    AimAzimuth = Int32.Parse(azimuthBussolUSSR.Text);
                else
                    AimAzimuth = TransformToUSSR(Int32.Parse(azimuthBussolUSSR.Text));
            }
            else if (azimuthBussolNATO.Text != "")
            {
                if (isNATO)
                    AimAzimuth = Int32.Parse(azimuthBussolNATO.Text);
                else
                    AimAzimuth = TransformToNATO(Int32.Parse(azimuthBussolNATO.Text));
            }
            else if (azimuthAimingUSSR.Text != "")
            {
                if (!isNATO)
                    AimAzimuth = Int32.Parse(azimuthAimingUSSR.Text);
                else
                    AimAzimuth = TransformToUSSR(Int32.Parse(azimuthAimingUSSR.Text));

                AimAzimuth = (TargetAzimuth - AimAzimuth + 3000) % 6000;
            }
            else if (azimuthAimingNATO.Text != "")
            {
                if (isNATO)
                    AimAzimuth = Int32.Parse(azimuthAimingNATO.Text);
                else
                    AimAzimuth = TransformToNATO(Int32.Parse(azimuthAimingNATO.Text));

                AimAzimuth = (TargetAzimuth - AimAzimuth + 3200) % 6400;
            }
            else
            {
                // Тут тоже не даем ошибку
                /*
                ErrorCode = CALC_ERROR.BAD_INPUT_SOURCE;
                ErrorDescription = "Не заполнены поля АТН и УБ.\n" +
                    "Нужно заполнить 1 тип в одной шкале";
                goto Error;
                */
                AimAzimuth = 0;
            }

            // Далее считается что мы уже знаем горизонтальный азимут,
            // который надо выставить в прицеле без учета поправок.

            // Значения горизонтального азимута могут быть равны 0,
            // в таком случае мы должны просто считать дальше поправки.
            // Будем думать что чел сам все выставил и дадим ему
            // поправки в виде какого то числа, которое надо
            // суммировать с его уже вычисленным прицелом.
            // Например в случае использования буссоли 2-мя игроками.


            ProjectileBase projectile = null;
            TableItem item;
            // Считаем вертикальный прицел
            if(recom_proj.Text != "")
            {
                projectile = GunList.GetGun(GunName).projectiles[ProjectileIndex];
                if(projectile == null)
                {
                    ErrorCode = CALC_ERROR.UNKNOWN;
                    ErrorDescription = "Снаряда не существует. Неизвестная ошибка.";
                    goto Error;
                }
                // Далее projectile - наш снаряд

                // Табличное значение расстояния
                int TableRange = 0;

                // Ищем ближайшее значение расстояния в таблице
                if (isArcShoot.Checked)
                    item = projectile.GetArcCorrectionTableItem(Distance, WeatherIndex, out TableRange);
                else
                    item = projectile.GetCorrectionTableItem(Distance, WeatherIndex, out TableRange);
                // Далее item - наша строка с поправками
                
                if(item.Range == -1)
                {
                    ErrorCode = CALC_ERROR.BAD_INPUT_TARGET;
                    ErrorDescription = "Расстоние до цели не входит в промежуток " +
                        "возможной дистанции стрельбы данным снарядом.\n" +
                        "Мин. дист.: " + projectile.GetMinRange() +
                        ".\nМакс. дист.: " + projectile.GetMaxRange() + ".";
                    goto Error;
                }

                ScopeValue = (int)item.ScopeValue[WeatherIndex];
                // Далее ScopeValue будет нашим значением вертикального прицела до поправок

                // Поправки на десятки метров (за каждые 50м.)
                ScopeValue += ((int)item.RangeOffset) * ((Distance - TableRange) / 50);

                // Поправки на единицы метров
                ScopeValue += ((Distance - TableRange) % 50) / ((int) item.RangePerUnit);

                // Добавляем деривацию
                if (projectile.IsExistDerivation())
                    TempAzimuth += item.Derivation;
            }
            else
            {
                ErrorCode = CALC_ERROR.BAD_INPUT_PROJECTILE;
                ErrorDescription = "Поле снаряда пустое.\n" +
                    "Выберите тип снаряда или запросите рекомендацию.";
                goto Error;
            }

            // Проверяем наши значения в каждом поле
            // и если они есть - считаем

            // Начинаем со смещения наводки
            if (horizontalShift.Text != "")
            {
                double Shift = Double.Parse(horizontalShift.Text);
                bool isLeft = false;
                
                if (Shift < 0)
                    isLeft = true;

                // По формуле косинусов вычисляем угол прицела до новой цели:
                // A - сторона сдвига (Shift).
                // B и C - стороны смежные с вычисляемым углом, в нашем случае
                // расстояние до цели (Distance).
                // angle = arccos((B^2 + C^2 - A^2) / 2BC)
                // Отсюда angle - угол в радианах.
                double result = Math.Acos((Distance * Distance * 2 - Shift * Shift) / (2 * Distance * Distance));
                result *= (180 / Math.PI);
                //Math.Acos((Distance * Distance * 2 - Shift * Shift) / (2 * Distance * Distance));
                //result = Math.Acos((Distance * Distance * 2 - Shift * Shift) /
                //    (4 * Distance));

                // Далее надо умножить на значение единицы той СИ,
                // которая используется для нашего оружия.

                // Для NATO: 360 / 6400 = 0.05625 (единиц за градус угла)
                if (isNATO)
                    result /= 0.05625;
                // Для советов: 360 / 6000 = 0.06
                else
                    result /= 0.06;

                if (isLeft)
                    result *= -1;

                TempAzimuth += result;

                // Не будем менять АТН, АЦ и УБ пока-что.
                // Для этого надо как минимум кнопку на замену.
            }

            // Считаем поправку на перепад высот
            if(altitudeOur.Text != "" && altitudeTarget.Text != "")
            {
                TempScope += (Double.Parse(altitudeTarget.Text) - Double.Parse(altitudeOur.Text)) /
                    100 * item.HeightOffset;
            }

            if (wind.Text != "")
            {
                double dWind = Double.Parse(wind.Text);
                double dDirectionV = 0, dDirectionH = 0;
                if (windDirectionHorizontal.Text != "")
                {
                    dDirectionH = Double.Parse(windDirectionHorizontal.Text);
                }
                if (windDirectionVertical.Text != "")
                {
                    dDirectionV = Double.Parse(windDirectionVertical.Text);
                }
                
                // Ветер >= 0, -1 >= направление <= 1
                if(dWind < 0 || Math.Abs(dDirectionH) > 1 || Math.Abs(dDirectionV) > 1)
                {
                    ErrorCode = CALC_ERROR.BAD_INPUT_WIND;
                    ErrorDescription = "Скорость ветра и(или) Направление ветра введены неправильно.\n" +
                        "Посмотрите справку в меню, по использованию калькулятора.";
                    goto Error;
                }

                // Считаем горизонтальную поправку на ветер
                TempAzimuth += item.SideWind * dWind * dDirectionH * -1;
                TempScope += item.LongitudinalWind * dWind * dDirectionV;

                // Считаем вертикальную поправку на ветер
                // Надо сделать отдельный TextBox для направления относительно вертикали
                //TempScope += item.LongitudinalWind * dWind * dDirection;
            }

            // Cчитаем поправку на давление
            if (pressureAir.Text != "")
            {
            }

            // Считаем поправку на температуру
            if (temperature.Text != "")
            {
            }

            // Добавляем поправки
            // Для горизонтальной наводки надо учесть макс значения
            AimAzimuth += (int)Math.Round(TempAzimuth);
            AimAzimuth = (AimAzimuth + (isNATO ? 6400 : 6000)) %
                (isNATO ? 6400 : 6000);
            ScopeValue += (int)Math.Round(TempScope);

            // Заполняем интерфейс
            resultHorizontal.Text = AimAzimuth.ToString();
            resultVertical.Text = ScopeValue.ToString();

            // Дальше не идем т.к. там ошибка
            return;
            Error:
                MessageBox.Show("" + ErrorDescription, "Ошибка расчета #" + ErrorCode,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
