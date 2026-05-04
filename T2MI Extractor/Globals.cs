using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace T2MI_Extractor
{
    public class Globals
    {

        public static string OFDbutton()
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = ".ts";
                ofd.Filter = "MPEG-2 TS Video (*.ts)|*.ts | All files (*.*)|*.*";
                 if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                 {
                     string filename = ofd.FileName;
                 }
                return ofd.FileName;
            }
    }
}
