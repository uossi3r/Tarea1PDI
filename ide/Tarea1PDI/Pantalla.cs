using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Tarea1PDI
{
    public partial class Pantalla : Form
    {
        string imagenCopiada;
        private FuncionesBitmap bitmap;
        public Pantalla()
        {
            InitializeComponent();
            grados.SelectedIndex = 0;
            bitmap = new FuncionesBitmap();
        }

        private void CargarImagen_Click(object sender, EventArgs e)
        {
            try {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Archivo De Bitmap|*.bmp|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                openFileDialog.Multiselect = false;
                bool? userClickedOK = openFileDialog.ShowDialog().ToString().Equals("OK");

                if (userClickedOK == true)
                {
                    imagenCopiada = "imagenCopiada.bmp";
                    File.Copy(openFileDialog.FileName, imagenCopiada, true);
                    bitmap.CargarImagen(imagenCopiada);
                    DireccionArchivo.Text = openFileDialog.FileName;
                    imagenMuestra.ImageLocation = imagenCopiada;
                }
                GC.Collect();
            }
            catch (System.ArgumentException)
            {
                imagenMuestra.Image = null;
                DireccionArchivo.Text = "";
                MessageBox.Show("Error abriendo archivo.");
            }
        }

        private void guardarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog Guardar = new SaveFileDialog();
                Guardar.Filter = "JPEG(*.JPG)|*.JPG|bitmap(*.bmp)|*.bitmap|PNG(*.PNG)|*.PNG";
                Image Imagen = imagenMuestra.Image;
                Guardar.ShowDialog();

                Imagen.Save(Guardar.FileName);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Para guardar una imagen, primero debe abrirla.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error guardando archivo.");
            }
        }

        private void negativo_Click(object sender, EventArgs e)
        {
            try
            {
                bitmap.HacerNegativo();
                bitmap.NuevaImagen(imagenCopiada);
                imagenMuestra.ImageLocation=imagenCopiada;
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Para hacerle negativo a una imagen, primero debe abrirla.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error al hacerle negativo a esa imagen.");
            }
        }

        private void vertical_Click(object sender, EventArgs e)
        {
            try
            {
                bitmap.VoltearVertical();
                bitmap.NuevaImagen(imagenCopiada);
                imagenMuestra.ImageLocation = imagenCopiada;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Para voltear una imagen, primero debe abrirla.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error al hacerle espejo a esa imagen.");
            }
        }

        private void horizontal_Click(object sender, EventArgs e)
        {
            try
            {
                bitmap.VoltearHorizontal();
                bitmap.NuevaImagen(imagenCopiada);
                imagenMuestra.ImageLocation = imagenCopiada;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Para voltear una imagen, primero debe abrirla.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error al hacerle espejo a esa imagen.");
            }
        }

        private void rotarIzquierda_Click(object sender, EventArgs e)
        {
            try
            {
                if (grados.SelectedIndex == 0)
                    bitmap.GirarIzquierda();
                if (grados.SelectedIndex == 1)
                    bitmap.Girar180grados();
                if (grados.SelectedIndex == 2)
                    bitmap.GirarDerecha();
                bitmap.NuevaImagen(imagenCopiada);
                imagenMuestra.ImageLocation = imagenCopiada;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Para rotar una imagen, primero debe abrirla.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error rotando esa imagen.");
            }
        }

        private void rotarDerecha_Click(object sender, EventArgs e)
        {
            try
            {
                if (grados.SelectedIndex == 0)
                    bitmap.GirarDerecha();
                if (grados.SelectedIndex == 1)
                    bitmap.Girar180grados();
                if (grados.SelectedIndex == 2)
                    bitmap.GirarIzquierda();
                bitmap.NuevaImagen(imagenCopiada);
                imagenMuestra.ImageLocation = imagenCopiada;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Para rotar una imagen, primero debe abrirla.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error rotando esa imagen.");
            }
        }

    }
}
