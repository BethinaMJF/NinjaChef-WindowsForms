using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ninjaT5.PAGES
{
    public partial class menu : baseForm
    {
        private dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        private MemoryStream ms { get; set; } = new MemoryStream();

        public menu()
        {
            InitializeComponent();
            carregarFoto();
            label2.Text = dados.usuarioAtual.nick;
            comboBox1.SelectedIndex = dados.dificuldadeJogo;
        }

        #region Navegação
        private void button2_Click(object sender, EventArgs e)
        {
            new PAGES.ranking().Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new PAGES.mercado().Show();
            Hide();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            dados.dificuldadeJogo = comboBox1.SelectedIndex;
            new jogo() { tipoDeJogo = "classico" }.Show();
            Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            dados.dificuldadeJogo = comboBox1.SelectedIndex;
            new jogo() { tipoDeJogo = "arcade" }.Show();
            Hide();
        }

        #endregion

        #region Editar Foto

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var user = ct.Usuario.FirstOrDefault(u => u.id == dados.usuarioAtual.id);

            // Alterar foto
            if (pictureBox4.Image != null)
            {
                var novaImagem = ms.ToArray();
                user.foto = ms.ToArray();
            }
            else
            {
                user.foto = null;
            }
            ct.SaveChanges();
            panel1.Visible = false;
            pictureBox4.Image = null;
            dados.usuarioAtual = user; 
            carregarFoto();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            pictureBox4.Image = null;
            panel1.Visible = false;
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != null)
            {
                ms = new MemoryStream();
                pictureBox4.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox4.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        #endregion


        private void carregarFoto()
        {
            pictureBox1.Image = dados.usuarioAtual.foto == null ? Properties.Resources.semFoto : Image.FromStream(new MemoryStream(dados.usuarioAtual.foto)) ;
        }
    }
}
