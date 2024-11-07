using System;
using System.Collections.Generic;
using System.Linq;

namespace PartidoBasket
{
    // Interfaz para los equipos
    public interface IEquipo
    {
        void AgregarJugador(Jugador jugador);
        int ObtenerPuntajeTotal();
        void MostrarJugadores();
    }

    // Clase Jugador
    public class Jugador
    {
        private string nombre;
        private string posicion;
        private int rendimiento;

        public Jugador(string nombre, string posicion, int rendimiento)
        {
            this.nombre = nombre;
            this.posicion = posicion;
            this.rendimiento = rendimiento;
        }

        // Métodos para obtener los valores
        public string ObtenerNombre() => nombre;
        public string ObtenerPosicion() => posicion;
        public int ObtenerRendimiento() => rendimiento;
    }

    // Clase Equipo implementando la interfaz IEquipo
    public class Equipo : IEquipo
    {
        private string nombreEquipo;
        private List<Jugador> jugadores;

        public Equipo(string nombreEquipo)
        {
            this.nombreEquipo = nombreEquipo;
            this.jugadores = new List<Jugador>();
        }

        public void AgregarJugador(Jugador jugador)
        {
            if (jugadores.Count < 3)
            {
                jugadores.Add(jugador);
            }
        }

        public int ObtenerPuntajeTotal()
        {
            return jugadores.Sum(j => j.ObtenerRendimiento());
        }

        public void MostrarJugadores()
        {
            Console.WriteLine($"{nombreEquipo}:");
            foreach (var jugador in jugadores)
            {
                Console.WriteLine($"{jugador.ObtenerNombre()} - {jugador.ObtenerPosicion()} - Rendimiento: {jugador.ObtenerRendimiento()}");
            }
        }

        public string ObtenerNombre() => nombreEquipo;
    }

    // Clase Partido
    public class Partido
    {
        private List<Jugador> jugadores;
        private Equipo equipo1;
        private Equipo equipo2;

        public Partido(List<Jugador> jugadores)
        {
            this.jugadores = new List<Jugador>(jugadores);
            this.equipo1 = new Equipo("Equipo 1");
            this.equipo2 = new Equipo("Equipo 2");
        }

        public void SeleccionarJugadores()
        {
            var random = new Random();
            var jugadoresDisponibles = new List<Jugador>(jugadores);

            for (int i = 0; i < 3; i++)
            {
                // Selección aleatoria para el Equipo 1
                var jugadorSeleccionado = jugadoresDisponibles[random.Next(jugadoresDisponibles.Count)];
                equipo1.AgregarJugador(jugadorSeleccionado);
                jugadoresDisponibles.Remove(jugadorSeleccionado);

                // Selección aleatoria para el Equipo 2
                jugadorSeleccionado = jugadoresDisponibles[random.Next(jugadoresDisponibles.Count)];
                equipo2.AgregarJugador(jugadorSeleccionado);
                jugadoresDisponibles.Remove(jugadorSeleccionado);
            }
        }

        public void JugarPartido()
        {
            int puntajeEquipo1 = equipo1.ObtenerPuntajeTotal();
            int puntajeEquipo2 = equipo2.ObtenerPuntajeTotal();

            equipo1.MostrarJugadores();
            Console.WriteLine($"Puntaje total: {puntajeEquipo1}\n");

            equipo2.MostrarJugadores();
            Console.WriteLine($"Puntaje total: {puntajeEquipo2}\n");

            if (puntajeEquipo1 > puntajeEquipo2)
            {
                Console.WriteLine($"Ganador: {equipo1.ObtenerNombre()}!");
            }
            else if (puntajeEquipo1 < puntajeEquipo2)
            {
                Console.WriteLine($"Ganador: {equipo2.ObtenerNombre()}!");
            }
            else
            {
                Console.WriteLine("El partido terminó en empate.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Lista de jugadores
            List<Jugador> jugadores = new List<Jugador>
            {
                new Jugador("Juan", "Base", 8),
                new Jugador("Pedro", "Alero", 6),
                new Jugador("Carlos", "Pívot", 7),
                new Jugador("Luis", "Escolta", 9),
                new Jugador("Miguel", "Ala-Pívot", 5),
                new Jugador("Jorge", "Alero", 7)
            };

            // Crear y ejecutar el partido
            Partido partido = new Partido(jugadores);
            partido.SeleccionarJugadores();
            partido.JugarPartido();
        }
    }
}