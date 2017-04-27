//Lluís Cañellas Salesa
//Mario Daniel Gallardo Cruzado
using System;
using System.IO;

namespace P3 {
	class App {

		struct Casilla {
			public bool mina;
			/* o no destapada
			 * m marcada
			 * * mina
			 * 0 - 9 num de minas alrededor */
			public char estado;
		}

		struct Tablero {
			public Casilla[,] cas;
			public int posX, posY;
		}

		static Tablero creaTablero (int fils, int cols, int numMinas) {
			Random rand = new Random ();
			Casilla[,] casilleros = new Casilla[fils, cols];
			for (int a = 0; a < fils; a++) {
				for (int b = 0; b < cols; b++) {
					casilleros [a, b].estado = 'o';
				}
			}

			Tablero tab = new Tablero { cas = casilleros, posX = 0, posY = 0 };

			int fila, columna, i = 0;
			while (i < numMinas) {
				fila = rand.Next (0, fils);
				columna = rand.Next (0, cols);
				if (!tab.cas [fila, columna].mina) {
					tab.cas [fila, columna].mina = true;
					i++;
				}
			}
			return tab;
		}

		static void Dibuja (ref Tablero tab, bool bomba) {
			for (int i = 0; i < tab.cas.GetLength (0); i++) {
				for (int j = 0; j < tab.cas.GetLength (1); j++) {
					Console.ResetColor ();
					Console.SetCursorPosition (j, i);
					bool cursor = (i == tab.posX && j == tab.posY);
					switch (tab.cas [i, j].estado) {
					case '*':
						Console.ForegroundColor = ConsoleColor.Red;
						if (cursor)	ColoresCursor ();
						Console.Write ("x");
						break;
					case 'X':
						Console.ForegroundColor = ConsoleColor.Red;
						if (cursor)	ColoresCursor ();
						Console.Write ("x");
						break;
					case 'o':
						if (tab.cas [i, j].mina && bomba) {
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write ("x");
						} else {
							Console.ForegroundColor = ConsoleColor.White;
							if (cursor)	ColoresCursor ();
							Console.Write ("o");
						}
						break;
					case 'm':
						Console.BackgroundColor = ConsoleColor.Yellow;
						Console.ForegroundColor = ConsoleColor.White;
						if (cursor)	ColoresCursor ();
						Console.Write ("*");
						break;
					case '0':
						Console.ForegroundColor = ConsoleColor.Blue;
						if (cursor)	ColoresCursor ();
						Console.Write (" ");
						break;
					case '1':
						Console.ForegroundColor = ConsoleColor.Blue;
						if (cursor)	ColoresCursor ();
						Console.Write ("1");
						break;
					case '2':
						Console.ForegroundColor = ConsoleColor.Green;
						if (cursor)	ColoresCursor ();
						Console.Write ("2");
						break;
					case '3':
						Console.ForegroundColor = ConsoleColor.Red;
						if (cursor)	ColoresCursor ();
						Console.Write ("3");
						break;
					case '4':
						Console.ForegroundColor = ConsoleColor.DarkBlue;
						if (cursor)	ColoresCursor ();
						Console.Write ("4");
						break;
					case '5':
						Console.ForegroundColor = ConsoleColor.DarkRed;
						if (cursor)	ColoresCursor ();
						Console.Write ("5");
						break;
					case '6':
						Console.ForegroundColor = ConsoleColor.Cyan;
						if (cursor)	ColoresCursor ();
						Console.Write ("6");
						break;
					case '7':
						Console.ForegroundColor = ConsoleColor.DarkGreen;
						if (cursor)	ColoresCursor ();
						Console.Write ("7");
						break;
					case '8':
						Console.ForegroundColor = ConsoleColor.Magenta;
						if (cursor)	ColoresCursor ();
						Console.Write ("8");
						break;
					default:
						break;
					}

				}
			}
			if (bomba) {
				Console.SetCursorPosition (tab.posY, tab.posX);
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.BackgroundColor = ConsoleColor.Red;
				Console.Write ("*");
				Console.SetCursorPosition (0, tab.cas.GetLength (1) + 1);
				Console.WriteLine ("\n¡Has perdido!");
				Console.Read ();
			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;
		}


		static void DescubreAdyacentes (ref Tablero tab, int x, int y) {
			int fils = tab.cas.GetLength (0);
			int cols = tab.cas.GetLength (1);
			int visitado = 0;
			int pend = 0;
			int[,] pendientes = new int[fils * cols, 2];
			pendientes [0, 0] = x;
			pendientes [0, 1] = y;
			while (visitado <= pend) {

				int numAdyacentes = 0;
				for (int i = Math.Max (pendientes [visitado, 0] - 1, 0); i < Math.Min (fils, pendientes [visitado, 0] + 2); i++) {
					for (int j = Math.Max (pendientes [visitado, 1] - 1, 0); j < Math.Min (cols, pendientes [visitado, 1] + 2); j++) {
						if (tab.cas [i, j].mina)
							numAdyacentes++;
					}
				}

				tab.cas [pendientes [visitado, 0], pendientes [visitado, 1]].estado = char.Parse ("" + numAdyacentes + "");

				for (int i = Math.Max (pendientes [visitado, 0] - 1, 0); i < Math.Min (fils, pendientes [visitado, 0] + 2) && numAdyacentes == 0; i++) {
					for (int j = Math.Max (pendientes [visitado, 1] - 1, 0); j < Math.Min (cols, pendientes [visitado, 1] + 2); j++) {
						Casilla casilla = tab.cas [i, j];
						if (casilla.estado == 'o') {
							int a = 0;
							bool repet = false;
							while (!repet && a <= pend) {
								if (i == pendientes [a, 0] && j == pendientes [a, 1])
									repet = true;
								a++;
							}
							if (!repet) {
								pend++;
								pendientes [pend, 0] = i;
								pendientes [pend, 1] = j;
							}
						}
					}
				}
				visitado++;
			}
		}

		static bool ClickCasilla (ref Tablero tab) {
			Casilla cas = tab.cas [tab.posX, tab.posY];
			if (!cas.mina) {
				DescubreAdyacentes (ref tab, tab.posX, tab.posY);
			} else {
				tab.cas [tab.posX, tab.posY].estado = '*';
			}
			return cas.mina;
		}

		static char LeeInput () {
			char letra = ' ';
			if (Console.KeyAvailable) {
				switch (Console.ReadKey (true).Key.ToString ()) {
				case "RightArrow":
					letra = 'r';
					break;
				case "LeftArrow":
					letra = 'l';
					break;
				case "UpArrow":
					letra = 'u';
					break;
				case "DownArrow":
					letra = 'd';
					break;
				case "Spacebar":
					letra = 'c';
					break;
				case "Escape":
					letra = 'q';
					break;
				case "Enter":
					letra = 'x';
					break;
				default:
					break;
				}

			}
			return letra;
		}

		static bool ProcesaInput (ref Tablero tab, char c) {
			bool perder = false;
			if (c == 'x') {
				if (tab.cas [tab.posX, tab.posY].estado == 'm') {
					tab.cas [tab.posX, tab.posY].estado = 'o';
				} else {
					tab.cas [tab.posX, tab.posY].estado = 'm';
				}
			} else if (c == 'c') {
				perder = ClickCasilla (ref tab);
			} else if (c == 'l') {
				tab.posY = Math.Max (0, tab.posY - 1);
			} else if (c == 'r') {
				tab.posY = Math.Min (tab.cas.GetLength (1) - 1, tab.posY + 1);
			} else if (c == 'u') {
				tab.posX = Math.Max (0, tab.posX - 1);
			} else if (c == 'd') {
				tab.posX = Math.Min (tab.cas.GetLength (0) - 1, tab.posX + 1);
			}
			return perder;
		}

		static void GuardarEstado (Tablero tab) {
			StreamWriter guard = new StreamWriter ("guardar.txt");
			guard.WriteLine (tab.cas.GetLength (0));
			guard.WriteLine (tab.cas.GetLength (1));
			for (int i = 0; i < tab.cas.GetLength (0); i++) {
				for (int j = 0; j < tab.cas.GetLength (1); j++) {
					guard.Write (tab.cas [i, j].estado);
				}
				guard.WriteLine ();
			}
			Random rnd = new Random ();
			for (int i = 0; i < tab.cas.GetLength (0); i++) {
				for (int j = 0; j < tab.cas.GetLength (1); j++) {
					if (tab.cas [i, j].mina) {
						guard.Write ('b');
					} else
						guard.Write ((char)rnd.Next (99, 123));
				}
			}
			guard.Close ();
		}

		static Tablero CargarEstado () {
			StreamReader leer = new StreamReader ("guardar.txt");
			int fils = int.Parse (leer.ReadLine ());
			int cols = int.Parse (leer.ReadLine ());
			Casilla[,] casilleros = new Casilla[fils, cols];
			for (int a = 0; a < fils; a++) {
				for (int b = 0; b < cols; b++) {
					casilleros [a, b].estado = (char)leer.Read ();
				}
				leer.ReadLine ();
			}
			for (int a = 0; a < fils; a++) {
				for (int b = 0; b < cols; b++) {
					if ((char)leer.Read () == 'b')
						casilleros [a, b].mina = true;
				}
			}
			leer.Close ();
			Tablero tab = new Tablero { cas = casilleros, posX = 0, posY = 0 };
			return tab;
		}

		static bool Terminado (ref Tablero tab) {
			bool fin = true;
			int i = 0, j;
			int l = tab.cas.GetLength (0);
			while (fin && i < l) {
				j = 0;
				while (fin && j < tab.cas.GetLength (1)) {
					if (tab.cas [i, j].mina)
						fin = (tab.cas [i, j].estado == 'm');
					j++;

				}
				i++;
			}
			return fin;
		}

		static void Jugar (Tablero tablero) {
			Console.Clear ();
			bool bomba = false;
			bool juego = true;
			char letra = ' ';
			Dibuja (ref tablero, ProcesaInput (ref tablero, letra));
			while (juego && !bomba) {
				letra = LeeInput ();
				if (letra != ' ') {
					bomba = ProcesaInput (ref tablero, letra);
					Dibuja (ref tablero, bomba);
				}
				juego = !Terminado (ref tablero) && letra != 'q';
			}
			if (Terminado(ref tablero) && !bomba) {
				Console.SetCursorPosition (0, tablero.cas.GetLength (0) + 1);
				Console.WriteLine ("Has ganado!");
				Console.ReadKey ();
			}
			if (letra == 'q') {
				GuardarEstado (tablero);
			}

		}

		public static void Main (string[] args) {
			Console.WriteLine ("Buscaminas\n c para cargar\n n para nueva partida");
			string resp = Console.ReadLine ();
			if (resp == "c" || resp == "C") {
				Jugar (CargarEstado ());
			} else if (resp == "n" || resp == "N") {
				bool empezar = false;
				int fils = 1;
				int cols = 1;
				int minas = 1;
				while (!empezar) {
					Console.WriteLine ("Introduce el numero de filas del tablero(entre 6 y 100):");
					fils = int.Parse (Console.ReadLine ());
					if (fils > 5 && fils <= 100)
						empezar = true;
					Console.WriteLine ("Introduce el numero de columnas del tablero(entre 6 y 100):");
					cols = int.Parse (Console.ReadLine ());
					if (cols <= 5 && cols > 100)
						empezar = false;
					Console.WriteLine ("Introduce el numero de minas del nivel:");
					minas = int.Parse (Console.ReadLine ());
					if (!empezar)
						Console.WriteLine ("Alguno de los datos introducidos no está en el rango.");
				}
				Tablero tablero = creaTablero (fils, cols, minas);
				Jugar (tablero);
			} else
				Console.WriteLine ("Comando desconocido.");
		}

		public static void ColoresCursor(){
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;
		}
	}
}