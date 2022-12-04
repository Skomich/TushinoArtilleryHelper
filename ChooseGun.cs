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
    public partial class ChooseGun : Form
    {
        public ChooseGun()
        {
            InitializeComponent();
        }

        private void b214_Click(object sender, EventArgs e)
        {
            Guns mode = Guns.b214;
            Calculator calc = new Calculator(mode);
            calc.Show();
            //Close();
        }
    }
}
