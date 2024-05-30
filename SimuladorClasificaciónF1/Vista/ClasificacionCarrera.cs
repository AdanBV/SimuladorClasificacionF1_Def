using SimuladorClasificaciónF1.Controlador;
using SimuladorClasificaciónF1.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorClasificaciónF1.Vista
{
    public partial class ClasificacionCarrera : Form
    {
        BDControlador controlador = new BDControlador();
        string nombreBaseDatos = "F1Data";
        List<Clasificacion> clasificaciones = new List<Clasificacion>();

        public ClasificacionCarrera(String nombre)
        {
            InitializeComponent();
            this.CenterToScreen();
            labNomGP.Text = nombre;
            labNomGP.TextAlign = ContentAlignment.MiddleCenter;
            labNomGP.Anchor = AnchorStyles.None;
            cargarCombobox();
        }

        private void cargarCombobox()
        {
            // Obtener la lista de pilotos desde el controlador
            List<Piloto> pilotos = controlador.ObtenerNombrePilotos(nombreBaseDatos);

            // Iterar sobre la lista de pilotos y agregar sus nombres a cada ComboBox
            foreach (Piloto piloto in pilotos)
            {
                comboBox1.Items.Add(piloto.Nombre);
                comboBox2.Items.Add(piloto.Nombre);
                comboBox3.Items.Add(piloto.Nombre);
                comboBox4.Items.Add(piloto.Nombre);
                comboBox5.Items.Add(piloto.Nombre);
                comboBox6.Items.Add(piloto.Nombre);
                comboBox7.Items.Add(piloto.Nombre);
                comboBox8.Items.Add(piloto.Nombre);
                comboBox9.Items.Add(piloto.Nombre);
                comboBox10.Items.Add(piloto.Nombre);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            // Limpiar los elementos de todos los ComboBoxes
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox9.Items.Clear();
            comboBox10.Items.Clear();

            // Cargar nuevamente los ComboBoxes con los nombres de los pilotos
            cargarCombobox();

            // Limpiar el texto de todos los ComboBoxes
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";

            // Habilitar todos los ComboBoxes
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
            comboBox6.Enabled = true;
            comboBox7.Enabled = true;
            comboBox8.Enabled = true;
            comboBox9.Enabled = true;
            comboBox10.Enabled = true;
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.Enabled = false;

            Clasificacion clasificacion = new Clasificacion();
            int id = controlador.ObtenerIdPiloto(nombreBaseDatos, comboBox.Text);

            int id_carrera = controlador.ObtenerIdCarrera(nombreBaseDatos, labNomGP.Text);

            clasificacion.idCarrera = id_carrera;
            clasificacion.idPiloto = id;

            // Asignar puntos según el índice del ComboBox
            int puntos = 0;
            switch (comboBox.Name)
            {
                case "comboBox1":
                    puntos = 25;
                    break;
                case "comboBox2":
                    puntos = 18;
                    break;
                case "comboBox3":
                    puntos = 15;
                    break;
                case "comboBox4":
                    puntos = 12;
                    break;
                case "comboBox5":
                    puntos = 10;
                    break;
                case "comboBox6":
                    puntos = 8;
                    break;
                case "comboBox7":
                    puntos = 6;
                    break;
                case "comboBox8":
                    puntos = 4;
                    break;
                case "comboBox9":
                    puntos = 2;
                    break;
                case "comboBox10":
                    puntos = 1;
                    break;
            }
            clasificacion.puntos = puntos;

            clasificaciones.Add(clasificacion);

            // Eliminar el piloto seleccionado de todos los demás ComboBoxes
            foreach (ComboBox otherComboBox in this.Controls.OfType<ComboBox>())
            {
                if (otherComboBox != comboBox)
                {
                    otherComboBox.Items.Remove(comboBox.Text);
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verificar si se han seleccionado todos los pilotos en los ComboBoxes
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "" 
                || comboBox5.Text == "" || comboBox6.Text == "" || comboBox7.Text == ""
                || comboBox8.Text == "" || comboBox9.Text == "" || comboBox10.Text == "")
            {
                // Mostrar un mensaje de error si no se han seleccionado todos los pilotos
                MessageBox.Show("No se han seleccionado todos los pilotos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Insertar las clasificaciones en la base de datos
                controlador.InsertClasificaciones(clasificaciones, nombreBaseDatos);

                // Marcar la carrera como completada en la base de datos
                controlador.MarcarCompletada(labNomGP.Text, nombreBaseDatos);

                // Cerrar el formulario actual
                this.Close();

                // Mostrar el formulario de Carreras
                Carreras carrera = new Carreras();
                carrera.ShowDialog();
            }
        }

        private void btnFin_Click(object sender, EventArgs e)
        {
            this.Close();
            Carreras carrera = new Carreras();
            carrera.ShowDialog();
        }
    }
}