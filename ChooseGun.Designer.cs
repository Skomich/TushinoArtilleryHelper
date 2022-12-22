
using System.Collections.Generic;
using System.Linq;

namespace ArtilleryHelper
{
    partial class ChooseGun
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
            this.mainPanel = new System.Windows.Forms.FlowLayoutPanel();

            foreach (var gun in guns)
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
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(12, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(776, 426);
            this.mainPanel.TabIndex = 0;
            // 
            // ChooseGun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainPanel);
            this.Name = "ChooseGun";
            this.Text = "ChooseGun";
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel mainPanel;
    }
}