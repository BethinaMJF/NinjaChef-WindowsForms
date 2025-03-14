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
        public string dificuldade { get; set; }
        public string novamente { get; set; }
        public dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public PictureBox p { get; set; }
        public int y0, g = 1, direcao, xFinal, tempo, pontosGanhos, vidas = 3, tempo60 = 60;
        public double v0x, v0y;
        public Random rand = new Random();
        public bool cortou = false;

        public jogo()
        {
            InitializeComponent();
            
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            this.DoubleBuffered = true; // Reduz flickering
        }
        private void jogo_Load(object sender, EventArgs e)
        {
            var imgEspada = new Bitmap((Image)Properties.Resources.ResourceManager.GetObject($"espada__{dados.atual.idEspadaAtual}_"), 30, 130).GetHicon();
            Cursor = new Cursor(imgEspada);

            timer1.Interval = dificuldade == "Fácil" ? 30 : dificuldade == "Médio" ? 22 : 16;

            if (tipoDeJogo == "classico")
            {
                label1.Text = "❤️ ❤️ ❤️";
            }
            else
            {
                label1.Text = "60";
                timer2.Start();
            }

            CRIAR();
            timer1.Start();
        }

        private void jogo_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(this, DragDropEffects.Copy);
            cortou = true;
        }

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

        private void jogo_MouseUp(object sender, MouseEventArgs e)
        {
            cortou = false;
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            if (tempo60 > 0 )
            {
                tempo60--;
                label1.Text = tempo60.ToString();
            }
            else
            {
                ENCERRAR();
            }
        }

        private void CRIAR()
        {
            var tipo = rand.Next(0,4); 
            p= new PictureBox()
            {
                Height= 60, 
                Width= 60,
                AllowDrop = true,
                Location = new Point(0, this.Height ),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = tipo == 0 ? "bomba" : "fruta "
            };
            p.DragEnter += P_DragEnter;
            
            if (p.Tag.ToString() == ("bomba"))
            {
                p.Image = Properties.Resources.bomba;
            }
            else
            {
                var ind = rand.Next(1, ct.Ingrediente.Count());
                p.Image = (Image)Properties.Resources.ResourceManager.GetObject($"{ct.Ingrediente.FirstOrDefault(u=> u.id == ind).Ingredientes}");
                p.Tag += ind.ToString();
            }
            Controls.Add(p);
            CARREGAR();

        }


        private void CARREGAR()
        {
            tempo = 0;
            y0 = this.Height;
            xFinal = rand.Next(60, this.Height - 60);
            v0x = rand.Next(20,40);
            v0y = rand.Next(20, 32);
            direcao = rand.Next(0,2);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x;
            if (direcao == 0)
            {
                x = (int)(v0x + tempo + xFinal);
            }
            else
            {
                x = (int)(v0x - tempo + xFinal);

            }

            var y = (int)(y0 - v0y * tempo + (g * Math.Pow(tempo, 2)) / 2);
            p.Location = new Point(x, y);
            tempo++;
            if (y > this.Height)
            {
                if (tipoDeJogo == "classico" && !p.Tag.ToString().Contains("bomba"))
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
                            ENCERRAR();
                            break;
                        default:
                            break;
                    }
                }
                p.Dispose();
                CRIAR();
            }


        }
        private async void P_DragEnter(object sender, DragEventArgs e)
        {

            var h = new Bitmap((Image)Properties.Resources.ResourceManager.GetObject($"espada__{dados.atual.idEspadaAtual}_"), 30,130).GetHicon();
            Cursor = new Cursor(h);
            if (cortou)
            {
                cortou = false;
                
                if (p.Tag.ToString().Contains("bomba"))
                {
                   
                    if (tipoDeJogo == "classico")
                    {
                        timer1.Stop();
                        p.BackColor = Color.Transparent;
                        p.Image = Properties.Resources.explosaoGif;
                        await Task.Delay(500);
                        ENCERRAR();
                    }
                    else
                    {
                        if ((tempo60 - 10) > 0)
                        {
                            tempo60 -= 10;
                            label1.Text = tempo60.ToString();
                            label1.Font = new Font(label1.Font, FontStyle.Bold);
                            await Task.Delay(300);
                            label1.Font = new Font(label1.Font, FontStyle.Regular);

                            timer1.Stop();
                            p.BackColor = Color.Transparent;
                            p.Image = Properties.Resources.explosaoGif;
                            await Task.Delay(500);
                            timer1.Start();
                        }
                        else
                        {
                            ENCERRAR();
                        }

                    }
                    
                }
                else
                {
                    var id = int.Parse(p.Tag.ToString().Split(' ')[1]);
                    var pontos = ct.Ingrediente.FirstOrDefault(u=> u.id == id);
                    pontosGanhos += pontos.ponto.Value;
                    label2.Text = pontosGanhos.ToString() + " pontos";

                    p.Image = (Image)Properties.Resources.ResourceManager.GetObject($"{pontos.Ingredientes}c");
                    await Task.Delay(100);

                }
                p.Dispose();
                CRIAR();
            }
        }

        private  void ENCERRAR()
        {

            var nv = new ExtratoJogador()
            {
                idOrigemPontos = tipoDeJogo == "classico" ? 2 : 3,
                idUsuario = dados.atual.id,
                pontos = pontosGanhos,

            };
            ct.ExtratoJogador.Add(nv);
            ct.SaveChanges();

            timer1.Stop();
            timer2.Stop();
            new score(pontosGanhos) { tipoDeJogoScore = tipoDeJogo, dificuldadeScore = dificuldade}.Show();
            Hide();
        }

    }
}
