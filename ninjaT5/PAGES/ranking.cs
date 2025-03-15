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
    public partial class ranking : baseForm
    {
        private dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public ranking()
        {
            InitializeComponent();

            var lista = ct.Usuario.Select(u =>
                new dadosPlacar
                {
                    nome = u.nick,
                    pontos = ct.ExtratoJogador.Where(o => o.idOrigemPontos != 1 && o.idUsuario == u.id).Sum(p => p.pontos) ?? 0
                }
            ).OrderByDescending(o=> o.pontos)
            .Take(10)
            .ToList();

            foreach (var item in lista)
            {
                flowLayoutPanel1.Controls.Add(new usuarioPlacar(item));
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            new menu().Show();
            Hide();
        }
    }

    public class dadosPlacar
    {
        public string nome { get; set; }
        public int pontos { get; set; }
    }
}
