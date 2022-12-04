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

        public Calculator(Guns mode)
        {
            InitializeComponent();
            /*
            Тут по идее надо по какому-то интерфейсу/абстрактному классу создавать классы для
            конкретного миномета и после этого уже делать расчеты с запросами через классы.
            По типу mortar.GetProjectile(LengthToTarget) и тд. Но пока-что член нам в рот! 
             */
        }

        private void DigitOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 1 && number != 3 && number != 8 && number != 22)
            {
                e.Handled = true;
            }
        }

        private void DecimalOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 1 && number != 3
                && number != 8 && number != 22 && number != 46)
            {
                e.Handled = true;
            }
        }
    }
}
