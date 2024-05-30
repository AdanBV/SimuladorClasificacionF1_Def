namespace SimuladorClasificaciónF1.Vista
{
    partial class ClasificacionVista
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
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.labNomApp = new System.Windows.Forms.Label();
            this.btnFin = new System.Windows.Forms.Button();
            this.btnPiloto = new System.Windows.Forms.Button();
            this.btnEquipo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clasificación General";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(110, 101);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(557, 337);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // labNomApp
            // 
            this.labNomApp.AutoSize = true;
            this.labNomApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNomApp.Location = new System.Drawing.Point(110, 9);
            this.labNomApp.Name = "labNomApp";
            this.labNomApp.Size = new System.Drawing.Size(359, 25);
            this.labNomApp.TabIndex = 28;
            this.labNomApp.Text = "Simulador de Clasificación de F1";
            // 
            // btnFin
            // 
            this.btnFin.Location = new System.Drawing.Point(674, 393);
            this.btnFin.Name = "btnFin";
            this.btnFin.Size = new System.Drawing.Size(74, 45);
            this.btnFin.TabIndex = 30;
            this.btnFin.Text = "Salir";
            this.btnFin.UseVisualStyleBackColor = true;
            this.btnFin.Click += new System.EventHandler(this.btnFin_Click);
            // 
            // btnPiloto
            // 
            this.btnPiloto.Location = new System.Drawing.Point(12, 161);
            this.btnPiloto.Name = "btnPiloto";
            this.btnPiloto.Size = new System.Drawing.Size(92, 42);
            this.btnPiloto.TabIndex = 31;
            this.btnPiloto.Text = "Clasificación Pilotos";
            this.btnPiloto.UseVisualStyleBackColor = true;
            this.btnPiloto.Click += new System.EventHandler(this.btnPiloto_Click);
            // 
            // btnEquipo
            // 
            this.btnEquipo.Location = new System.Drawing.Point(12, 291);
            this.btnEquipo.Name = "btnEquipo";
            this.btnEquipo.Size = new System.Drawing.Size(92, 43);
            this.btnEquipo.TabIndex = 32;
            this.btnEquipo.Text = "Clasificación Equipos";
            this.btnEquipo.UseVisualStyleBackColor = true;
            this.btnEquipo.Click += new System.EventHandler(this.btnEquipo_Click);
            // 
            // ClasificacionVista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btnEquipo);
            this.Controls.Add(this.btnPiloto);
            this.Controls.Add(this.btnFin);
            this.Controls.Add(this.labNomApp);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Name = "ClasificacionVista";
            this.Text = "Clasificación";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labNomApp;
        private System.Windows.Forms.Button btnFin;
        private System.Windows.Forms.Button btnPiloto;
        private System.Windows.Forms.Button btnEquipo;
    }
}