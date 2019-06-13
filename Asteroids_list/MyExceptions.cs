using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Asteroids
{
    class MyExceptions : Exception
    {
        string msg;

        public override string Message => this.msg;

        public int Error { get; set; }

        public MyExceptions(string Msg, int Error)
        {
            this.Error = Error;
            this.msg = Msg;
        }

        public override string ToString()
        {
            return $"{this.Error} {base.Message} {this.msg}";
        }
        public void ToFile()
        {
            using (StreamWriter sw = new StreamWriter(@"Logs\logs.txt", true))
                sw.WriteLine($"{DateTime.Now} ... {ToString()}");
        }
    }
}
