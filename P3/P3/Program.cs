using System;

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

		static bool[,] visitados;

		struct Tablero {
			public Casilla[,] cas;
			public int posX, posY;
		}

		static Tablero creaTablero(int fils, int cols, int numMinas) {
			Random rand = new Random();
			Casilla[,] casilleros = new Casilla[fils, cols];
			for (int a = 0; a < fils; a++) {
				for (int b = 0; b < cols; b++) {
					casilleros[a, b].estado = 'o';
				}
			}

			Tablero tab = new Tablero { cas = casilleros, posX = 0, posY = 0 };

			int fila, columna, i = 0;
			while (i < numMinas) {
				fila = rand.Next(0, fils);
				columna = rand.Next(0, cols);
				if (!tab.cas[fila, columna].mina) {
					tab.cas[fila, columna].mina = true;
					i++;
				}
			}
			return tab;
		}

		static void Dibuja(ref Tablero tab, bool bomba) {
			for (int i = 0; i < tab.cas.GetLength(0); i++) {
				for (int j = 0; j < tab.cas.GetLength(1); j++) {
					Console.ResetColor();
					Console.SetCursorPosition(j, i);
					//if(tab.cas[i, j].mina) Console.Write("p");
					switch (tab.cas[i, j].estado) {
						case '*':
							if (tab.cas[i, j].mina && bomba) {
								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write("x");
							} else {
								Console.BackgroundColor = ConsoleColor.Red;
								Console.ForegroundColor = ConsoleColor.Gray;
								Console.Write("x");
							}
							break;
						case 'X':
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write("x");
							break;
						case 'o':
							if (tab.cas[i, j].mina && bomba) {
								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write("x");
							} else {
								Console.ForegroundColor = ConsoleColor.White;
								if (i == tab.posX && j == tab.posY) {
									Console.ForegroundColor = ConsoleColor.Black;
									Console.BackgroundColor = ConsoleColor.White;
								}
								Console.Write("o");
							}
							break;
						case 'm':
							Console.BackgroundColor = ConsoleColor.Yellow;
							Console.ForegroundColor = ConsoleColor.White;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("*");
							break;
						case '0':
							Console.ForegroundColor = ConsoleColor.Blue;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write(" ");
							break;
						case '1':
							Console.ForegroundColor = ConsoleColor.Blue;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("1");
							break;
						case '2':
							Console.ForegroundColor = ConsoleColor.Green;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("2");
							break;
						case '3':
							Console.ForegroundColor = ConsoleColor.Red;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("3");
							break;
						case '4':
							Console.ForegroundColor = ConsoleColor.DarkBlue;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("4");
							break;
						case '5':
							Console.ForegroundColor = ConsoleColor.DarkRed;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("5");
							break;
						case '6':
							Console.ForegroundColor = ConsoleColor.Cyan;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("6");
							break;
						case '7':
							Console.ForegroundColor = ConsoleColor.DarkGreen;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("7");
							break;
						case '8':
							Console.ForegroundColor = ConsoleColor.Magenta;
							if (i == tab.posX && j == tab.posY) {
								Console.ForegroundColor = ConsoleColor.Black;
								Console.BackgroundColor = ConsoleColor.White;
							}
							Console.Write("8");
							break;
						default:
							break;
					}

				}
			}
			if (bomba) {
				Console.SetCursorPosition(tab.posY, tab.posX);
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.BackgroundColor = ConsoleColor.Red;
				Console.Write("*");
				Console.SetCursorPosition(0, tab.cas.GetLength(1) + 1);
				Console.WriteLine("\n¡Has perdido pendejo!");
				Console.Read();
			}

		}


		static void DescubreAdyacentes(ref Tablero tab, int x, int y) {
			int fils = tab.cas.GetLength(0);
			int cols = tab.cas.GetLength(1);
			//Casilla[] visitados = new Casilla [1000];
			int tam = 0;
			int numAdyacentes = 0;
			for (int i = Math.Max(x - 1, 0); i < Math.Min(fils, x + 2); i++) {
				for (int j = Math.Max(y - 1, 0); j < Math.Min(cols, y + 2); j++) {
					if (tab.cas[i, j].mina)
						numAdyacentes++;

				}
			}

			tab.cas[x, y].estado = char.Parse("" + numAdyacentes + "");
			visitados[x, y] = true;

			for (int i = Math.Max(x - 1, 0); i < Math.Min(fils, x + 2) && numAdyacentes == 0; i++) {
				for (int j = Math.Max(y - 1, 0); j < Math.Min(cols, y + 2); j++) {
					Casilla casilla = tab.cas[i, j];
					if (casilla.estado == 'o' && !visitados[i, j]) {
						DescubreAdyacentes(ref tab, i, j);
					}
				}
			}

		}

		static bool ClickCasilla(ref Tablero tab) {
			Casilla cas = tab.cas[tab.posX, tab.posY];
			if (!cas.mina) {
				DescubreAdyacentes(ref tab, tab.posX, tab.posY);
			} else {
				tab.cas[tab.posX, tab.posY].estado = '*';
			}
			return cas.mina;
		}

		static char LeeInput() {
			char letra = ' ';
			if (Console.KeyAvailable) {
				switch (Console.ReadKey(true).Key.ToString()) {
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

		static bool ProcesaInput(ref Tablero tab, char c) {

			bool perder = false;
			if (c == 'x') {
				if (tab.cas[tab.posX, tab.posY].estado == 'm') {
					tab.cas[tab.posX, tab.posY].estado = 'o';
				} else {
					tab.cas[tab.posX, tab.posY].estado = 'm';
				}
			} else if (c == 'c') {
				perder = ClickCasilla(ref tab);
			} else if (c == 'l') {
				tab.posY = Math.Max(0, tab.posY - 1);
			} else if (c == 'r') {
				tab.posY = Math.Min(tab.cas.GetLength(1) - 1, tab.posY + 1);
			} else if (c == 'u') {
				tab.posX = Math.Max(0, tab.posX - 1);
			} else if (c == 'd') {
				tab.posX = Math.Min(tab.cas.GetLength(0) - 1, tab.posX + 1);
			}
			return perder;
		}

		static bool Terminado(ref Tablero tab) {
			bool fin = true;
			int i = 0, j;
			int l = tab.cas.GetLength(0);
			while (fin && i < l) {
				j = 0;
				while (fin && j < tab.cas.GetLength(1)) {
					if (tab.cas[i, j].mina)
						fin = (tab.cas[i, j].estado == 'm');
					j++;

				}
				i++;
			}
			return fin;
		}

		static void Jugar() {
			int fils = 20;
			int cols = 20;
			visitados = new bool[fils, cols];
			Tablero tablero = creaTablero(fils, cols, 50);
			bool bomba = false;
			bool juego = true;
			char letra = ' ';
			Dibuja(ref tablero, ProcesaInput(ref tablero, letra));
			while (juego && !bomba) {
				letra = LeeInput();
				if (letra != ' ') {
					bomba = ProcesaInput(ref tablero, letra);
					Dibuja(ref tablero, bomba);
				}
				juego = !Terminado(ref tablero) && letra != 'q';
			}

		}

		public static void Main(string[] args) {
			Jugar();
		}


	}
}