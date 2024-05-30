using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuladorClasificaciónF1.Modelo;

namespace SimuladorClasificaciónF1.Controlador
{
    class BDControlador
    {
        private BD modelo = new BD();

        public void CrearBaseDatos(string nombreBaseDatos)
        {
            if (modelo.VerificarExistenciaBaseDatos(nombreBaseDatos))
            {
                modelo.EliminarBaseDatos(nombreBaseDatos);

                modelo.CrearBaseDatos(nombreBaseDatos);
            }
            else
            {
                modelo.CrearBaseDatos(nombreBaseDatos);
            }
        }

        public void InsertPiloto(string nombreBaseDatos)
        {
            Piloto[] pilotos = new Piloto[20];

            pilotos[0] = new Piloto(1, "Lewis Hamilton", 44, "Mercedes", @"..\..\Images\Lewis_Hamilton.jpeg");
            pilotos[1] = new Piloto(2, "George Russell", 63, "Mercedes", @"..\..\Images\george-russell.jpeg");
            pilotos[2] = new Piloto(3, "Max Verstappen", 1, "Red Bull Racing", @"..\..\Images\max-verstappen.jpeg");
            pilotos[3] = new Piloto(4, "Sergio Pérez", 11, "Red Bull Racing", @"..\..\Images\sergio-perez.jpeg");
            pilotos[4] = new Piloto(5, "Charles Leclerc", 16, "Ferrari", @"..\..\Images\charles-leclerc.jpeg");
            pilotos[5] = new Piloto(6, "Carlos Sainz", 55, "Ferrari", @"..\..\Images\carlos-sainz.jpeg");
            pilotos[6] = new Piloto(7, "Lando Norris", 4, "McLaren", @"..\..\Images\lando-norris.jpeg");
            pilotos[7] = new Piloto(8, "Oscar Piastri", 81, "McLaren", @"..\..\Images\oscar-piastri.jpeg");
            pilotos[8] = new Piloto(9, "Pierre Gasly", 10, "Alpine", @"..\..\Images\pierre-gasly.jpeg");
            pilotos[9] = new Piloto(10, "Esteban Ocon", 31, "Alpine", @"..\..\Images\esteban-ocon.jpeg");
            pilotos[10] = new Piloto(11, "Fernando Alonso", 14, "Aston Martin", @"..\..\Images\fernando-alonso.jpeg");
            pilotos[11] = new Piloto(12, "Lance Stroll", 18, "Aston Martin", @"..\..\Images\lance-stroll.jpeg");
            pilotos[12] = new Piloto(13, "Logan Sargeant", 2, "Williams", @"..\..\Images\logan-sargeant.jpeg");
            pilotos[13] = new Piloto(14, "Alex Albon", 23, "Williams", @"..\..\Images\alex-albon.jpeg");
            pilotos[14] = new Piloto(15, "Nico Hulkenberg", 27, "Haas", @"..\..\Images\nico-hulkenberg.jpeg");
            pilotos[15] = new Piloto(16, "Kevin Magnussen", 20, "Haas", @"..\..\Images\kevin-magnussen.jpeg");
            pilotos[16] = new Piloto(17, "Valtteri Bottas", 77, "Sauber F1 Team", @"..\..\Images\valtteri-bottas.jpeg");
            pilotos[17] = new Piloto(18, "Zhou Guanyu", 24, "Sauber F1 Team", @"..\..\Images\zhou-guanyu.jpeg");
            pilotos[18] = new Piloto(19, "Daniel Ricciardo", 3, "Visa CashApp RB", @"..\..\Images\daniel-ricciardo.jpeg");
            pilotos[19] = new Piloto(20, "Yuki Tsunoda", 22, "Visa CashApp RB", @"..\..\Images\yuki-tsunoda.jpeg");

            modelo.InsertPilotos(pilotos, nombreBaseDatos);
        }

        public void InsertCarreras(string nombrenombreBaseDatos)
        {
            Carrera[] carreras = new Carrera[]
            {
                new Carrera(1, "Bahrain Grand Prix", new DateTime(2024, 3, 2), false),
                new Carrera(2, "Saudi Arabian Grand Prix", new DateTime(2024, 3, 9), false),
                new Carrera(3, "Australian Grand Prix", new DateTime(2024, 3, 24), false),
                new Carrera(4, "Japan Grand Prix", new DateTime(2024, 4, 7), false),
                new Carrera(5, "China Grand Prix", new DateTime(2024, 4, 21), false),
                new Carrera(6, "Miami Grand Prix", new DateTime(2024, 5, 5), false),
                new Carrera(7, "Emilia Romagna Grand Prix", new DateTime(2024, 5, 19), false),
                new Carrera(8, "Monaco Grand Prix", new DateTime(2024, 5, 26), false),
                new Carrera(9, "Canada Grand Prix", new DateTime(2024, 6, 9), false),
                new Carrera(10, "Spain Grand Prix", new DateTime(2024, 6, 23), false),
                new Carrera(11, "Austria Grand Prix", new DateTime(2024, 6, 30), false),
                new Carrera(12, "British Grand Prix", new DateTime(2024, 7, 7), false),
                new Carrera(13, "Hungary Grand Prix", new DateTime(2024, 7, 21), false),
                new Carrera(14, "Belgian Grand Prix", new DateTime(2024, 7, 28), false),
                new Carrera(15, "Dutch Grand Prix", new DateTime(2024, 8, 25), false),
                new Carrera(16, "Italian Grand Prix", new DateTime(2024, 9, 1), false),
                new Carrera(17, "Azerbaijan Grand Prix", new DateTime(2024, 9, 15), false),
                new Carrera(18, "Singapore Grand Prix", new DateTime(2024, 9, 22), false),
                new Carrera(19, "United States Grand Prix", new DateTime(2024, 10, 20), false),
                new Carrera(20, "Mexico Grand Prix", new DateTime(2024, 10, 27), false),
                new Carrera(21, "Brazil Grand Prix", new DateTime(2024, 11, 3), false),
                new Carrera(22, "Las Vegas Grand Prix", new DateTime(2024, 11, 24), false),
                new Carrera(23, "Qatar Grand Prix", new DateTime(2024, 12, 1), false),
                new Carrera(24, "Abu Dhabi Grand Prix", new DateTime(2024, 12, 8), false)
            };

            modelo.InsertCarreras(carreras, nombrenombreBaseDatos);
        }

        public string ObtenerNombrePiloto(string nombreBaseDatos, int index)
        {
            return modelo.ObtenerNombreById(index, nombreBaseDatos);
        }

        public string ObtenerFotoPiloto(string nombreBaseDatos, int index)
        {
            return modelo.ObtenerFotoById(index, nombreBaseDatos);
        }

        public List<Piloto> ObtenerPilotos(string nombreBaseDatos)
        {
            return modelo.ObtenerPilotos(nombreBaseDatos);
        }

        public int ObtenerIdPiloto(string nombreBaseDatos, string nombre)
        {
            return modelo.ObtenerIdPiloto(nombreBaseDatos, nombre);
        }

        public int ObtenerIdCarrera(string nombreBaseDatos, string text)
        {
            return modelo.ObtenerIdCarrera(nombreBaseDatos, text);
        }

        public void InsertClasificaciones(List<Clasificacion> clasificaciones, string nombreBaseDatos)
        {
            modelo.InsertClasificaciones(clasificaciones, nombreBaseDatos);
        }

        public List<Piloto> ObtenerNombrePilotos(string nombreBaseDatos)
        {
            return modelo.ObtenerNombrePilotos(nombreBaseDatos);
        }

        internal void EliminarPiloto(string nombreBaseDatos, string v)
        {
            modelo.EliminarPiloto(nombreBaseDatos, v);
        }

        internal int ObtenerCantidadPilotos(string nombreBaseDatos)
        {
            return modelo.ObtenerCantidadPilotos(nombreBaseDatos);
        }

        internal void InsertarPiloto(Piloto piloto, string nombreBaseDatos)
        {
            modelo.InsertarPiloto(piloto, nombreBaseDatos);
        }

        internal bool ObtenerCompletada(string nombreBD, string text)
        {
            return modelo.ObtenerCompletada(nombreBD, text);
        }

        internal void MarcarCompletada(string text, string nombreBaseDatos)
        {
            modelo.MarcarCompletada(text, nombreBaseDatos);
        }

        internal List<string> ObtenerNombreEquipos(string nombreBaseDatos)
        {
            return modelo.ObtenerNombreEquipos(nombreBaseDatos);
        }

        internal bool ObtenerEspacioTeam(string nombreBaseDatos, string team)
        {
            return modelo.ObtenerEspacioTeam(nombreBaseDatos, team);
        }

        internal bool ObtenerNumeroLibre(string nombreBaseDatos, int v)
        {
            return modelo.ObtenerNumeroLibre(nombreBaseDatos, v);
        }

        internal void InsertarClasificacion(Piloto piloto, string nombreBaseDatos)
        {
            modelo.InsertarClasificacion(piloto, nombreBaseDatos);
        }

        internal List<Equipo> ObtenerEquipo(string nombreBaseDatos)
        {
            return modelo.ObtenerEquipo(nombreBaseDatos);
        }
    }
}