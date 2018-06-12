namespace Iskrabol
{
    partial class SplashScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.pictureBoxBg = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxLaroNa = new System.Windows.Forms.PictureBox();
            this.timerLogoAnimation = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLaroNa)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBg
            // 
            this.pictureBoxBg.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxBg.Image")));
            this.pictureBoxBg.Location = new System.Drawing.Point(-2, 1);
            this.pictureBoxBg.Name = "pictureBoxBg";
            this.pictureBoxBg.Size = new System.Drawing.Size(491, 511);
            this.pictureBoxBg.TabIndex = 0;
            this.pictureBoxBg.TabStop = false;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(25, 1);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(427, 332);
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            // 
            // pictureBoxLaroNa
            // 
            this.pictureBoxLaroNa.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLaroNa.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLaroNa.Image")));
            this.pictureBoxLaroNa.Location = new System.Drawing.Point(25, 336);
            this.pictureBoxLaroNa.Name = "pictureBoxLaroNa";
            this.pictureBoxLaroNa.Size = new System.Drawing.Size(427, 68);
            this.pictureBoxLaroNa.TabIndex = 2;
            this.pictureBoxLaroNa.TabStop = false;
            this.pictureBoxLaroNa.Click += new System.EventHandler(this.pictureBoxLaroNa_Click);
            this.pictureBoxLaroNa.MouseLeave += new System.EventHandler(this.pictureBoxLaroNa_MouseLeave);
            this.pictureBoxLaroNa.MouseHover += new System.EventHandler(this.pictureBoxLaroNa_MouseHover);
            // 
            // timerLogoAnimation
            // 
            this.timerLogoAnimation.Enabled = true;
            this.timerLogoAnimation.Interval = 30;
            this.timerLogoAnimation.Tick += new System.EventHandler(this.timerLogoAnimation_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 408);
            this.Controls.Add(this.pictureBoxLaroNa);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.pictureBoxBg);
            this.Name = "SplashScreen";
            this.Text = "Iskrabol";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLaroNa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBg;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxLaroNa;
        private System.Windows.Forms.Timer timerLogoAnimation;
    }
}

