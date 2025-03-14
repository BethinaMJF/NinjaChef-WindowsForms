using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ninjaT5.PAGES
{
    public partial class usuarioPlacar : UserControl
    {
        public usuarioPlacar(dadosPlacar user)
        {
            InitializeComponent();
            label1.Text = user.nome;
            label2.Text = user.pontos.ToString();
        }
    }
}
