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
    public partial class mercado : baseForm
    {
        private int totalUser { get; set; }
        private dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public mercado()
        {
            InitializeComponent();
        }

        public void pontosUsuario()
        {
            totalUser = ct.ExtratoJogador.Where(u => u.idUsuario == dados.usuarioAtual.id).Sum(u => u.pontos) ?? 0;
            label18.Text = $"{totalUser} moedas";
        }

        private void mercado_Load(object sender, EventArgs e)
        {
            DadosMercado();
        }

        public void DadosMercado()
        {
            pontosUsuario();

            for (int i = 0; i < 8; i++)
            {
                var nome = (Label)Controls["label" + (i + 1)];
                var foto = (PictureBox)Controls["pictureBox" + (i + 1)];
                var preco = (Label)Controls["label" + (9 + i)];
                var bloqueado = (PictureBox)Controls["pictureBox" + (i + 9)];
                var btnAcao = (Button)Controls["button" + (i + 1)];

                nome.Text = ct.Espada.FirstOrDefault(u => u.id == (i + 1)).espada1;
                foto.Image = (Image)Properties.Resources.ResourceManager.GetObject($"espada__{(i + 1)}_");
                preco.Text = ct.Espada.FirstOrDefault(u => u.id == (i + 1)).preco.ToString();

                if (dados.usuarioAtual.idEspadaAtual == (1 + i) )
                {
                    btnAcao.Text = "Selecionada";
                    btnAcao.BackColor = ColorTranslator.FromHtml("#f56c00");
                    panel1.Location = foto.Location;
                    bloqueado.Visible = false;
                }
                else if (ct.espadasUsuario.FirstOrDefault(u => u.idUsuario == dados.usuarioAtual.id && u.idEspada == (i + 1)) != null)
                {
                    bloqueado.Visible = false;
                    btnAcao.Text = "Usar";
                    btnAcao.BackColor = Color.Transparent;
                }
                else
                {
                    btnAcao.Text = "Comprar";
                    btnAcao.BackColor = totalUser < int.Parse(preco.Text) ? btnAcao.BackColor = ColorTranslator.FromHtml("#8c1616") : Color.Transparent;
                }

            }
        }

        private void verificarAcao(int id)
        {
            var btnAcao = (Button)Controls["button" + id];
            if (btnAcao.Text == "Comprar")
            {
                comprarEspada(id);
            }
            else
            {
                usarEspada(id);
            }
            DadosMercado();

        }
        private void comprarEspada(int id)
        {
            if (totalUser >= ct.Espada.FirstOrDefault(u => u.id == id).preco)
            {
                var espadasUsuario = new espadasUsuario()
                {
                    idEspada = id,
                    idUsuario = dados.usuarioAtual.id,

                };
                ct.espadasUsuario.Add(espadasUsuario);

                var ExtratoJogador = new ExtratoJogador()
                {
                    idOrigemPontos = 1,
                    pontos = -(((int)ct.Espada.FirstOrDefault(u => u.id == (id)).preco.Value)),
                    idUsuario = dados.usuarioAtual.id
                };
                ct.ExtratoJogador.Add(ExtratoJogador);

                ct.SaveChanges();
                dados.usuarioAtual = ct.Usuario.FirstOrDefault(u => u.id == dados.usuarioAtual.id);
                pontosUsuario();
            }

        }

        private void usarEspada(int id)
        {
            var espadaAtual = Controls["button" + dados.usuarioAtual.idEspadaAtual];
            espadaAtual.Text = "Usar";

            var espadaSelecionada = Controls["button" + id];
            espadaSelecionada.Text = "Selecionada";

            var user = ct.Usuario.FirstOrDefault(u => u.id == dados.usuarioAtual.id);
            user.idEspadaAtual = id;
            ct.SaveChanges();
            dados.usuarioAtual = user;
            panel1.Location = Controls["label" + id].Location;

        }

        #region Metodos de Verificar Acao + idEspada selecionada
        private void button1_Click(object sender, EventArgs e)
        {
            verificarAcao(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            verificarAcao(2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            verificarAcao(3);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            verificarAcao(4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            verificarAcao(5);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            verificarAcao(6);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            verificarAcao(7);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            verificarAcao(8);

        }
        #endregion
        private void label19_Click(object sender, EventArgs e)
        {
            new PAGES.menu().Show();
            Hide();
        }
    }
}
