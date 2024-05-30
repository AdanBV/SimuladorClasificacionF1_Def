using SimuladorClasificaciónF1.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimuladorClasificaciónF1.Controlador;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using SimuladorClasificaciónF1.Vista;

namespace SimuladorClasificaciónF1
{
    public partial class Inicio : Form
    {

        BDControlador controlador = new BDControlador();

        public Inicio()
        {
            InitializeComponent();
            CenterToScreen();
        }



        private void btnFin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            int id = 0, id2 = 0, id3 = 0, id4 = 0, id5 = 0, id6 = 0, id7 = 0, id8 = 0, id9 = 0, id10 = 0;
            string nombrePiloto , fotoPiloto;

            string nombreBaseDatos = "F1Data";

            if (!string.IsNullOrEmpty(nombreBaseDatos))
            {
                controlador.CrearBaseDatos(nombreBaseDatos);
                controlador.InsertPiloto(nombreBaseDatos);
                controlador.InsertCarreras(nombreBaseDatos);
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un nombre para la base de datos.");
            }

            Random random = new Random();

            List<int> ids = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                Boolean existe = false;
                while (!existe)
                {
                    int idRandom = random.Next(1, 21);
                    if (!ids.Contains(idRandom))
                    {
                        ids.Add(idRandom);
                        existe = true;
                    }
                }   
            }

            id = ids[0];
            id2 = ids[1];
            id3 = ids[2];
            id4 = ids[3];
            id5 = ids[4];
            id6 = ids[5];
            id7 = ids[6];
            id8 = ids[7];
            id9 = ids[8];
            id10 = ids[9];

            CargarPilotoAsync(id, nombreBaseDatos, lab1, img1);
            CargarPilotoAsync(id2, nombreBaseDatos, lab2, img2);
            CargarPilotoAsync(id3, nombreBaseDatos, lab3, img3);
            CargarPilotoAsync(id4, nombreBaseDatos, lab4, img4);
            CargarPilotoAsync(id5, nombreBaseDatos, lab5, img5);
            CargarPilotoAsync(id6, nombreBaseDatos, lab6, img6);
            CargarPilotoAsync(id7, nombreBaseDatos, lab7, img7);
            CargarPilotoAsync(id8, nombreBaseDatos, lab8, img8);
            CargarPilotoAsync(id9, nombreBaseDatos, lab9, img9);
            CargarPilotoAsync(id10, nombreBaseDatos, lab10, img10);
        }

        private async Task CargarPilotoAsync(int id, string nombreBaseDatos, Label label, PictureBox pictureBox)
        {
            string nombrePiloto = controlador.ObtenerNombrePiloto(nombreBaseDatos, id);
            string fotoPiloto = controlador.ObtenerFotoPiloto(nombreBaseDatos, id);

            label.Text = nombrePiloto;
            pictureBox.ImageLocation = fotoPiloto;
        }

        private void carrerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Carreras carreras = new Carreras();
            carreras.ShowDialog();
        }

        private void clasificaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClasificacionVista clasificacion = new ClasificacionVista();
            clasificacion.ShowDialog();
        }

        private void informaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Informacion informacion = new Informacion();
            informacion.ShowDialog();
        }

        private void organizarPilotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrganizarPilotos organizarPilotos = new OrganizarPilotos();
            organizarPilotos.ShowDialog();
        }
    }
}
