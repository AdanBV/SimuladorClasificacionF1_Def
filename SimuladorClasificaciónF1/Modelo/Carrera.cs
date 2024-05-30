using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorClasificaciónF1.Modelo
{
    public class Carrera
    {
        public Carrera(int id, string nombre, DateTime fecha, bool completada)
        {
            this.id = id;
            this.nombre = nombre;
            this.fecha = fecha;
            this.completada = completada;
        }
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public bool completada { get; set; }
    }
}
