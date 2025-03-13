using ninjaT5.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ninjaT5.PAGES
{
    public partial class login : baseForm
    {
        dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public PictureBox itemTela { get; set; }
        int y0, g = 1, direcao, xFinal, tempo, pontosGanhos;
        double v0x, v0y;
        Random random = new Random();
        public login()
        {
            InitializeComponent();
            button1.Focus();
            timer1.Interval = 30;
            Height += 1;
        }
        private void login_Load(object sender, EventArgs e)
        {
            CRIAR();
            timer1.Start();
            textBox1.Text = "insira seu nick ou email";
            textBox2.Text = "inisira sua senha";

        }
        private void CRIAR() // Apenas alimentos 
        {
            itemTela = new PictureBox()
            {
                Height = 60,
                Width = 60,
                AllowDrop = true,
                Location = new Point(0, this.Height),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            var id = random.Next(1, ct.Ingrediente.Count());
            itemTela.Image = (Image)Properties.Resources.ResourceManager.GetObject($"{ct.Ingrediente.FirstOrDefault(u=> u.id == id).Ingredientes}");
            Controls.Add(itemTela);    
            panel2.SendToBack();
            CARREGAR();
        }

        private void CARREGAR()
        {
            tempo = 0;
            y0 = this.Height;
            int valueFinal = this.Height - 60;
            xFinal = random.Next(60, (valueFinal > 60 ? valueFinal : 61));
            var aceleracaoY = Height / 150;
            var aceleracaoX = Width / 150;
            v0x = random.Next(aceleracaoY + 20, aceleracaoY + 30);
            v0y = random.Next(aceleracaoX + 20, aceleracaoX + 30);
            direcao = random.Next(0, 2);
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
            itemTela.Location = new Point(x + 30, y);
            tempo++;
            if (y > this.Height)
            {
                itemTela.Dispose();
                CRIAR();
            }
        }

        //Limpar Placeholder
        #region
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";

        }

        private void linkLabel1_MouseMove(object sender, MouseEventArgs e)
        {
            linkLabel1.LinkColor =  ColorTranslator.FromHtml("#F7B801");
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.White;

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text != "insira sua senha")
            {
                textBox2.Text = "";
                textBox2.UseSystemPasswordChar = true;
            }
        }
        #endregion


        private void label1_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !(bool)textBox2.UseSystemPasswordChar;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // if(verificarLogin() == "ok")
            {
                //dados.atual = ct.Usuario.FirstOrDefault(u => u.nick == textBox1.Text || u.email == textBox1.Text);
                dados.atual = ct.Usuario.FirstOrDefault(u=> u.id== 2);
                new menu().Show();
                Hide();
            }
           // else
            {
             //  MessageBox.Show(verificarLogin(), "Fruits Ninja", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private string verificarLogin()
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
            string.IsNullOrWhiteSpace(textBox2.Text) ||
             textBox1.Text == "insira seu nick ou email" ||
            textBox2.Text == "inisira sua senha" )
                return "Preencha todos os campos";

            if (!Regex.IsMatch(textBox1.Text, "^[a-zA-Z0-9]+$") && !Regex.IsMatch(textBox1.Text, "^[a-zA-Z]+@[a-zA-Z]+\\.[a-zA-Z]+$"))
                return "O formato de nick ou email esta incorreto";


            if (textBox2.Text.Length < 5 || textBox2.Text.Length > 10)
                return "A senha precisa conter entre 5 e 10 caracteres";

            var user = ct.Usuario.FirstOrDefault(u => u.email == textBox1.Text || u.nick == textBox1.Text);
            if (user != null)
            {

                var salt = Convert.FromBase64String(user.salt);
                var rfc = new Rfc2898DeriveBytes(textBox2.Text, salt);
                var senha = Convert.ToBase64String(rfc.GetBytes(32));
                if (user != null && user.senha != senha)
                    return "Senha incorreta";
            }
            else
            {
                return "Nick ou email nao encontrado";
            }

            return "ok";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settings.PrimeiraVez = false;
            settings.Save();
            new Form1().Show();
            Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new cadastro().Show();
            Hide();

        }
    }
}
