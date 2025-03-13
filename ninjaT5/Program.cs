using ninjaT5.PAGES;
using ninjaT5.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace ninjaT5
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
          /*  var p = new WindowsMediaPlayer() { URL = "Resources\\musica.mp3" };
            p.controls.play();*/
            
            var settings = new Settings();
            if (settings.PrimeiraVez == false)
            {
                settings.PrimeiraVez = true;
                settings.Save();
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new login());

            }

        }
    }
}
