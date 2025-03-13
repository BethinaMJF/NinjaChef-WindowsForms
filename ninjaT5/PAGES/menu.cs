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
        dbFrutaNinjaEntities ct = new dbFrutaNinjaEntities();
        public MemoryStream ms { get; set; } = new MemoryStream();

        public menu()
        {
            InitializeComponent();
            carregarFoto();
            label2.Text = dados.atual.nick;
            comboBox1.SelectedIndex = dados.tipoJogo;
        }

        private void carregarFoto()
        {
            if (dados.atual.foto != null)
            {
                pictureBox1.Image = Image.FromStream(new MemoryStream(dados.atual.foto));
            }
            else
            {
                pictureBox1.Image = Properties.Resources.semFoto;

            }

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

        #endregion

        #region Editar Foto

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var user = ct.Usuario.FirstOrDefault(u => u.id == dados.atual.id);

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
            dados.atual = user; 
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new jogo() { tipoDeJogo = "classico", dificuldade = comboBox1.SelectedItem.ToString() }.Show();
            Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            new jogo() { tipoDeJogo = "arcade", dificuldade = comboBox1.SelectedItem.ToString() }.Show();
            Hide();
        }
    }
}
