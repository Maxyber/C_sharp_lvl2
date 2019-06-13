using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Game.flagUp = true;
                    break;
                case Keys.A:
                    Game.flagLeft = true;
                    break;
                case Keys.S:
                    Game.flagDown = true;
                    break;
                case Keys.D:
                    Game.flagRight = true;
                    break;
            }
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Game.flagUp = false;
                    break;
                case Keys.A:
                    Game.flagLeft = false;
                    break;
                case Keys.S:
                    Game.flagDown = false;
                    break;
                case Keys.D:
                    Game.flagRight = false;
                    break;
            }
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                if ((ClientSize.Width > 1600) || (ClientSize.Height > 900)) throw new ArgumentOutOfRangeException("form resize", "client size too big");
                if ((ClientSize.Width < 160) || (ClientSize.Height < 90)) throw new ArgumentOutOfRangeException("Form resize", "client size too low");
            }
            catch
            {
            }
        }
    }
}
