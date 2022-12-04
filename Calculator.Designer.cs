
namespace ArtilleryHelper
{
    partial class Calculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.azimuthAimingTxt = new System.Windows.Forms.Label();
            this.azimuthAimingNATO = new System.Windows.Forms.TextBox();
            this.azimuthTargetTxt = new System.Windows.Forms.Label();
            this.azimuthTargetNATO = new System.Windows.Forms.TextBox();
            this.natoTxt = new System.Windows.Forms.Label();
            this.GenTxt = new System.Windows.Forms.Label();
            this.azimuthAimingUSSR = new System.Windows.Forms.TextBox();
            this.azimuthTargetUSSR = new System.Windows.Forms.TextBox();
            this.ussrTxt = new System.Windows.Forms.Label();
            this.windTxt = new System.Windows.Forms.Label();
            this.wind = new System.Windows.Forms.TextBox();
            this.altitudeTxt = new System.Windows.Forms.Label();
            this.altitudeOurTxt = new System.Windows.Forms.Label();
            this.altitudeTargetTxt = new System.Windows.Forms.Label();
            this.altitudeOur = new System.Windows.Forms.TextBox();
            this.altitudeTarget = new System.Windows.Forms.TextBox();
            this.weatherTxt = new System.Windows.Forms.Label();
            this.pressureTxt = new System.Windows.Forms.Label();
            this.pressureAir = new System.Windows.Forms.TextBox();
            this.temperatureTxt = new System.Windows.Forms.Label();
            this.temperature = new System.Windows.Forms.TextBox();
            this.windDirectionTxt = new System.Windows.Forms.Label();
            this.windDirection = new System.Windows.Forms.TextBox();
            this.delimiter = new System.Windows.Forms.Label();
            this.rangeTxt = new System.Windows.Forms.Label();
            this.range = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.loadTxt = new System.Windows.Forms.Label();
            this.loadHelpTxt = new System.Windows.Forms.Label();
            this.isArcShoot = new System.Windows.Forms.CheckBox();
            this.resultHorizontalTxt = new System.Windows.Forms.Label();
            this.resultVerticalTxt = new System.Windows.Forms.Label();
            this.resultHorizontal = new System.Windows.Forms.Label();
            this.resultVertical = new System.Windows.Forms.Label();
            this.characteristicTxt = new System.Windows.Forms.Label();
            this.maxRangeTxtTxt = new System.Windows.Forms.Label();
            this.maxRangeTxt = new System.Windows.Forms.Label();
            this.minRange = new System.Windows.Forms.Label();
            this.maxRange = new System.Windows.Forms.Label();
            this.resultTimeTxt = new System.Windows.Forms.Label();
            this.resultTime = new System.Windows.Forms.Label();
            this.azimuthBussolTxt = new System.Windows.Forms.Label();
            this.azimuthBussolNATO = new System.Windows.Forms.TextBox();
            this.azimuthBussolUSSR = new System.Windows.Forms.TextBox();
            this.canArcShootTxt = new System.Windows.Forms.Label();
            this.canArcShoot = new System.Windows.Forms.Label();
            this.avgTimeTxt = new System.Windows.Forms.Label();
            this.avgTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // azimuthAimingTxt
            // 
            this.azimuthAimingTxt.AutoSize = true;
            this.azimuthAimingTxt.Location = new System.Drawing.Point(11, 60);
            this.azimuthAimingTxt.Name = "azimuthAimingTxt";
            this.azimuthAimingTxt.Size = new System.Drawing.Size(29, 13);
            this.azimuthAimingTxt.TabIndex = 0;
            this.azimuthAimingTxt.Text = "АТН";
            // 
            // azimuthAimingNATO
            // 
            this.azimuthAimingNATO.Location = new System.Drawing.Point(52, 57);
            this.azimuthAimingNATO.MaxLength = 4;
            this.azimuthAimingNATO.Name = "azimuthAimingNATO";
            this.azimuthAimingNATO.Size = new System.Drawing.Size(100, 20);
            this.azimuthAimingNATO.TabIndex = 1;
            this.azimuthAimingNATO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // azimuthTargetTxt
            // 
            this.azimuthTargetTxt.AutoSize = true;
            this.azimuthTargetTxt.Location = new System.Drawing.Point(11, 86);
            this.azimuthTargetTxt.Name = "azimuthTargetTxt";
            this.azimuthTargetTxt.Size = new System.Drawing.Size(22, 13);
            this.azimuthTargetTxt.TabIndex = 2;
            this.azimuthTargetTxt.Text = "АЦ";
            // 
            // azimuthTargetNATO
            // 
            this.azimuthTargetNATO.Location = new System.Drawing.Point(52, 83);
            this.azimuthTargetNATO.MaxLength = 4;
            this.azimuthTargetNATO.Name = "azimuthTargetNATO";
            this.azimuthTargetNATO.Size = new System.Drawing.Size(100, 20);
            this.azimuthTargetNATO.TabIndex = 3;
            this.azimuthTargetNATO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // GenTxt
            // 
            this.GenTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenTxt.Location = new System.Drawing.Point(13, 13);
            this.GenTxt.Name = "GenTxt";
            this.GenTxt.Size = new System.Drawing.Size(775, 13);
            this.GenTxt.TabIndex = 5;
            this.GenTxt.Text = "Калькулятор поправок для миномета 2Б14-1";
            this.GenTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // natoTxt
            // 
            this.natoTxt.AutoSize = true;
            this.natoTxt.Location = new System.Drawing.Point(70, 41);
            this.natoTxt.Name = "natoTxt";
            this.natoTxt.Size = new System.Drawing.Size(72, 13);
            this.natoTxt.TabIndex = 4;
            this.natoTxt.Text = "НАТО шкала";
            // 
            // ussrTxt
            // 
            this.ussrTxt.AutoSize = true;
            this.ussrTxt.Location = new System.Drawing.Point(162, 41);
            this.ussrTxt.Name = "ussrTxt";
            this.ussrTxt.Size = new System.Drawing.Size(96, 13);
            this.ussrTxt.TabIndex = 8;
            this.ussrTxt.Text = "Советская шкала";
            // 
            // azimuthAimingUSSR
            // 
            this.azimuthAimingUSSR.Location = new System.Drawing.Point(158, 57);
            this.azimuthAimingUSSR.MaxLength = 4;
            this.azimuthAimingUSSR.Name = "azimuthAimingUSSR";
            this.azimuthAimingUSSR.Size = new System.Drawing.Size(100, 20);
            this.azimuthAimingUSSR.TabIndex = 6;
            this.azimuthAimingUSSR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // azimuthTargetUSSR
            // 
            this.azimuthTargetUSSR.Location = new System.Drawing.Point(158, 83);
            this.azimuthTargetUSSR.MaxLength = 4;
            this.azimuthTargetUSSR.Name = "azimuthTargetUSSR";
            this.azimuthTargetUSSR.Size = new System.Drawing.Size(100, 20);
            this.azimuthTargetUSSR.TabIndex = 7;
            this.azimuthTargetUSSR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // azimuthBussolTxt
            // 
            this.azimuthBussolTxt.AutoSize = true;
            this.azimuthBussolTxt.Location = new System.Drawing.Point(11, 112);
            this.azimuthBussolTxt.Name = "azimuthBussolTxt";
            this.azimuthBussolTxt.Size = new System.Drawing.Size(22, 13);
            this.azimuthBussolTxt.TabIndex = 42;
            this.azimuthBussolTxt.Text = "УБ";
            // 
            // azimuthBussolNATO
            // 
            this.azimuthBussolNATO.Location = new System.Drawing.Point(52, 109);
            this.azimuthBussolNATO.MaxLength = 4;
            this.azimuthBussolNATO.Name = "azimuthBussolNATO";
            this.azimuthBussolNATO.Size = new System.Drawing.Size(100, 20);
            this.azimuthBussolNATO.TabIndex = 43;
            // 
            // azimuthBussolUSSR
            // 
            this.azimuthBussolUSSR.Location = new System.Drawing.Point(158, 109);
            this.azimuthBussolUSSR.MaxLength = 4;
            this.azimuthBussolUSSR.Name = "azimuthBussolUSSR";
            this.azimuthBussolUSSR.Size = new System.Drawing.Size(100, 20);
            this.azimuthBussolUSSR.TabIndex = 44;
            // 
            // windTxt
            // 
            this.windTxt.AutoSize = true;
            this.windTxt.Location = new System.Drawing.Point(12, 321);
            this.windTxt.Name = "windTxt";
            this.windTxt.Size = new System.Drawing.Size(37, 13);
            this.windTxt.TabIndex = 9;
            this.windTxt.Text = "Ветер";
            // 
            // wind
            // 
            this.wind.Location = new System.Drawing.Point(158, 318);
            this.wind.MaxLength = 4;
            this.wind.Name = "wind";
            this.wind.Size = new System.Drawing.Size(100, 20);
            this.wind.TabIndex = 10;
            this.wind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly_KeyPress);
            // 
            // altitudeTxt
            // 
            this.altitudeTxt.AutoSize = true;
            this.altitudeTxt.Location = new System.Drawing.Point(79, 157);
            this.altitudeTxt.Name = "altitudeTxt";
            this.altitudeTxt.Size = new System.Drawing.Size(47, 13);
            this.altitudeTxt.TabIndex = 11;
            this.altitudeTxt.Text = "Высоты";
            // 
            // altitudeOurTxt
            // 
            this.altitudeOurTxt.AutoSize = true;
            this.altitudeOurTxt.Location = new System.Drawing.Point(13, 185);
            this.altitudeOurTxt.Name = "altitudeOurTxt";
            this.altitudeOurTxt.Size = new System.Drawing.Size(35, 13);
            this.altitudeOurTxt.TabIndex = 12;
            this.altitudeOurTxt.Text = "Наша";
            // 
            // altitudeTargetTxt
            // 
            this.altitudeTargetTxt.AutoSize = true;
            this.altitudeTargetTxt.Location = new System.Drawing.Point(13, 222);
            this.altitudeTargetTxt.Name = "altitudeTargetTxt";
            this.altitudeTargetTxt.Size = new System.Drawing.Size(33, 13);
            this.altitudeTargetTxt.TabIndex = 13;
            this.altitudeTargetTxt.Text = "Цели";
            // 
            // altitudeOur
            // 
            this.altitudeOur.Location = new System.Drawing.Point(52, 182);
            this.altitudeOur.MaxLength = 4;
            this.altitudeOur.Name = "altitudeOur";
            this.altitudeOur.Size = new System.Drawing.Size(100, 20);
            this.altitudeOur.TabIndex = 14;
            this.altitudeOur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // altitudeTarget
            // 
            this.altitudeTarget.Location = new System.Drawing.Point(52, 219);
            this.altitudeTarget.MaxLength = 4;
            this.altitudeTarget.Name = "altitudeTarget";
            this.altitudeTarget.Size = new System.Drawing.Size(100, 20);
            this.altitudeTarget.TabIndex = 15;
            this.altitudeTarget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // weatherTxt
            // 
            this.weatherTxt.AutoSize = true;
            this.weatherTxt.Location = new System.Drawing.Point(49, 290);
            this.weatherTxt.Name = "weatherTxt";
            this.weatherTxt.Size = new System.Drawing.Size(102, 13);
            this.weatherTxt.TabIndex = 16;
            this.weatherTxt.Text = "Погодные условия";
            // 
            // pressureTxt
            // 
            this.pressureTxt.AutoSize = true;
            this.pressureTxt.Location = new System.Drawing.Point(13, 373);
            this.pressureTxt.Name = "pressureTxt";
            this.pressureTxt.Size = new System.Drawing.Size(58, 13);
            this.pressureTxt.TabIndex = 17;
            this.pressureTxt.Text = "Давление";
            // 
            // pressureAir
            // 
            this.pressureAir.Location = new System.Drawing.Point(158, 370);
            this.pressureAir.MaxLength = 4;
            this.pressureAir.Name = "pressureAir";
            this.pressureAir.Size = new System.Drawing.Size(100, 20);
            this.pressureAir.TabIndex = 18;
            this.pressureAir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // temperatureTxt
            // 
            this.temperatureTxt.AutoSize = true;
            this.temperatureTxt.Location = new System.Drawing.Point(12, 399);
            this.temperatureTxt.Name = "temperatureTxt";
            this.temperatureTxt.Size = new System.Drawing.Size(74, 13);
            this.temperatureTxt.TabIndex = 19;
            this.temperatureTxt.Text = "Температура";
            // 
            // temperature
            // 
            this.temperature.Location = new System.Drawing.Point(158, 396);
            this.temperature.MaxLength = 4;
            this.temperature.Name = "temperature";
            this.temperature.Size = new System.Drawing.Size(100, 20);
            this.temperature.TabIndex = 20;
            this.temperature.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly_KeyPress);
            // 
            // windDirectionTxt
            // 
            this.windDirectionTxt.AutoSize = true;
            this.windDirectionTxt.Location = new System.Drawing.Point(12, 347);
            this.windDirectionTxt.Name = "windDirectionTxt";
            this.windDirectionTxt.Size = new System.Drawing.Size(107, 13);
            this.windDirectionTxt.TabIndex = 21;
            this.windDirectionTxt.Text = "Направление ветра";
            // 
            // windDirection
            // 
            this.windDirection.Location = new System.Drawing.Point(158, 344);
            this.windDirection.MaxLength = 4;
            this.windDirection.Name = "windDirection";
            this.windDirection.Size = new System.Drawing.Size(100, 20);
            this.windDirection.TabIndex = 22;
            this.windDirection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly_KeyPress);
            // 
            // delimiter
            // 
            this.delimiter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delimiter.Location = new System.Drawing.Point(281, 41);
            this.delimiter.Name = "delimiter";
            this.delimiter.Size = new System.Drawing.Size(13, 375);
            this.delimiter.TabIndex = 23;
            this.delimiter.Text = "|||||||||||||||||||||||||||||";
            this.delimiter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rangeTxt
            // 
            this.rangeTxt.AutoSize = true;
            this.rangeTxt.Location = new System.Drawing.Point(301, 60);
            this.rangeTxt.Name = "rangeTxt";
            this.rangeTxt.Size = new System.Drawing.Size(63, 13);
            this.rangeTxt.TabIndex = 24;
            this.rangeTxt.Text = "Дальность";
            // 
            // range
            // 
            this.range.Location = new System.Drawing.Point(440, 53);
            this.range.MaxLength = 7;
            this.range.Name = "range";
            this.range.Size = new System.Drawing.Size(100, 20);
            this.range.TabIndex = 25;
            this.range.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitOnly_KeyPress);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(440, 97);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 27;
            // 
            // loadTxt
            // 
            this.loadTxt.AutoSize = true;
            this.loadTxt.Location = new System.Drawing.Point(301, 100);
            this.loadTxt.Name = "loadTxt";
            this.loadTxt.Size = new System.Drawing.Size(133, 13);
            this.loadTxt.TabIndex = 28;
            this.loadTxt.Text = "Рекомендуемый снаряд:";
            // 
            // loadHelpTxt
            // 
            this.loadHelpTxt.Location = new System.Drawing.Point(301, 122);
            this.loadHelpTxt.Name = "loadHelpTxt";
            this.loadHelpTxt.Size = new System.Drawing.Size(440, 30);
            this.loadHelpTxt.TabIndex = 29;
            this.loadHelpTxt.Text = "Будет выбран автоматически. Можно сменить вручную, но если снаряд не подходит по " +
                "дальности - будет выдана ошибка.";
            // 
            // isArcShoot
            // 
            this.isArcShoot.AutoSize = true;
            this.isArcShoot.Location = new System.Drawing.Point(304, 171);
            this.isArcShoot.Name = "isArcShoot";
            this.isArcShoot.Size = new System.Drawing.Size(248, 17);
            this.isArcShoot.TabIndex = 30;
            this.isArcShoot.Text = "Навесом по возможности (для артиллерии)";
            this.isArcShoot.UseVisualStyleBackColor = true;
            // 
            // characteristicTxt
            // 
            this.characteristicTxt.AutoSize = true;
            this.characteristicTxt.Location = new System.Drawing.Point(461, 219);
            this.characteristicTxt.Name = "characteristicTxt";
            this.characteristicTxt.Size = new System.Drawing.Size(135, 13);
            this.characteristicTxt.TabIndex = 35;
            this.characteristicTxt.Text = "Характеристики снаряда";
            // 
            // maxRangeTxtTxt
            // 
            this.maxRangeTxtTxt.AutoSize = true;
            this.maxRangeTxtTxt.Location = new System.Drawing.Point(316, 247);
            this.maxRangeTxtTxt.Name = "maxRangeTxtTxt";
            this.maxRangeTxtTxt.Size = new System.Drawing.Size(137, 13);
            this.maxRangeTxtTxt.TabIndex = 36;
            this.maxRangeTxtTxt.Text = "Минимальная дальность:";
            // 
            // maxRangeTxt
            // 
            this.maxRangeTxt.AutoSize = true;
            this.maxRangeTxt.Location = new System.Drawing.Point(316, 278);
            this.maxRangeTxt.Name = "maxRangeTxt";
            this.maxRangeTxt.Size = new System.Drawing.Size(143, 13);
            this.maxRangeTxt.TabIndex = 37;
            this.maxRangeTxt.Text = "Максимальная дальность:";
            // 
            // minRange
            // 
            this.minRange.AutoSize = true;
            this.minRange.Location = new System.Drawing.Point(480, 247);
            this.minRange.Name = "minRange";
            this.minRange.Size = new System.Drawing.Size(13, 13);
            this.minRange.TabIndex = 38;
            this.minRange.Text = "0";
            // 
            // maxRange
            // 
            this.maxRange.AutoSize = true;
            this.maxRange.Location = new System.Drawing.Point(480, 278);
            this.maxRange.Name = "maxRange";
            this.maxRange.Size = new System.Drawing.Size(13, 13);
            this.maxRange.TabIndex = 39;
            this.maxRange.Text = "0";
            // 
            // canArcShootTxt
            // 
            this.canArcShootTxt.AutoSize = true;
            this.canArcShootTxt.Location = new System.Drawing.Point(520, 247);
            this.canArcShootTxt.Name = "canArcShootTxt";
            this.canArcShootTxt.Size = new System.Drawing.Size(179, 13);
            this.canArcShootTxt.TabIndex = 45;
            this.canArcShootTxt.Text = "Возможность стрельбы навесом:";
            // 
            // canArcShoot
            // 
            this.canArcShoot.AutoSize = true;
            this.canArcShoot.Location = new System.Drawing.Point(715, 247);
            this.canArcShoot.Name = "canArcShoot";
            this.canArcShoot.Size = new System.Drawing.Size(26, 13);
            this.canArcShoot.TabIndex = 46;
            this.canArcShoot.Text = "Нет";
            // 
            // avgTimeTxt
            // 
            this.avgTimeTxt.AutoSize = true;
            this.avgTimeTxt.Location = new System.Drawing.Point(520, 278);
            this.avgTimeTxt.Name = "avgTimeTxt";
            this.avgTimeTxt.Size = new System.Drawing.Size(171, 13);
            this.avgTimeTxt.TabIndex = 47;
            this.avgTimeTxt.Text = "Среднее время полета снаряда:";
            // 
            // avgTime
            // 
            this.avgTime.AutoSize = true;
            this.avgTime.Location = new System.Drawing.Point(715, 278);
            this.avgTime.Name = "avgTime";
            this.avgTime.Size = new System.Drawing.Size(22, 13);
            this.avgTime.TabIndex = 48;
            this.avgTime.Text = "0.0";
            // 
            // resultHorizontalTxt
            // 
            this.resultHorizontalTxt.AutoSize = true;
            this.resultHorizontalTxt.Location = new System.Drawing.Point(301, 373);
            this.resultHorizontalTxt.Name = "resultHorizontalTxt";
            this.resultHorizontalTxt.Size = new System.Drawing.Size(138, 13);
            this.resultHorizontalTxt.TabIndex = 31;
            this.resultHorizontalTxt.Text = "Горизонтальная наводка:";
            // 
            // resultVerticalTxt
            // 
            this.resultVerticalTxt.AutoSize = true;
            this.resultVerticalTxt.Location = new System.Drawing.Point(301, 399);
            this.resultVerticalTxt.Name = "resultVerticalTxt";
            this.resultVerticalTxt.Size = new System.Drawing.Size(127, 13);
            this.resultVerticalTxt.TabIndex = 32;
            this.resultVerticalTxt.Text = "Вертикальная наводка:";
            // 
            // resultHorizontal
            // 
            this.resultHorizontal.AutoSize = true;
            this.resultHorizontal.Location = new System.Drawing.Point(461, 373);
            this.resultHorizontal.Name = "resultHorizontal";
            this.resultHorizontal.Size = new System.Drawing.Size(31, 13);
            this.resultHorizontal.TabIndex = 33;
            this.resultHorizontal.Text = "3000";
            // 
            // resultVertical
            // 
            this.resultVertical.AutoSize = true;
            this.resultVertical.Location = new System.Drawing.Point(461, 399);
            this.resultVertical.Name = "resultVertical";
            this.resultVertical.Size = new System.Drawing.Size(31, 13);
            this.resultVertical.TabIndex = 34;
            this.resultVertical.Text = "1000";
            // 
            // resultTimeTxt
            // 
            this.resultTimeTxt.AutoSize = true;
            this.resultTimeTxt.Location = new System.Drawing.Point(520, 373);
            this.resultTimeTxt.Name = "resultTimeTxt";
            this.resultTimeTxt.Size = new System.Drawing.Size(176, 13);
            this.resultTimeTxt.TabIndex = 40;
            this.resultTimeTxt.Text = "Итоговое время полета снаряда:";
            // 
            // resultTime
            // 
            this.resultTime.AutoSize = true;
            this.resultTime.Location = new System.Drawing.Point(702, 373);
            this.resultTime.Name = "resultTime";
            this.resultTime.Size = new System.Drawing.Size(13, 13);
            this.resultTime.TabIndex = 41;
            this.resultTime.Text = "0";
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.avgTime);
            this.Controls.Add(this.avgTimeTxt);
            this.Controls.Add(this.canArcShoot);
            this.Controls.Add(this.canArcShootTxt);
            this.Controls.Add(this.azimuthBussolUSSR);
            this.Controls.Add(this.azimuthBussolNATO);
            this.Controls.Add(this.azimuthBussolTxt);
            this.Controls.Add(this.resultTime);
            this.Controls.Add(this.resultTimeTxt);
            this.Controls.Add(this.maxRange);
            this.Controls.Add(this.minRange);
            this.Controls.Add(this.maxRangeTxt);
            this.Controls.Add(this.maxRangeTxtTxt);
            this.Controls.Add(this.characteristicTxt);
            this.Controls.Add(this.resultVertical);
            this.Controls.Add(this.resultHorizontal);
            this.Controls.Add(this.resultVerticalTxt);
            this.Controls.Add(this.resultHorizontalTxt);
            this.Controls.Add(this.isArcShoot);
            this.Controls.Add(this.loadHelpTxt);
            this.Controls.Add(this.loadTxt);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.range);
            this.Controls.Add(this.rangeTxt);
            this.Controls.Add(this.delimiter);
            this.Controls.Add(this.windDirection);
            this.Controls.Add(this.windDirectionTxt);
            this.Controls.Add(this.temperature);
            this.Controls.Add(this.temperatureTxt);
            this.Controls.Add(this.pressureAir);
            this.Controls.Add(this.pressureTxt);
            this.Controls.Add(this.weatherTxt);
            this.Controls.Add(this.altitudeTarget);
            this.Controls.Add(this.altitudeOur);
            this.Controls.Add(this.altitudeTargetTxt);
            this.Controls.Add(this.altitudeOurTxt);
            this.Controls.Add(this.altitudeTxt);
            this.Controls.Add(this.wind);
            this.Controls.Add(this.windTxt);
            this.Controls.Add(this.ussrTxt);
            this.Controls.Add(this.azimuthTargetUSSR);
            this.Controls.Add(this.azimuthAimingUSSR);
            this.Controls.Add(this.GenTxt);
            this.Controls.Add(this.natoTxt);
            this.Controls.Add(this.azimuthTargetNATO);
            this.Controls.Add(this.azimuthTargetTxt);
            this.Controls.Add(this.azimuthAimingNATO);
            this.Controls.Add(this.azimuthAimingTxt);
            this.Name = "Calculator";
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label azimuthAimingTxt;
        private System.Windows.Forms.TextBox azimuthAimingNATO;
        private System.Windows.Forms.Label azimuthTargetTxt;
        private System.Windows.Forms.TextBox azimuthTargetNATO;
        private System.Windows.Forms.Label natoTxt;
        private System.Windows.Forms.Label GenTxt;
        private System.Windows.Forms.TextBox azimuthAimingUSSR;
        private System.Windows.Forms.TextBox azimuthTargetUSSR;
        private System.Windows.Forms.Label ussrTxt;
        private System.Windows.Forms.Label windTxt;
        private System.Windows.Forms.TextBox wind;
        private System.Windows.Forms.Label altitudeTxt;
        private System.Windows.Forms.Label altitudeOurTxt;
        private System.Windows.Forms.Label altitudeTargetTxt;
        private System.Windows.Forms.TextBox altitudeOur;
        private System.Windows.Forms.TextBox altitudeTarget;
        private System.Windows.Forms.Label weatherTxt;
        private System.Windows.Forms.Label pressureTxt;
        private System.Windows.Forms.TextBox pressureAir;
        private System.Windows.Forms.Label temperatureTxt;
        private System.Windows.Forms.TextBox temperature;
        private System.Windows.Forms.Label windDirectionTxt;
        private System.Windows.Forms.TextBox windDirection;
        private System.Windows.Forms.Label delimiter;
        private System.Windows.Forms.Label rangeTxt;
        private System.Windows.Forms.TextBox range;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label loadTxt;
        private System.Windows.Forms.Label loadHelpTxt;
        private System.Windows.Forms.CheckBox isArcShoot;
        private System.Windows.Forms.Label resultHorizontalTxt;
        private System.Windows.Forms.Label resultVerticalTxt;
        private System.Windows.Forms.Label resultHorizontal;
        private System.Windows.Forms.Label resultVertical;
        private System.Windows.Forms.Label characteristicTxt;
        private System.Windows.Forms.Label maxRangeTxtTxt;
        private System.Windows.Forms.Label maxRangeTxt;
        private System.Windows.Forms.Label minRange;
        private System.Windows.Forms.Label maxRange;
        private System.Windows.Forms.Label resultTimeTxt;
        private System.Windows.Forms.Label resultTime;
        private System.Windows.Forms.Label azimuthBussolTxt;
        private System.Windows.Forms.TextBox azimuthBussolNATO;
        private System.Windows.Forms.TextBox azimuthBussolUSSR;
        private System.Windows.Forms.Label canArcShootTxt;
        private System.Windows.Forms.Label canArcShoot;
        private System.Windows.Forms.Label avgTimeTxt;
        private System.Windows.Forms.Label avgTime;
    }
}