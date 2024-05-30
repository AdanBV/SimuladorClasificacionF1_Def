using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorClasificaciónF1.Modelo
{
    internal class Equipo
    {
        public Equipo()
        {
        }

        public Equipo(string nombre, string piloto1, string piloto2, int puntos)
        {
            Nombre = nombre;
            Piloto1 = piloto1;
            Piloto2 = piloto2;
            Puntos = puntos;
        }

        public String Nombre { get; set; }
        public int Puntos { get; set; }
        public int Posicion { get; set; }
        public String Piloto1{ get; set; }
        public String Piloto2 { get; set; }
    }
}
