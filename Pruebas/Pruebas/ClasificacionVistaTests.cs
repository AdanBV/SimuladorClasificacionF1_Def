using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimuladorClasificaciónF1.Modelo;
using SimuladorClasificaciónF1.Vista;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimuladorClasificaciónF1.Vista
{
    [TestClass]
    public class ClasificacionVistaTests
    {
        // Simulación de una clase Controlador con un método ObtenerPilotos simulado
        public class ControladorSimulado
        {
            public List<Piloto> ObtenerPilotos(string nombreBaseDatos)
            {
                // Simulación de datos de prueba
                return new List<Piloto>
                {
                    new Piloto { Nombre = "Lewis Hamilton", Numero = 44, Equipo = "Mercedes", Puntos = 100 },
                    new Piloto { Nombre = "Max Verstappen", Numero = 33, Equipo = "Red Bull", Puntos = 90 },
                };
            }
        }

        [TestMethod]
        public void TestBtnPiloto_Click()
        {
            // Arrange
            ClasificacionVista form = new ClasificacionVista();

            // Act
            form.btnPiloto_Click(null, null); // Llamamos directamente al evento Click del botón

            ListView listView = form.GetListView();

            // Assert
            Assert.IsTrue(listView.Items.Count >= 3);
            Assert.AreEqual("Lewis Hamilton", listView.Items[0].SubItems[1].Text);
            Assert.AreEqual("Max Verstappen", listView.Items[1].SubItems[1].Text);
        }
    }
}
