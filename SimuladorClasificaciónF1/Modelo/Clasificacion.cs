using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorClasificaciónF1.Modelo
{
    public class Clasificacion
    {
        public int Id { get; set; }
        public int idCarrera { get; set; }
        public int idPiloto { get; set; }
        public int posicion { get; set; }
        public int puntos { get; set; }
    }
}
