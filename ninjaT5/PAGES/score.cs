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
    public partial class score : baseForm
    {
        private dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public string tipoDeJogoScore { get; set; }
        public score(int pontos)
        {
            InitializeComponent();

            label3.Text = pontos.ToString();
            List<ExtratoJogador> lista = ct.ExtratoJogador.Where(u => u.idUsuario == dados.usuarioAtual.id).ToList();
            if (lista.Count() > 0)
            {
                label5.Text = ct.ExtratoJogador.Where(u => u.idUsuario == dados.usuarioAtual.id).OrderByDescending(u => u.pontos).First().pontos.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new menu().Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new jogo() { tipoDeJogo = tipoDeJogoScore}.Show();
            Hide();
        }
    }
}
