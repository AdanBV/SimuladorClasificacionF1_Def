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

namespace SimuladorClasificaciónF1.Vista
{
    public partial class Carreras : Form
    {
        BDControlador controlador = new BDControlador();
        String nombreBD = "F1Data";
        public Carreras()
        {
            InitializeComponent();
            CenterToScreen();
            ComprobarCarreras();
        }

        private void ComprobarCarreras()
        {
            // Crear un array de botones para poder iterar sobre ellos
            Button[] botonesCarrera = { btnGp1, btnGP2, btnGP3, btnGP4, btnGP5, btnGP6, btnGP7, btnGP8, btnGP9,
                                btnGP10, btnGP11, btnGP12, btnGP13, btnGP14, btnGP15, btnGP16, btnGP17,
                                btnGP18, btnGP19, btnGP20, btnGP21, btnGP22, btnGP23, btnGP24 };

            // Iterar sobre cada botón y comprobar si la carrera asociada ha sido completada
            foreach (Button botonCarrera in botonesCarrera)
            {
                if (controlador.ObtenerCompletada(nombreBD, botonCarrera.Text))
                {
                    botonCarrera.Enabled = false;
                }
            }
        }


        private void btnGp_Click(object sender, EventArgs e)
        {
            // Obtener el botón que disparó el evento
            Button btn = (Button)sender;

            // Cerrar el formulario actual
            this.Close();

            // Crear una nueva instancia del formulario ClasificacionCarrera, pasando el texto del botón como argumento
            ClasificacionCarrera clasificacionCarrera = new ClasificacionCarrera(btn.Text);

            // Mostrar el formulario ClasificacionCarrera como un diálogo
            clasificacionCarrera.ShowDialog();
        }

        private void btnFin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
