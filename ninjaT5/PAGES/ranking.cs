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
        dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public ranking()
        {
            InitializeComponent();

            var lista = ct.Usuario.Select(u =>
                new nv
                {
                    nome = u.nick,
                    pontos = ct.ExtratoJogador.Where(o => o.idOrigemPontos != 1 && o.idUsuario == u.id).Sum(p => p.pontos) ?? 0
                }
            ).OrderByDescending(o=> o.pontos)
            .Take(10)
            .ToList();

            foreach (var item in lista)
            {
                dataGridView1.Rows.Add(item.nome, item.pontos);   
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new menu().Show();
            Hide();
        }
    }

    internal class nv
    {
        public string nome { get; set; }
        public int pontos { get; set; }
    }
}
