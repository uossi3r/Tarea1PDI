namespace Tarea1PDI
{
    partial class Pantalla
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pantalla));
            this.CargarImagen = new System.Windows.Forms.Button();
            this.DireccionArchivo = new System.Windows.Forms.Label();
            this.imagenMuestra = new System.Windows.Forms.PictureBox();
            this.rotacion = new System.Windows.Forms.GroupBox();
            this.grados = new System.Windows.Forms.ComboBox();
            this.rotarIzquierda = new System.Windows.Forms.Button();
            this.rotarDerecha = new System.Windows.Forms.Button();
            this.espejo = new System.Windows.Forms.GroupBox();
            this.vertical = new System.Windows.Forms.Button();
            this.horizontal = new System.Windows.Forms.Button();
            this.negativo = new System.Windows.Forms.Button();
            this.guardarImagen = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.imagenMuestra)).BeginInit();
            this.rotacion.SuspendLayout();
            this.espejo.SuspendLayout();
            this.SuspendLayout();
            // 
            // CargarImagen
            // 
            this.CargarImagen.Location = new System.Drawing.Point(12, 425);
            this.CargarImagen.Name = "CargarImagen";
            this.CargarImagen.Size = new System.Drawing.Size(90, 23);
            this.CargarImagen.TabIndex = 0;
            this.CargarImagen.Text = "Cargar Imagen";
            this.CargarImagen.UseVisualStyleBackColor = true;
            this.CargarImagen.Click += new System.EventHandler(this.CargarImagen_Click);
            // 
            // DireccionArchivo
            // 
            this.DireccionArchivo.AutoSize = true;
            this.DireccionArchivo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DireccionArchivo.Location = new System.Drawing.Point(115, 425);
            this.DireccionArchivo.MaximumSize = new System.Drawing.Size(550, 16);
            this.DireccionArchivo.MinimumSize = new System.Drawing.Size(550, 16);
            this.DireccionArchivo.Name = "DireccionArchivo";
            this.DireccionArchivo.Size = new System.Drawing.Size(550, 16);
            this.DireccionArchivo.TabIndex = 1;
            this.DireccionArchivo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imagenMuestra
            // 
            this.imagenMuestra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imagenMuestra.Location = new System.Drawing.Point(12, 12);
            this.imagenMuestra.Name = "imagenMuestra";
            this.imagenMuestra.Size = new System.Drawing.Size(760, 400);
            this.imagenMuestra.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imagenMuestra.TabIndex = 2;
            this.imagenMuestra.TabStop = false;
            // 
            // rotacion
            // 
            this.rotacion.Controls.Add(this.grados);
            this.rotacion.Controls.Add(this.rotarIzquierda);
            this.rotacion.Controls.Add(this.rotarDerecha);
            this.rotacion.Location = new System.Drawing.Point(780, 50);
            this.rotacion.Name = "rotacion";
            this.rotacion.Size = new System.Drawing.Size(90, 110);
            this.rotacion.TabIndex = 3;
            this.rotacion.TabStop = false;
            this.rotacion.Text = "Rotacion";
            // 
            // grados
            // 
            this.grados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.grados.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grados.FormattingEnabled = true;
            this.grados.Items.AddRange(new object[] {
            "90",
            "180",
            "270"});
            this.grados.Location = new System.Drawing.Point(7, 80);
            this.grados.Name = "grados";
            this.grados.Size = new System.Drawing.Size(75, 21);
            this.grados.TabIndex = 2;
            // 
            // rotarIzquierda
            // 
            this.rotarIzquierda.Location = new System.Drawing.Point(7, 50);
            this.rotarIzquierda.Name = "rotarIzquierda";
            this.rotarIzquierda.Size = new System.Drawing.Size(75, 23);
            this.rotarIzquierda.TabIndex = 1;
            this.rotarIzquierda.Text = "Izquierda";
            this.rotarIzquierda.UseVisualStyleBackColor = true;
            this.rotarIzquierda.Click += new System.EventHandler(this.rotarIzquierda_Click);
            // 
            // rotarDerecha
            // 
            this.rotarDerecha.Location = new System.Drawing.Point(7, 20);
            this.rotarDerecha.Name = "rotarDerecha";
            this.rotarDerecha.Size = new System.Drawing.Size(75, 23);
            this.rotarDerecha.TabIndex = 0;
            this.rotarDerecha.Text = "Derecha";
            this.rotarDerecha.UseVisualStyleBackColor = true;
            this.rotarDerecha.Click += new System.EventHandler(this.rotarDerecha_Click);
            // 
            // espejo
            // 
            this.espejo.Controls.Add(this.vertical);
            this.espejo.Controls.Add(this.horizontal);
            this.espejo.Location = new System.Drawing.Point(780, 200);
            this.espejo.Name = "espejo";
            this.espejo.Size = new System.Drawing.Size(90, 80);
            this.espejo.TabIndex = 4;
            this.espejo.TabStop = false;
            this.espejo.Text = "Espejo";
            // 
            // vertical
            // 
            this.vertical.Location = new System.Drawing.Point(7, 50);
            this.vertical.Name = "vertical";
            this.vertical.Size = new System.Drawing.Size(75, 23);
            this.vertical.TabIndex = 1;
            this.vertical.Text = "Vertical";
            this.vertical.UseVisualStyleBackColor = true;
            this.vertical.Click += new System.EventHandler(this.vertical_Click);
            // 
            // horizontal
            // 
            this.horizontal.Location = new System.Drawing.Point(7, 20);
            this.horizontal.Name = "horizontal";
            this.horizontal.Size = new System.Drawing.Size(75, 23);
            this.horizontal.TabIndex = 0;
            this.horizontal.Text = "Horizontal";
            this.horizontal.UseVisualStyleBackColor = true;
            this.horizontal.Click += new System.EventHandler(this.horizontal_Click);
            // 
            // negativo
            // 
            this.negativo.Location = new System.Drawing.Point(787, 350);
            this.negativo.Name = "negativo";
            this.negativo.Size = new System.Drawing.Size(75, 23);
            this.negativo.TabIndex = 5;
            this.negativo.Text = "Negativo";
            this.negativo.UseVisualStyleBackColor = true;
            this.negativo.Click += new System.EventHandler(this.negativo_Click);
            // 
            // guardarImagen
            // 
            this.guardarImagen.Location = new System.Drawing.Point(675, 425);
            this.guardarImagen.Name = "guardarImagen";
            this.guardarImagen.Size = new System.Drawing.Size(100, 23);
            this.guardarImagen.TabIndex = 6;
            this.guardarImagen.Text = "Guardar Imagen";
            this.guardarImagen.UseVisualStyleBackColor = true;
            this.guardarImagen.Click += new System.EventHandler(this.guardarImagen_Click);
            // 
            // Pantalla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(880, 457);
            this.Controls.Add(this.guardarImagen);
            this.Controls.Add(this.negativo);
            this.Controls.Add(this.espejo);
            this.Controls.Add(this.rotacion);
            this.Controls.Add(this.imagenMuestra);
            this.Controls.Add(this.DireccionArchivo);
            this.Controls.Add(this.CargarImagen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(900, 500);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "Pantalla";
            this.Text = "Tarea 1 PDI";
            ((System.ComponentModel.ISupportInitialize)(this.imagenMuestra)).EndInit();
            this.rotacion.ResumeLayout(false);
            this.espejo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CargarImagen;
        private System.Windows.Forms.Label DireccionArchivo;
        private System.Windows.Forms.PictureBox imagenMuestra;
        private System.Windows.Forms.GroupBox rotacion;
        private System.Windows.Forms.ComboBox grados;
        private System.Windows.Forms.Button rotarIzquierda;
        private System.Windows.Forms.Button rotarDerecha;
        private System.Windows.Forms.GroupBox espejo;
        private System.Windows.Forms.Button vertical;
        private System.Windows.Forms.Button horizontal;
        private System.Windows.Forms.Button negativo;
        private System.Windows.Forms.Button guardarImagen;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

