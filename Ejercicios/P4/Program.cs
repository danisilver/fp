using System;
using System.IO;

namespace P4 {
	class Tablero {
		
		int FILS, COLS;

		enum Casilla {
Blanco,
			Muro,
			Comida,
			Vitamina,
			MuroCelda}

		;

		Casilla[,] cas;

		struct Personaje {
			public int posX, posY;
			// posicion del personaje
			public int dirX, dirY;
			// direccion de movimiento
			public int defX, defY;
			// posiciones de partida por defecto
		}

		// vector de personajes, 0 es Pacman y el resto fantasmas
		Personaje[] pers;
		// colores para los personajes
		ConsoleColor[] colors = {
			ConsoleColor.DarkYellow,
			ConsoleColor.Red,
			ConsoleColor.Magenta,
			ConsoleColor.Cyan,
			ConsoleColor.DarkBlue
		};
		int lapFantasmas = 3000;
		int numComida;
		// numero de casillas retantes con comida o vitamina
		int numNivel;
		// nivel actual de juego

		Random rnd;
		// flag para mensajes de depuracion en consola
		private bool Debug = true;

		Tablero (string archivo) {
			FILS = getDim (archivo, out COLS);
			numComida = 0;
			pers = new Personaje[5];
			cas = new Casilla[FILS, COLS];
			StreamReader leer = new StreamReader (archivo);
			for (int i = 0; i < FILS; i++) {
				string linea = leer.ReadLine ().Replace (" ", "");
				for (int j = 0; j < COLS; j++) {
					switch (linea [j]) {
					case ' ':
					case '0':
						cas [i, j] = Casilla.Blanco;
						break;
					case '1':
						cas [i, j] = Casilla.Muro;
						break;
					case '2':
						cas [i, j] = Casilla.Comida;
						numComida++;
						break;
					case '3':
						cas [i, j] = Casilla.Vitamina;
						numComida++;
						break;
					case '4':
						cas [i, j] = Casilla.MuroCelda;
						break;
					case '5':
						pers [1].posX = pers [1].defX = i;
						pers [1].posY = pers [1].defY = j;
						cas [i, j] = Casilla.Blanco;
						break;
					case '6':
						pers [2].posX = pers [2].defX = i;
						pers [2].posY = pers [2].defY = j;
						cas [i, j] = Casilla.Blanco;
						break;
					case '7':
						pers [3].posX = pers [3].defX = i;
						pers [3].posY = pers [3].defY = j;
						cas [i, j] = Casilla.Blanco;
						break;
					case '8':
						pers [4].posX = pers [4].defX = i;
						pers [4].posY = pers [4].defY = j;
						cas [i, j] = Casilla.Blanco;
						break;
					case '9':
						pers [0].posX = pers [0].defX = i;
						pers [0].posY = pers [0].defY = j;
						cas [i, j] = Casilla.Blanco;
						break;
					default:
						break;
					}
				}
			}
			numNivel = int.Parse (leer.ReadLine ());

			if (Debug)
				rnd = new Random (100);
			else
				rnd = new Random ();
			leer.Close ();
		}

		static int getDim (string archivo, out int ancho) {
			StreamReader leer = new StreamReader (archivo);
			string pal = leer.ReadLine ();
			ancho = pal.Replace (" ", "").Length;
			int i = 1;
			while (!(leer.EndOfStream)) {
				leer.ReadLine ();
				i++;
			}
			leer.Close ();
			return i - 1;
		}

		public static void Main (string[] args) {
			Tablero t = new Tablero ("level00.dat");
			int lap = 200; // retardo para bucle ppal
			char c = ' ';

			Console.WriteLine ("c parar cargar");
			if (Console.ReadKey (true).KeyChar == 'c') 
				t = new Tablero ("guardar.txt");

			t.Dibuja ();
			while (!t.finNivel () && !t.captura ()) {
				leeInput (ref c);
				if (c == 'g') {
					t.guardar ("guardar.txt");
				}
				if (c != ' ' && t.cambiaDir(c)) c=' ';
				t.muevePacman ();
				t.mueveFantasmas (lap);
				t.Dibuja ();
				System.Threading.Thread.Sleep (lap);
			}

			/*
			 * 
            Console.WriteLine("Bienvenido a PACMAN\nPulsa C para cargar una partida o pulsa N para empezar una nueva partida");
            bool valid = false;
            string resp = "";
            Tablero t = new Tablero("level00.dat");
            while (!valid)
            {
                resp = Console.ReadLine();
                if (resp == "c" || resp == "n")
                    valid = true;
                else Console.WriteLine("Respuesta no válida");
            }
            if (resp == "n")
                t = new Tablero("level00.dat");
            else
            {
                bool valid2 = false;
                while (!valid2)
                {
                    valid2 = true;
                    Console.WriteLine("Introduce el nombre de la partida");
                    string r = Console.ReadLine();
                    try
                    {
                        t = new Tablero(r);
                    }
                    catch
                    {
                        Console.WriteLine("Esta partida no existe");
                        valid2 = false;
                    }
                }
            }
            t.Dibuja();
            int lap = 200; // retardo para bucle ppal
            char c = ' ';
            bool capture = false;
            while (!capture)
            {
                while (!t.finNivel()&& !capture)
                {
                    leeInput(ref c);
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
                    Tablero t2 = new Tablero("Tablero0" + t.numNivel + 1 + ".dat");
                    t = t2;
                }
            }
            Console.Clear();
            Console.WriteLine("FIN DE LA PARTIDA");
            Console.ReadKey();
        }
			 * */
		}

		public void Dibuja () {
			//Console.Clear();
			for (int i = 0; i < FILS; i++) {
				for (int j = 0; j < COLS; j++) {
					Console.ResetColor ();
					Console.SetCursorPosition (j, i);
					switch (cas [i, j]) {
					case Casilla.Blanco:
						Console.Write (" ");
						break;
					case Casilla.Comida:
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write (".");
						break;
					case Casilla.Muro:
						Console.BackgroundColor = ConsoleColor.White;
						Console.Write (" ");
						break;
					case Casilla.MuroCelda:
						Console.BackgroundColor = ConsoleColor.Blue;
						Console.Write (" ");
						break;
					case Casilla.Vitamina:
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write ("*");
						break;
					default:
						break;
					}
				}
			}

			Console.SetCursorPosition (pers [0].posY, pers [0].posX);
			Console.BackgroundColor = colors [0];
			Console.Write ("C");

			for (int i = 1; i < pers.Length; i++) {
				Console.SetCursorPosition (pers [i].posY, pers [i].posX);
				Console.BackgroundColor = colors [i];
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write (i);
			}

			Console.ResetColor ();
		}

		public bool siguiente (int x, int y, int dx, int dy, out int nx, out int ny) {
			nx = x + dx;
			if (nx == FILS)
				nx = 0;
			else if (nx < 0)
				nx = FILS - 1;
			ny = y + dy;
			if (ny == COLS)
				ny = 0;
			else if (ny < 0)
				ny = COLS - 1;
			if (dx == 0 && dy == 0)
				return true;
			return (cas [nx, ny] != Casilla.Muro && cas [nx, ny] != Casilla.MuroCelda);
		}

		public void muevePacman () {
			int nx, ny;
			if (siguiente (pers [0].posX, pers [0].posY, pers [0].dirX, pers [0].dirY, out nx, out ny)) {
				pers [0].posX = nx;
				pers [0].posY = ny;
			}

			if (cas [pers [0].posX, pers [0].posY] == Casilla.Comida ||
			    cas [pers [0].posX, pers [0].posY] == Casilla.Vitamina) {
				numComida--;
				cas [pers [0].posX, pers [0].posY] = Casilla.Blanco;
			}
		}

		public bool cambiaDir (char c) {
			int dx, dy;
			dx = dy = 0;
			if (c == 'l')
				dy = -1;
			if (c == 'r')
				dy = 1;
			if (c == 'u')
				dx = -1;
			if (c == 'd')
				dx = 1;
			int nx, ny;
			if (siguiente (pers [0].posX, pers [0].posY, dx, dy, out nx, out ny)) {
				pers [0].dirX = dx;
				pers [0].dirY = dy;
				return true;
			}
			return false;
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
				default:
					break;
				}
			}
		}

		// determina si hay algún fantasma en la misma posición que Pacman.
		public bool captura () {
			for (int i = 1; i < pers.Length; i++) {
				if (pers [0].posX == pers [i].posX && pers [0].posY == pers [i].posY)
					return true;
			}
			return false;
		}

		// comprueba si Pacman se ha comido toda la comida del nivel. Esto es inme-
		// diato utilizando el atributo numComida.
		public bool finNivel () {
			return numComida <= 0;
		}

		//determina si la posición (x,y) contiene un fantasma.
		public bool hayFantasma (int x, int y) {
			for (int i = 1; i < pers.Length; i++) {
				if (pers [i].posX == x && pers [i].posY == y)
					return true;
			}

			return false;
		}

		public void posiblesDirs (int fant, out ListaPares l, out int cont) {
			l = new ListaPares ();
			cont = 0;
			for (int i = -1; i < 2; i += 2) {
				int nx, ny;
				if (siguiente (pers [fant].posX, pers [fant].posY, i, 0, out nx, out ny)) {
					l.insertaFin (i, 0);
					cont++;
				}
				if (siguiente (pers [fant].posX, pers [fant].posY, 0, i, out nx, out ny)) {
					l.insertaFin (0, i);
					cont++;
				} 
			}

			if (cont > 1) {
				if (l.eliminaElto (-pers [fant].dirX, -pers [fant].dirY))
					cont--;
			}
		}

		public void seleccionaDir (int fant) {
			ListaPares l;
			int cont;
			posiblesDirs (fant, out l, out cont);
			l.nEsimo (rnd.Next (0, cont), out pers [fant].dirX, out pers [fant].dirY);
		}

		public void eliminaMuroFantasmas () {
			for (int i = 0; i < cas.GetLength (0); i++) {
				for (int j = 0; j < cas.GetLength (1); j++) {
					if (cas [i, j] == Casilla.MuroCelda)
						cas [i, j] = Casilla.Blanco;
				}
			}
		}

		void mueveFantasmas (int lap) {
			lapFantasmas -= lap;
			if (lapFantasmas <= 0)
				eliminaMuroFantasmas ();
			for (int i = 1; i < pers.Length; i++) {
				int nx, ny;
				if (siguiente (pers [i].posX, pers [i].posY, pers [i].dirX, pers [i].dirY, out nx, out ny)) {
					pers [i].posX = nx;
					pers [i].posY = ny;
				}
				seleccionaDir (i);
			}
		}

		public void guardar (string nombreArchivo) {
			StreamWriter sw = new StreamWriter (nombreArchivo);
			for (int i = 0; i < FILS; i++) {
				for (int j = 0; j < COLS; j++) {
					switch (cas [i, j]) {
					case	 Casilla.Blanco:
						bool encontrado = false;
						for (int k = 0; k < pers.Length; k++) {
							if (pers [k].posX == i && pers [k].posY == j) {
								if (k == 0)
									sw.Write ("9");
								else
									sw.Write ("" + (k + 4));
								encontrado = true;
							} 

						}
						if (!encontrado)
							sw.Write ("0");
						
						break;
					case	 Casilla.Muro:
						sw.Write ("1");
						break;
					case	 Casilla.MuroCelda:
						sw.Write ("4");
						break;
					case	 Casilla.Comida:
						sw.Write ("2");
						break;
					case	 Casilla.Vitamina:
						sw.Write ("3");
						break;
					default:
						break;
					}
				}
				sw.WriteLine ();
			}
			sw.Write ("" + numNivel);
			sw.Close ();
			Console.WriteLine ();
		}

	}
}
