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

        private Dictionary<String, GunBase> guns = null;
        public ChooseGun(Dictionary<String, GunBase> g)
        {
            this.guns = g;
            InitializeComponent();
        }

        private void OnGunButtonClick(object sender, EventArgs e)
        {
            Calculator calc = new Calculator();
            calc.Show();
        }
    }
}
