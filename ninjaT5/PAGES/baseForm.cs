using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace ninjaT5.PAGES
{
    public partial class baseForm : Form
    {
        public baseForm()
        {
            InitializeComponent();
           
           Icon = Icon.FromHandle(Properties.Resources.apple.GetHicon());
            this.StartPosition = FormStartPosition.CenterScreen;
            // adicionar um usuarioEspada com idEspada == 1 quando Criar um usuario
            // Criar idEspadaAtual cpomo 1 tbm -- default

        }

        private void baseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
