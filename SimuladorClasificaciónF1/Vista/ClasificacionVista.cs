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
    public partial class ClasificacionVista : Form
    {
        BDControlador controlador = new BDControlador();
        string nombreBaseDatos = "F1Data";

        public ClasificacionVista()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnFin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnPiloto_Click(object sender, EventArgs e)
        {
            // Limpiar el contenido actual del ListView
            listView1.Clear();

            // Establecer la vista del ListView en modo Detalles para mostrar los datos en columnas
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            // Agregar las columnas al ListView
            listView1.Columns.Add("Posición", 70);
            listView1.Columns.Add("Nombre", 150);
            listView1.Columns.Add("Numero", 50);
            listView1.Columns.Add("Equipo", 100);
            listView1.Columns.Add("Puntos", 50);
            listView1.Columns.Add("Diferencia", 50);

            // Obtener la lista de pilotos desde el controlador
            List<Piloto> pilotos = controlador.ObtenerPilotos(nombreBaseDatos);

            // Iterar sobre la lista de pilotos
            for (int i = 0; i < pilotos.Count; i++)
            {
                // Obtener el primer piloto en la lista
                Piloto primer = pilotos[0];

                // Obtener el piloto actual en la iteración
                Piloto piloto = pilotos[i];

                // Calcular la posición del piloto en la lista (empezando desde 1)
                int pos = i + 1;

                // Calcular la diferencia de puntos del piloto actual con respecto al primer piloto en la lista
                int diff = piloto.Puntos - primer.Puntos;

                // Crear un nuevo elemento ListViewItem para representar al piloto
                ListViewItem item = new ListViewItem(pos.ToString());
                item.SubItems.Add(piloto.Nombre);
                item.SubItems.Add(piloto.Numero.ToString());
                item.SubItems.Add(piloto.Equipo);
                item.SubItems.Add(piloto.Puntos.ToString());
                item.SubItems.Add(diff.ToString());

                // Agregar el elemento ListViewItem al ListView
                listView1.Items.Add(item);
            }
        }

        private void btnEquipo_Click(object sender, EventArgs e)
        {
            // Limpiar el contenido actual del ListView
            listView1.Clear();

            // Establecer la vista del ListView en modo Detalles para mostrar los datos en columnas
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            // Agregar las columnas al ListView
            listView1.Columns.Add("Posición", 70);
            listView1.Columns.Add("Nombre", 150);
            listView1.Columns.Add("Piloto 1", 110);
            listView1.Columns.Add("Piloto 2", 110);
            listView1.Columns.Add("Puntos", 50);
            listView1.Columns.Add("Diferencia", 50);

            // Obtener la lista de equipos desde el controlador
            List<Equipo> equipos = controlador.ObtenerEquipo(nombreBaseDatos);

            // Iterar sobre la lista de equipos
            for (int i = 0; i < equipos.Count; i++)
            {
                // Obtener el primer equipo en la lista
                Equipo primer = equipos[0];

                // Obtener el equipo actual en la iteración
                Equipo equipo = equipos[i];

                // Calcular la posición del equipo en la lista (empezando desde 1)
                int pos = i + 1;

                // Calcular la diferencia de puntos del equipo actual con respecto al primer equipo en la lista
                int diff = equipo.Puntos - primer.Puntos;

                // Crear un nuevo elemento ListViewItem para representar al equipo
                ListViewItem item = new ListViewItem(pos.ToString());
                item.SubItems.Add(equipo.Nombre);
                item.SubItems.Add(equipo.Piloto1);
                item.SubItems.Add(equipo.Piloto2);
                item.SubItems.Add(equipo.Puntos.ToString());
                item.SubItems.Add(diff.ToString());

                // Agregar el elemento ListViewItem al ListView
                listView1.Items.Add(item);
            }

        }

        internal void btnPiloto_Click(object value1, object value2)
        {
            throw new NotImplementedException();
        }
    }
}
