using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ninjaT5.PAGES
{
    public partial class cadastro : baseForm
    {
        dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public MemoryStream ms { get; set; } = new MemoryStream();

        public cadastro()
        {
            InitializeComponent();
        }
        #region Visibilidade senha
        private void label1_Click(object sender, EventArgs e)
        {
            button1.Focus();
            textBox3.UseSystemPasswordChar = !(bool)textBox3.UseSystemPasswordChar;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            button1.Focus();
            textBox4.UseSystemPasswordChar = !(bool)textBox4.UseSystemPasswordChar;

        }
        #endregion

        #region Placeholder e Focos

        private void cadastro_Load(object sender, EventArgs e)
        {
            textBox1.Text = "nick";
            textBox2.Text = "email";
            textBox3.Text = "senha";
            textBox4.Text = "confirmar senha";

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.UseSystemPasswordChar = true;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox4.UseSystemPasswordChar = true;

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificar() == "ok")
            {
                var salt = new byte[8];
                new RNGCryptoServiceProvider().GetBytes(salt);
                var rfc = new Rfc2898DeriveBytes(textBox3.Text, salt    );
                string senha = Convert.ToBase64String(rfc.GetBytes(32));
                string saltString = Convert.ToBase64String(salt);

                var usuario = new Usuario()
                {
                    nick = textBox1.Text,
                    email = textBox2.Text,
                    senha = senha,
                    salt = saltString,
                    foto = pictureBox1.Image != null ? ms.ToArray() : null,
                    idEspadaAtual = 1
                };
                ct.Usuario.Add(usuario);

                var espadaUsuario = new espadasUsuario()
                {
                    idEspada = 1,
                    idUsuario = usuario.id
                };
                ct.espadasUsuario.Add(espadaUsuario);

                ct.SaveChanges();
                dados.usuarioAtual = ct.Usuario.FirstOrDefault(u => u.email == textBox2.Text);
                new PAGES.menu().Show();
                Hide();
            }
            else
            {
                MessageBox.Show(verificar(), "Fruits Ninja", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string verificar()
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) )
                return "Preencha todos os campos";

            if (!Regex.IsMatch(textBox1.Text, "^[a-zA-Z0-9]+$"))
                return "O nick precisa conter apenas letras e numeros";


            if (!Regex.IsMatch(textBox2.Text, "^[a-zA-Z]+@[a-zA-Z]+\\.[a-zA-Z]+$"))
                return "O email precisa conter o padrao letras@letras.letras";

            if (ct.Usuario.FirstOrDefault(u => u.nick == textBox1.Text) != null)
                return "O nick ja esta sendo usado, digite um novo";

            if (ct.Usuario.FirstOrDefault(u => u.email == textBox2.Text) != null)
                return "O email ja esta sendo usado, digite um novo";

            if (textBox3.Text  != textBox4.Text)
                return "As senha estao diferentes";

            if (textBox3.Text.Length < 5 || textBox3.Text.Length > 10)
                return "A senha precisa conter entre 5 e 10 caracteres";

            return "ok";
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK &&  openFileDialog1.FileName != null)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        private void cadastro_Resize(object sender, EventArgs e)
        {
           
          panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
          panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;

        }

        private void label4_Click(object sender, EventArgs e)
        {
            new login().Show();
            Hide();
        }
    }
}
