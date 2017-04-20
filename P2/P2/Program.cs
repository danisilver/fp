//Gallardo Cruzado Mario D.
//Canellas Salesa Lluis
using System;
using System.IO;

namespace P2 {
	class MainClass {
		enum Casilla { Muro, Libre, Destino };

		struct Tablero {
			public int fils, cols;
			public Casilla[,] fijas;
			public bool[,] cajas;
			public int jugX, jugY;
		}

		// Metodo principal del programa.
		static void Main(string[] args) {
			//inicialización del juego
			Tablero tab;
			int movimientos = 0;
			// seleccion del nivel
			Console.WriteLine("Introduce el nivel: ");
			tab = LeeNivel("levels.txt", int.Parse(Console.ReadLine()));
			Dibuja(tab, movimientos);
			//bucle principal del juego
			while (!Terminado(tab)) {
				// input
				char letra = LeeEntrada();
				if (letra != ' ') {
					//lógica del juego
					if (Mueve(ref tab, letra)) {
						movimientos++;
						//renderizado
						Dibuja(tab, movimientos);
					}
				}
			}
			//mensaje de fin de partida
			Console.WriteLine("Has Ganado, crack!");
			Console.ReadLine();
		}

		//creacion de un tablero con valores por defecto.
		static void CreaTableroVacio(out Tablero tab) {
			tab.fils = tab.cols = 0;
			tab.fijas = new Casilla[50, 50];
			tab.cajas = new bool[50, 50];
			for (int i = 0; i < 50; i++)
				for (int j = 0; j < 50; j++) {
					tab.fijas[i, j] = Casilla.Muro;
					tab.cajas[i, j] = false;

				}
			tab.jugX = 0;
			tab.jugY = 0;
		}

		//Leer de un fichero el nivel especificado, devuelve un struct tablero
		static Tablero LeeNivel(string file, int nivel) {
			//inicializacion variables
			Tablero tab;
			CreaTableroVacio(out tab);
			StreamReader sr = new StreamReader(file);
			bool nivelEncontrado = false, leer = true;
			string line = "";
 		
			int i = 0;
			while (!sr.EndOfStream && leer) {
				line = sr.ReadLine();
				if (!nivelEncontrado) {
					nivelEncontrado = (line == "Level " + nivel);
				} else {
					if (tab.cols < line.Length) tab.cols = line.Length;
					leer = !(line=="");
					if (leer) {
						for (int k = 0; k < line.Length; k++) {
							switch (line[k]) {
								case '#':
									tab.fijas[i, k] = Casilla.Muro;
									break;
								case ' ':
									tab.fijas[i, k] = Casilla.Libre;
									break;
								case '.':
									tab.fijas[i, k] = Casilla.Destino;
									break;
								case '*':
									tab.fijas[i, k] = Casilla.Destino;
									tab.cajas[i, k] = true;
									break;
								case '$':
									tab.fijas[i, k] = Casilla.Libre;
									tab.cajas[i, k] = true;
									break;
								case '+':
									tab.fijas[i, k] = Casilla.Destino;
									tab.jugX = i;
									tab.jugY = k;
									break;
								case '@':
									tab.fijas[i, k] = Casilla.Libre;
									tab.jugX = i;
									tab.jugY = k;
									break;
								default:
									break;
							}
						}
						i++;
						tab.fils = i;
					} 
;
				}
			}

			sr.Close();
			return tab;
		}
		//Realiza un recorrido por el tablero y dibuja muros,casillas libres, casillas destino,jugador y cajas
		//con distintos colores y carácteres para conseguir una interpretación visual del estado del juego clara.
		static void Dibuja(Tablero tab, int mov) {
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();

			for (int i = 0; i < tab.fils; i++) {
				for (int j = 0; j < tab.cols; j++) {
					Console.SetCursorPosition(j * 2, i);
					switch (tab.fijas[i, j]) {
						case Casilla.Muro:
							Console.BackgroundColor = ConsoleColor.Blue;
							Console.Write("  ");
							break;
						case Casilla.Libre:
							if (tab.cajas[i, j]) {
								Console.BackgroundColor = ConsoleColor.Red;
								Console.Write("[]");
							} else {
								Console.BackgroundColor = ConsoleColor.Magenta;
								Console.Write("  ");
							}
							break;
						case Casilla.Destino:
							if (tab.cajas[i, j]) {
								Console.BackgroundColor = ConsoleColor.Yellow;
								Console.Write("[]");
							} else {
								Console.BackgroundColor = ConsoleColor.Magenta;
								Console.Write("()");
							}
							break;
						default:
							break;
					}
				}
			}
			Console.SetCursorPosition(tab.jugY * 2, tab.jugX);
			Console.BackgroundColor = ConsoleColor.Green;
			Console.Write("ºº");

			Console.BackgroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(0, tab.fils);
			Console.WriteLine("Numero de movimientos: " + mov);
		}
		//Se encarga del input del jugador,lee del teclado y devuelve un char que indica la dirección de movimiento
		//solo si la entrada es una flecha direccional.
		static char LeeEntrada() {
			string tecla;
			char letra = ' ';
			if (Console.KeyAvailable) {
				tecla = Console.ReadKey(true).Key.ToString();
				if (tecla == "RightArrow")
					letra = 'r';
				else if (tecla == "LeftArrow")
					letra = 'l';
				else if (tecla == "UpArrow")
					letra = 'u';
				else if (tecla == "DownArrow")
					letra = 'd';

			}
			return letra;
		}

		//Comprueba la siguiente posición en el tablero según la dirección de entrada y 
		//determina si esa posición es válida con un booleano.
		static bool Siguiente(int x, int y, char dir, Tablero tab, out int nx, out int ny) {
			nx = x;
			ny = y;

			if (dir == 'r')
				ny++;
			else if (dir == 'l')
				ny--;
			else if (dir == 'u')
				nx--;
			else if (dir == 'd')
				nx++;

			if (ny < 0 || ny >= tab.cols || nx < 0 || nx >= tab.fils)
				return false;
			else
				return true;
		}

		//Cambia la posición del jugador y, puede,la de una caja hacia la dirección indicada comprobando antes si esta está permitida.
		//Devuelve un booleano que comprueba si el jugador se ha conseguido mover de manera efectiva.
		static bool Mueve(ref Tablero tab, char dir) {
			int a1, a2, b1, b2;
			bool movimiento = false;
			//comprueba que la siguiente posición del jugador es válida
			if (Siguiente(tab.jugX, tab.jugY, dir, tab, out a1, out a2)) {
				Casilla c1 = tab.fijas[a1, a2];
				if (c1 != Casilla.Muro) {
					//Si se encuentra una caja vuelve a comprobar si delante tiene una posición válida, si es así se mueve el jugador y la caja.
					if (tab.cajas[a1, a2]) {
						if (Siguiente(a1, a2, dir, tab, out b1, out b2)) {
							Casilla c2 = tab.fijas[b1, b2];
							if ((c2 == Casilla.Libre || c2 == Casilla.Destino) && !tab.cajas[b1, b2]) {
								tab.jugX = a1;
								tab.jugY = a2;
								tab.cajas[a1, a2] = false;
								tab.cajas[b1, b2] = true;
								movimiento = true;
							}
						}
					}
					//Si no hay caja el jugador se mueve hacia la dirección indicada sin problemas.
					else {
						tab.jugX = a1;
						tab.jugY = a2;
						movimiento = true;
					}
				}
			}
			return movimiento;
		}

		//Comprueba si el juego ha terminado mediante una busqueda en la matriz del tablero 
		//hasta encontrar una casilla destino sin caja y devuelve falso, en caso contrario devuelve verdadero
		static bool Terminado(Tablero tab) {
			bool fin = true;
			int i = 0, j;
			while (fin && (i < tab.fils)) {
				j = 0;
				while (fin && (j < tab.cols)) {	
				    fin = !(tab.fijas[i, j] == Casilla.Destino && !tab.cajas[i, j]);
					j++;
				}
				i++;
			}
			return fin;
		}
	}
}
