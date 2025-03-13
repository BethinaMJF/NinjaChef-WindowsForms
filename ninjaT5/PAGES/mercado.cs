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
        public mercado()
        {
            InitializeComponent();
        }

        public int totalUser { get; set; }
        dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        private void mercado_Load(object sender, EventArgs e)
        {
            int totalMoedas = ct.ExtratoJogador.Where(u => u.idUsuario == dados.atual.id).Sum(u => u.pontos) ?? 0;
            totalUser = totalMoedas;
            label18.Text = totalUser.ToString() + " moedas";
            DadosMercado();
        }

        public void DadosMercado()
        {
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

                if (ct.Usuario.FirstOrDefault(u => u.id == dados.atual.id && u.idEspadaAtual == (i + 1)) != null)
                {
                    btnAcao.Text = "Selecionada";
                    btnAcao.BackColor = ColorTranslator.FromHtml("#f56c00");
                    panel1.Location = foto.Location;
                    bloqueado.Visible = false;
                }
                else if (ct.espadasUsuario.FirstOrDefault(u => u.idUsuario == dados.atual.id && u.idEspada == (i + 1)) != null)
                {
                    bloqueado.Visible = false;
                    btnAcao.Text = "Usar";
                    btnAcao.BackColor = Color.Transparent;

                }
                else
                {
                    btnAcao.Text = "Comprar";
                    if (totalUser < int.Parse(preco.Text))
                    {
                        btnAcao.BackColor = ColorTranslator.FromHtml("#8c1616");
                    }
                    btnAcao.BackColor = Color.Transparent;

                }

            }
        }

        private void verificar(int id)
        {
            var btnAcao = (Button)Controls["button" + id];
            if (btnAcao.Text == "Comprar")
            {
                comprar(id);
            }
            else
            {
                usar(id);
            }
            DadosMercado();

        }
        private void comprar(int id)
        {
            if (totalUser >= ct.Espada.FirstOrDefault(u => u.id == id).preco)
            {
                var espadasUsuario = new espadasUsuario()
                {
                    idEspada = id,
                    idUsuario = dados.atual.id,

                };

                ct.espadasUsuario.Add(espadasUsuario);

                var ExtratoJogador = new ExtratoJogador()
                {
                    idOrigemPontos = 1,
                    pontos = -(((int)ct.Espada.FirstOrDefault(u => u.id == (id)).preco.Value)),
                    idUsuario = dados.atual.id
                };

                ct.ExtratoJogador.Add(ExtratoJogador);
                ct.SaveChanges();
                dados.atual = ct.Usuario.FirstOrDefault(u => u.id == dados.atual.id);
                totalUser = ct.ExtratoJogador.Where(u => u.idUsuario == dados.atual.id).Sum(u => u.pontos) ?? 0;
                label18.Text = totalUser + " Pontos";
            }

        }

        private void usar(int id)
        {
            var btnEspadaAtual = Controls["button" + dados.atual.idEspadaAtual];
            btnEspadaAtual.Text = "Usar";

            var spadaSelecionada = Controls["button" + id];
            spadaSelecionada.Text = "Selecionada";

            var user = ct.Usuario.FirstOrDefault(u => u.id == dados.atual.id);
            user.idEspadaAtual = id;
            ct.SaveChanges();
            dados.atual = user;
            panel1.Location = Controls["label" + id].Location;

        }

        #region Metodos de verificar acao + idEspada
        private void button1_Click(object sender, EventArgs e)
        {
            verificar(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            verificar(2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            verificar(3);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            verificar(4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            verificar(5);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            verificar(6);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            verificar(7);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            verificar(8);

        }
        #endregion
        private void label19_Click(object sender, EventArgs e)
        {
            new PAGES.menu().Show();
            Hide();
        }
    }
}
