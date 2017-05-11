using System;
using System.IO;

namespace P4 {
	public class MainClass {
		struct Jugador{
			public string nombre;
			public int nivel;
		}

		public static void Main (string[] args) {

			Console.WriteLine("Bienvenido a PACMAN\nPulsa c para cargar una partida\npulsa C para cargar tus logros\npulsa n para empezar una nueva partida\npulsa r para ver la lista de jugadores\n cualquier otra tecla para salir");
			char tecla = Console.ReadKey (true).KeyChar;

			Tablero t;
			if (tecla == 'c') {
				Console.Write ("Introduce el nombre de tu partida: ");
				string partida = Console.ReadLine ();
				Console.Clear ();
				t = new Tablero (partida);
			} else if (tecla == 'C') {
				Console.Write ("Introduce tu nombre de jugador: ");
				Jugador player;
				string playername = Console.ReadLine ();
				if (buscarJugador (playername, out player)) {
					Console.WriteLine ("encontrado");
				} else { 
					Console.WriteLine ("creado nuevo jugador: " + playername);
				}
				Console.WriteLine ("pulsa enter para cargar el nivel" + player.nivel);
				Console.ReadLine ();
				Console.Clear ();
				t = new Tablero ("level0" + player.nivel +".dat");
			} else if (tecla == 'n') {
				Console.Clear ();
				t = new Tablero ("level00.dat");
			} else if (tecla == 'r') {
				mostrarJugadores ();
				return;
			} else {
				return;
			}

			t.Dibuja();
			int lap = 200; // retardo para bucle ppal
			char c = ' ';
			bool capture = false;
			while (!capture)
			{
				bool salir = false;
				while (!t.finNivel()&& !capture && !salir)
				{
					leeInput(ref c);
					if(c == 'g'){
						Console.Write ("Escribe el nombre de tu partida para guardar: ");
						string partida = Console.ReadLine ();
						t.guardar (partida);
						c = ' ';
						Console.Write ("Escribe tu nombre:");
					}
					if (c == 'p') {
						Console.WriteLine ("Pausa");
						Console.ReadLine ();
						c = ' ';
					} if (c == 'q') {
						salir = true;
						c = ' ';
					}
					if (c != ' ' && t.cambiaDir(c)) c = ' ';
					t.muevePacman();
					capture = t.captura();
					t.mueveFantasmas(lap);
					if (!capture)
						capture = t.captura();
					t.Dibuja();
					System.Threading.Thread.Sleep(lap);
				}
				if (!capture)
				{
					Console.Clear();
					Console.WriteLine("Has completado el nivel");
					Console.ReadKey();
					Tablero t2 = new Tablero("level0" + (t.numNivel + 1) + ".dat");
					t = t2;
				}
			}
			Console.Clear();
			Console.WriteLine("FIN DE LA PARTIDA");
			Console.ReadKey();
		}

		static bool buscarJugador(string playername, out Jugador jugador){
			jugador = new Jugador (){nombre = "", nivel=0 };
			StreamReader sr = new StreamReader ("jugadores.txt");
			bool encontrado = false;
			while (!sr.EndOfStream) {
				if (sr.ReadLine () == playername) {
					jugador.nombre = playername;
					jugador.nivel = int.Parse (sr.ReadLine ());
					encontrado = true;
				}
			}
			sr.Close ();

			return encontrado;
		}
		static void mostrarJugadores(){
			StreamReader sr = new StreamReader ("jugadores.txt");
			Console.Clear ();
			Console.WriteLine ("=========JUGADORES=======");
			while (!sr.EndOfStream) {
				Console.WriteLine (sr.ReadLine() + " ,nivel: " + sr.ReadLine());
			}
			sr.Close ();
		}

		public static void leeInput (ref char dir) {
			if (Console.KeyAvailable) {
				switch (Console.ReadKey (true).Key.ToString ()) {
				case "RightArrow":
					dir = 'r';
					break;
				case "LeftArrow":
					dir = 'l';
					break;
				case "UpArrow":
					dir = 'u';
					break;
				case "DownArrow":
					dir = 'd';
					break;
				case "G":
					dir = 'g';
					break;
				case "P":
					dir = 'p';
					break;
				case "Q":
					dir = 'q';
					break;
				default:
					break;
				}
			}
		}
	}
}

