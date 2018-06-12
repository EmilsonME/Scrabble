using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions; 


namespace Iskrabol
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            pictureBoxLogo.Parent = pictureBoxBg;
            pictureBoxLaroNa.Parent = pictureBoxBg;

        }

        private void animateLogo()
        {
            if( logoY != 0)
            {
                pictureBoxLogo.Location = new Point(25, logoY);
                logoY += 38;
            }
            else
            {
                pictureBoxLogo.Location = new Point(25, 1);
                timerLogoAnimation.Stop();
            }
        }

        private void btnPlayNow_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBoxLaroNa_Click(object sender, EventArgs e)
        {
            try {
                pictureBoxLaroNa.Image = Image.FromFile(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\graphics\splash screen\button splash screen clicked.png");
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            Board board = new Board();
            board.Show();
        }

        private void pictureBoxLaroNa_MouseHover(object sender, EventArgs e)
        {
            try {
                pictureBoxLaroNa.Image = Image.FromFile(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\graphics\splash screen\button splash screen hover.png");
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        private void pictureBoxLaroNa_MouseLeave(object sender, EventArgs e)
        {
            try {
                pictureBoxLaroNa.Image = Image.FromFile(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\graphics\splash screen\button splash screen.png");
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        int logoY = -304;

        private void timerLogoAnimation_Tick(object sender, EventArgs e)
        {
            animateLogo();
        }
    }
}
