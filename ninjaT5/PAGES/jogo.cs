using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ninjaT5.PAGES
{
    public partial class jogo : baseForm
    {
        public string tipoDeJogo { get; set; }

        private dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        private PictureBox pbElemento { get; set; }
        private int y0, g = 1, direcaoQueda, xFinal, tempo, pontosGanhos, vidas = 3, tempoJogada = 60;
        private double velocidadeX, velocidadeY;
        private Random random = new Random();
        private bool cortou = false;

        public jogo()
        {
            InitializeComponent();
        }

        private void jogo_Load(object sender, EventArgs e)
        {
            var imgEspada = new Bitmap((Image)Properties.Resources.ResourceManager.GetObject($"espada__{dados.usuarioAtual.idEspadaAtual}_"), 30, 130).GetHicon();
            Cursor = new Cursor(imgEspada);

            timer1.Interval = dados.dificuldadeJogo == 0 ? 30 : dados.dificuldadeJogo == 1 ? 20 : 5;

            if (tipoDeJogo == "classico")
            {
                label1.Text = "❤️ ❤️ ❤️";
            }
            else
            {
                label1.Text = "60";
                timer2.Start();
            }

            criarElemento();
            timer1.Start();
        }

        // Timer de 60 segundos
        private void timer2_Tick(object sender, EventArgs e) 
        {
            if (tempoJogada > 0)
            {
                tempoJogada--;
                label1.Text = tempoJogada.ToString();
            }
            else
            {
                encerrarJogo();
            }
        }

        // Metodo Pausar
        private void label4_Click(object sender, EventArgs e) 
        {
            timer1.Stop();
            timer2.Stop();

            var confirmacao = MessageBox.Show("Deseja voltar para o menu? Seu progresso e pontos nao serao contabilizados!", "Fruits Ninja", MessageBoxButtons.OKCancel);
            if (confirmacao == DialogResult.OK)
            {
                new menu().Show();
                Hide();
            }
            else
            {
                if (tipoDeJogo == "arcade")
                {
                    timer2.Start();
                }
                timer1.Start();
            }
        }


        #region Animacao
        private void criarElemento()
        {
            var tipoElemento = random.Next(0,4); // Probabilidade de ser bomba 1/4 (zero == bomba)
            pbElemento= new PictureBox()
            {
                Height= 60, 
                Width= 60,
                AllowDrop = true,
                Location = new Point(0, this.Height ),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            pbElemento.DragEnter += P_DragEnter;
            
            if (tipoElemento == 0)
            {
                pbElemento.Image = Properties.Resources.bomba;
                pbElemento.Tag = "bomba";
            }
            else
            {
                var idIngrediente = random.Next(1, ct.Ingrediente.Count());
                pbElemento.Image = (Image)Properties.Resources.ResourceManager.GetObject($"{ct.Ingrediente.FirstOrDefault(u=> u.id == idIngrediente).Ingredientes}");
                pbElemento.Tag = idIngrediente.ToString();
            }
            Controls.Add(pbElemento);
            carregarDados();

        }
        private void carregarDados()
        {
            tempo = 0;
            y0 = this.Height;
            xFinal = random.Next(60, this.Height - 60);
            velocidadeX = random.Next(20,40);
            velocidadeY = random.Next(20, 32);
            direcaoQueda = random.Next(0,2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = direcaoQueda == 0 ? (int)(velocidadeX + tempo + xFinal) : (int)(velocidadeX - tempo + xFinal);
            var y = (int)(y0 - velocidadeY * tempo + (g * Math.Pow(tempo, 2)) / 2);
            pbElemento.Location = new Point(x, y);

            tempo++;
            if (y > this.Height)
            {
                if (tipoDeJogo == "classico" && !pbElemento.Tag.ToString().Contains("bomba"))
                {
                    vidas--;
                    switch (label1.Text)
                    {
                        case "❤️ ❤️ ❤️":
                            label1.Text = "❤️ ❤️";
                            break;
                        case "❤️ ❤️":
                            label1.Text = "❤️";
                            break;
                        case "❤️":
                            encerrarJogo();
                            break;
                        default:
                            break;
                    }
                }
                pbElemento.Dispose();
                criarElemento();
            }
        }

        // Acao de Corte
        private void jogo_MouseUp(object sender, MouseEventArgs e)
        {
            cortou = false;
        }
        private void jogo_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(this, DragDropEffects.Copy);
            cortou = true;
        }
        private async void P_DragEnter(object sender, DragEventArgs e)
        {
            if (cortou)
            {
                cortou = false;
                if (pbElemento.Tag.ToString().Contains("bomba"))
                {
                    if (tipoDeJogo == "classico")
                    {
                        timer1.Stop();
                        pbElemento.Image = Properties.Resources.explosaoGif;
                        await Task.Delay(500);
                        encerrarJogo();
                    }
                    else
                    {
                        if ((tempoJogada - 10) > 0)
                        {
                            tempoJogada -= 10;
                            label1.Text = tempoJogada.ToString();
                            label1.ForeColor = Color.Red;
                            pbElemento.BackColor = Color.Transparent;
                            pbElemento.Image = Properties.Resources.explosaoGif;
                            await Task.Delay(700);
                            label1.ForeColor = Color.FromArgb(247, 186, 1);
                        }
                        else
                        {
                            encerrarJogo();
                        }

                    }
                }
                else
                {
                    var idIngrediente = int.Parse(pbElemento.Tag.ToString());
                    var ingrediente = ct.Ingrediente.FirstOrDefault(u=> u.id == idIngrediente);
                    pontosGanhos += ingrediente.ponto.Value;
                    label2.Text = $"{pontosGanhos} pontos";
                    pbElemento.Image = (Image)Properties.Resources.ResourceManager.GetObject($"{ingrediente.Ingredientes}c");
                    await Task.Delay(100);
                }
                pbElemento.Dispose();
                criarElemento();
            }
        }
        #endregion

        private void encerrarJogo()
        {
            var extratoJogo = new ExtratoJogador()
            {
                idOrigemPontos = tipoDeJogo == "classico" ? 2 : 3,
                idUsuario = dados.usuarioAtual.id,
                pontos = pontosGanhos,

            };
            ct.ExtratoJogador.Add(extratoJogo);
            ct.SaveChanges();

            timer1.Stop();
            timer2.Stop();
            new score(pontosGanhos) { tipoDeJogoScore = tipoDeJogo}.Show();
            Hide();
        }

    }
}
