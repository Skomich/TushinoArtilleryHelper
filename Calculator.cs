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
    public partial class Calculator : Form
    {

        private string GunName = string.Empty;
        private int WeatherLevel = 0;

        public Calculator(string GunName)
        {
            this.GunName = GunName;
            InitializeComponent();
            this.GenTxt.Text += GunName;
            // Заполняем ComboBox именами снарядов
            foreach (var projectile in GunList.GetGun(GunName).projectiles)
                this.recom_proj.Items.Add(projectile.Name);
            this.Weather.SelectedIndex = WeatherLevel;
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

            // Можно писать "," и "."
            if (CheckDigidTextBoxInput(number) && number != 44 && number != 46)
            {
                e.Handled = true;
            }
        }

        private void CalculateEvent(object sender, EventArgs e)
        {
            // Просто вызываем т.к. все расчеты проходят в той функции
            Calculate();
        }
        

        public void Calculate()
        {
            // Не думаю что надо делать много проверок сложных.
            // Если не умеет стрелять с арты - прога не поможет.
        }

        private void recom_proj_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ItemName = ((string)((System.Windows.Forms.ComboBox)sender).SelectedItem);
            foreach(var projectile in GunList.GetGun(GunName).projectiles)
            {
                if(ItemName == projectile.Name)
                {
                    minRange.Text = projectile.MinRange.ToString();
                    maxRange.Text = projectile.MaxRange.ToString();
                    if(projectile.IsArc())
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
            }
        }

        // Выбор уровня погодных условий
        private void Weather_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeatherLevel = ((ComboBox)sender).SelectedIndex;
        }
    }
}
