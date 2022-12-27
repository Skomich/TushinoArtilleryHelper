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
            
            // Добавляем кнопки на страницу
            foreach (var gun in GunList.GetInstance())
            {
                System.Windows.Forms.Button nbs = new System.Windows.Forms.Button();
                this.mainPanel.Controls.Add(nbs);
                nbs.Name = gun.Key;
                nbs.Size = new System.Drawing.Size(188, 79);
                nbs.TabIndex = 0;
                nbs.Text = gun.Key;
                nbs.UseVisualStyleBackColor = true;
                nbs.Click += new System.EventHandler(this.OnGunButtonClick);
            }

            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
        }

        private void OnGunButtonClick(object sender, EventArgs e)
        {
            // Обработчик только кнопок оружия
            Calculator calc = new Calculator(((System.Windows.Forms.Button)sender).Text);
            calc.Show();
        }
    }
}
