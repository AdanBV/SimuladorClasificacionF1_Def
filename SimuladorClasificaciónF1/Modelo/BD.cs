using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorClasificaciónF1.Modelo
{
    class BD
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["MiCadenaConexion"].ConnectionString;

        public void CrearBaseDatos(string nombreBaseDatos)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"CREATE DATABASE {nombreBaseDatos}";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                comando.ExecuteNonQuery();
                CrearTablas(nombreBaseDatos);
                conexion.Close();
            }
        }

        public bool VerificarExistenciaBaseDatos(string nombreBaseDatos, Inicio inicio)
        {
            bool existe = false;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE name = '{nombreBaseDatos}'";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                try
                {
                    conexion.Open();
                    existe = Convert.ToInt32(comando.ExecuteScalar()) > 0;
                    conexion.Close();
                } catch (System.Data.SqlClient.SqlException e)
                {
                    MessageBox.Show("No se ha podido conectar a la base de datos.\n" +
                        "Configura una cadena de conexión correcta en el archivo SimuladorClasificaciónF1.exe.config.",
                        "Cadena Conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    inicio.Close();
                }
            }
            return existe;
        }

        public void EliminarBaseDatos(string nombreBaseDatos)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"DROP DATABASE {nombreBaseDatos}";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
        }

        public void CrearTablas(string nombreBaseDatos)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"USE {nombreBaseDatos}; " +
                    "CREATE TABLE piloto (id INT PRIMARY KEY IDENTITY(1,1), nombre NVARCHAR(100), numero INT, equipo NVARCHAR(100), foto NVARCHAR(1000))";

                string consulta2 = $"USE {nombreBaseDatos}; " +
                    "CREATE TABLE carrera (" +
                    "id INT PRIMARY KEY IDENTITY(1,1), " +
                    "nombre NVARCHAR(100), " +
                    "fecha DATE, " +
                    "completada BIT)";


                string consultaClasificacion = $"USE {nombreBaseDatos}; " +
                    "CREATE TABLE clasificacion (" +
                    "id INT PRIMARY KEY IDENTITY(1,1), " +
                    "id_Piloto INT, " +
                    "Puntos INT" +
                    ")";
                SqlCommand comandoPiloto = new SqlCommand(consulta, conexion);
                SqlCommand comandoCarrera = new SqlCommand(consulta2, conexion);
                SqlCommand comandoClasificacion = new SqlCommand(consultaClasificacion, conexion);

                conexion.Open();
                comandoPiloto.ExecuteNonQuery();
                comandoCarrera.ExecuteNonQuery();
                comandoClasificacion.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void InsertPilotos(Piloto[] pilotos, string nombreBaseDatos)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                foreach (Piloto piloto in pilotos)
                {
                    string consulta = $"USE {nombreBaseDatos}; " +
                        $"INSERT INTO piloto (nombre, numero, equipo, foto) VALUES ('{piloto.Nombre}', {piloto.Numero}, '{piloto.Equipo}', '{piloto.Foto}')";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.ExecuteNonQuery();

                    string consultaClasificacion = $"USE {nombreBaseDatos}; " +
                        $"INSERT INTO clasificacion (id_Piloto, Puntos) VALUES ((SELECT id FROM piloto WHERE nombre = '{piloto.Nombre}'), 0)";
                    SqlCommand comandoClasificacion = new SqlCommand(consultaClasificacion, conexion);
                    comandoClasificacion.ExecuteNonQuery();
                }
                conexion.Close();
            }
        }

        public void InsertCarreras(Carrera[] carreras, string nombreBaseDatos)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                foreach (Carrera carrera in carreras)
                {
                    string consulta = $"USE {nombreBaseDatos}; " +
                        $"INSERT INTO carrera (nombre, fecha, completada) VALUES ('{carrera.nombre}'," +
                        $" '{carrera.fecha.ToString("yyyy-MM-dd")}'," +
                        $" {(carrera.completada ? 1 : 0)})";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.ExecuteNonQuery();
                }
                conexion.Close();
            }
        }

        public string ObtenerNombreById(int id, string nombreBaseDatos)
        {
            String nom = "";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"USE {nombreBaseDatos}; SELECT Nombre FROM piloto WHERE id = {id}";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    nom = reader.GetString(0);
                }
                reader.Close();
                conexion.Close();
            }

            return nom;
        }

        public string ObtenerFotoById(int id, string nombreBaseDatos)
        {
            String foto = "";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"USE {nombreBaseDatos}; SELECT Foto FROM piloto WHERE id = {id}";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    foto = reader.GetString(0);
                }
                reader.Close();
                conexion.Close();
            }

            return foto;
        }

        public List<Piloto> ObtenerPilotos(string nombreBaseDatos)
        {
            List<Piloto> pilotos = new List<Piloto>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"SELECT p.Nombre, p.Numero, p.Equipo, c.Puntos " +
                               $"FROM piloto p JOIN clasificacion c ON p.Id = c.id_piloto " +
                               $"ORDER BY c.Puntos DESC;";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        string nombre = reader[0].ToString();
                        int numero = int.Parse(reader[1].ToString());
                        string equipo = reader[2].ToString();
                        int puntos = int.Parse(reader[3].ToString());
                        Piloto piloto = new Piloto(nombre, numero, equipo, puntos);
                        pilotos.Add(piloto);
                    }

                    reader.Close();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return pilotos;
        }

        public int ObtenerIdPiloto(string nombreBaseDatos, string nombre)
        {
            int id;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"USE {nombreBaseDatos} SELECT id FROM piloto WHERE nombre = '{nombre}'";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                reader.Close();
                conexion.Close();
            }

            return id;
        }

        public int ObtenerIdCarrera(string nombreBaseDatos, string nombre)
        {
            int id;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = $"USE {nombreBaseDatos}; SELECT id FROM carrera WHERE nombre = '{nombre}'";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                reader.Close();
                conexion.Close();
            }

            return id;
        }

        public void InsertClasificaciones(List<Clasificacion> clasificaciones, string nombreBaseDatos)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                foreach (Clasificacion clasificacion in clasificaciones)
                {
                    string consulta = $"USE {nombreBaseDatos}; " +
                                      $"UPDATE clasificacion SET Puntos = Puntos + {clasificacion.puntos} WHERE id_Piloto = {clasificacion.idPiloto}";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.ExecuteNonQuery();
                }
                conexion.Close();
            }
        }

        public List<Piloto> ObtenerNombrePilotos(string nombreBaseDatos)
        {
            List<Piloto> pilotos = new List<Piloto>();

            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"SELECT Nombre " +
                               $"FROM piloto";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nombre = reader[0].ToString();
                        Piloto piloto = new Piloto(nombre);
                        pilotos.Add(piloto);
                    }

                    reader.Close();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }   
            return pilotos;
        }

        internal void EliminarPiloto(string nombreBaseDatos, string v)
        {
            int id = ObtenerIdPiloto(nombreBaseDatos, v);
            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"DELETE FROM piloto " +
                               $"WHERE id = {id}";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        internal int ObtenerCantidadPilotos(string nombreBaseDatos)
        {
            int count = 0;

            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"SELECT COUNT(*) " +
                               $"FROM piloto";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    count = (int)command.ExecuteScalar();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return count;
        }

        internal void InsertarPiloto(Piloto piloto, string nombreBaseDatos)
        {
            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"INSERT INTO piloto (Nombre, Numero, Equipo, Foto) " +
                               $"VALUES ('{piloto.Nombre}', {piloto.Numero}, '{piloto.Equipo}', '{piloto.Foto}')";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        internal bool ObtenerCompletada(string nombreBD, string text)
        {
            bool completada = false;

            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBD} " +
                               $"SELECT Completada " +
                               $"FROM carrera " +
                               $"WHERE Nombre = '{text}'";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    completada = (bool)command.ExecuteScalar();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return completada;
        }

        internal void MarcarCompletada(string text, string nombreBaseDatos)
        {
            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"UPDATE carrera " +
                               $"SET Completada = 1 " +
                               $"WHERE Nombre = '{text}'";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        internal List<string> ObtenerNombreEquipos(string nombreBaseDatos)
        {
            List<string> equipos = new List<string>();
            
            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"SELECT equipo " +
                               $"FROM piloto " +
                               $"GROUP BY equipo";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string equipo = reader[0].ToString();
                        equipos.Add(equipo);
                    }

                    reader.Close();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }   
            }

            return equipos;
        }

        internal bool ObtenerEspacioTeam(string nombreBaseDatos, string team)
        {
            bool completada = false;

            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"SELECT COUNT(*) " +
                               $"FROM piloto " +
                               $"WHERE equipo = '{team}'";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    completada = (int)command.ExecuteScalar() < 2;
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return completada;
        }

        internal bool ObtenerNumeroLibre(string nombreBaseDatos, int v)
        {
            bool completada = false;

            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"SELECT COUNT(*) " +
                               $"FROM piloto " +
                               $"WHERE numero = {v}";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    completada = (int)command.ExecuteScalar() == 0;
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return completada;
        }

        internal void InsertarClasificacion(Piloto piloto, string nombreBaseDatos)
        {
            using(SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                               $"INSERT INTO clasificacion (id_Piloto, Puntos) " +
                               $"VALUES ((SELECT id FROM piloto WHERE nombre = '{piloto.Nombre}'), 0)";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public List<Equipo> ObtenerEquipo(string nombreBaseDatos)
        {
            List<Equipo> equipos = new List<Equipo>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = $"USE {nombreBaseDatos} " +
                              "SELECT p1.equipo, " +
                              "MIN(p1.nombre) AS piloto1, " +
                              "MAX(p2.nombre) AS piloto2, " +
                              "SUM(c.puntos) AS puntos_totales " +
                              "FROM piloto p1 " +
                              "JOIN piloto p2 ON p1.equipo = p2.equipo " +
                              "AND p1.id < p2.id " +
                              "JOIN clasificacion c ON p1.id = c.id_piloto " +
                              "OR p2.id = c.id_piloto " +
                              "GROUP BY p1.equipo " +
                              "ORDER BY puntos_totales DESC;";

                SqlCommand command = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        string nombre = reader[0].ToString();
                        string piloto1 = reader[1].ToString();
                        string piloto2 = reader[2].ToString();
                        int puntos = int.Parse(reader[3].ToString());
                        Equipo equipo= new Equipo(nombre, piloto1, piloto2, puntos);
                        equipos.Add(equipo);
                    }

                    reader.Close();
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return equipos;
        }
    }
}
