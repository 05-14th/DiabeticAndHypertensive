using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class SplashSreen : Form
    {
        private string logUsername;
        public SplashSreen()
        {
            InitializeComponent();
        }

        private void SplashSreen_Load(object sender, EventArgs e)
        {
            timerSplash.Start();
        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value < 100)
            {
                progressBar.Value += 1;

                label1.Text = progressBar.Value.ToString() + "%";
            }

            else
            {
                timerSplash.Stop();
                SignIn login = new SignIn(logUsername);
                login.Show();
                this.Hide();
            }
        }
    }
}
