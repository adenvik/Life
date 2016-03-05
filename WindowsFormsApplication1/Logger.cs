using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication1
{
    class Logger
    {
        string path = "";
        public Logger()
        {
            path = "log.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Close();
            }
        }

        public void add(string message)
        {
            if (File.Exists(path))
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
            
        }
    }
}
