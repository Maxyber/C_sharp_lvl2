using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    class MyExceptions : Exception
    {
        public MyExceptions(string msg)
        {
            MessageBox.Show(msg);
            Application.Exit();
        }
        public void ArgumentOutOfRangeException(string msg)
        {
            MessageBox.Show(msg);
            Application.Exit();
        }
        public void GameObjectException(string msg)
        {
            MessageBox.Show(msg);
            Application.Exit();
        }
    }
}
