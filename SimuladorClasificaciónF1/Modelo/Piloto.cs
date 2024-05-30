using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorClasificaciónF1.Modelo
{
    public  class Piloto
    {
        public Piloto()
        {
        }

        public Piloto(string nombre, string equipo)
        {
            Nombre = nombre;
            Equipo = equipo;
        }

        public Piloto(string nombre, int numero, string equipo, string foto, int puntos, int posicion)
        {
            Nombre = nombre;
            Numero = numero;
            Equipo = equipo;
            Foto = foto;
            Puntos = puntos;
            Posicion = posicion;
        }

        public Piloto(int id, string nombre, int numero, string equipo, string foto)
        {
            Id = id;
            Nombre = nombre;
            Numero = numero;
            Equipo = equipo;
            Foto = foto;
        }

        public Piloto(string nombre, int numero, string equipo)
        {
            Nombre = nombre;
            Numero = numero;
            Equipo = equipo;
        }

        public Piloto(string nombre, int numero, string equipo, int puntos)
        {
            Nombre = nombre;
            Numero = numero;
            Equipo = equipo;
            Puntos = puntos;
        }
        public Piloto(string nombre, int numero, string equipo, string foto)
        {
            Nombre = nombre;
            Numero = numero;
            Equipo = equipo;
            Foto = foto;
        }

        public Piloto(string nombre)
        {
            Nombre = nombre;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public string Equipo { get; set; }
        public string Foto { get; set; }
        public int Puntos { get; set; }
        public int Posicion { get; set; }
    }
}
