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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace SimuladorClasificaciónF1.Vista
{
    public partial class OrganizarPilotos : Form
    {
        String nombreBaseDatos = "F1Data";
        BDControlador controlador = new BDControlador();
        public OrganizarPilotos()
        {
            InitializeComponent();
            CenterToScreen();
            cargarPilotos();

            cargarEquipos();
        }

        // Método para cargar los pilotos en el ComboBox cbxPiloto
        private void cargarPilotos()
        {
            // Obtener la lista de pilotos desde el controlador
            List<Piloto> pilotos = controlador.ObtenerNombrePilotos(nombreBaseDatos);

            // Limpiar los elementos existentes en el ComboBox antes de cargar los nuevos pilotos
            cbxPiloto.Items.Clear();

            // Recorrer la lista de pilotos y agregar cada nombre al ComboBox
            foreach (Piloto piloto in pilotos)
            {
                cbxPiloto.Items.Add(piloto.Nombre);
            }
        }

        // Método para cargar los equipos en el ComboBox cbxTeam
        private void cargarEquipos()
        {
            // Obtener la lista de nombres de equipos desde el controlador
            List<String> team = controlador.ObtenerNombreEquipos(nombreBaseDatos);

            // Limpiar los elementos existentes en el ComboBox antes de cargar los nuevos equipos
            cbxTeam.Items.Clear();

            // Recorrer la lista de nombres de equipos y agregar cada uno al ComboBox
            foreach (String equipo in team)
            {
                cbxTeam.Items.Add(equipo);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación
            DialogResult resultado = MessageBox.Show("¿Quieres continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (resultado == DialogResult.Yes)
            {
                cbxPiloto.Text = "";
                controlador.EliminarPiloto(nombreBaseDatos, cbxPiloto.SelectedItem.ToString());
                cbxPiloto.Items.Clear();
                cargarPilotos();
            } else
            {
                cbxPiloto.Text = "";
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener la cantidad actual de pilotos en la base de datos
            int count = controlador.ObtenerCantidadPilotos(nombreBaseDatos);

            // Verificar si alguno de los campos está vacío
            if (txtNombre.Text == "" || txtNum.Text == "" || cbxTeam.Text == "")
            {
                MessageBox.Show("Por favor, rellene todos los campos.");
            }
            else
            {
                // Crear un objeto Piloto con la información proporcionada por el usuario
                Piloto piloto = new Piloto(txtNombre.Text, int.Parse(txtNum.Text), cbxTeam.Text);

                // Verificar si se ha alcanzado el límite máximo de pilotos (20)
                if (count == 20)
                {
                    MessageBox.Show("Ya se han ingresado los 20 pilotos");
                }
                // Si aún no se ha alcanzado el límite de 20 pilotos
                else if (count < 20)
                {
                    // Verificar si el número del piloto ya está en uso
                    if (controlador.ObtenerNumeroLibre(nombreBaseDatos, int.Parse(txtNum.Text)))
                    {
                        // Insertar el nuevo piloto en la base de datos
                        controlador.InsertarPiloto(piloto, nombreBaseDatos);

                        // Insertar la clasificación del nuevo piloto en la base de datos
                        controlador.InsertarClasificacion(piloto, nombreBaseDatos);

                        // Limpiar los campos de entrada
                        txtNombre.Text = "";
                        txtNum.Text = "";
                        cbxTeam.Text = "";

                        // Mostrar un mensaje de éxito
                        MessageBox.Show("Piloto Añadido.");

                        // Limpiar el ComboBox de pilotos y volver a cargar la lista de pilotos
                        cbxPiloto.Items.Clear();
                        cargarPilotos();
                    }
                    else
                    {
                        MessageBox.Show("Ya hay un piloto con ese número.");
                    }
                    
                }
            }  
        }

        private void cbxTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el ComboBox que disparó el evento
            ComboBox cbx = (ComboBox)sender;

            // Obtener el nombre del equipo seleccionado en el ComboBox
            string team = cbx.SelectedItem.ToString();

            // llamo al controlador para verificar si ya hay 2 pilotos asignados a ese equipo en la base de datos
            bool completada = controlador.ObtenerEspacioTeam(nombreBaseDatos, team);

            // Si ya hay 2 pilotos asignados a ese equipo, mostrar un mensaje y limpiar la selección del ComboBox
            if (!completada)
            {
                MessageBox.Show("Ya hay 2 pilotos de este equipo.");
                cbxTeam.Text = ""; // Esto limpia la selección del ComboBox
            }
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es un número, un carácter de control (como la tecla de retroceso), o el separador decimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si no es ninguno de estos, se descarta la tecla
                e.Handled = true;
            }
        }
    }
}
