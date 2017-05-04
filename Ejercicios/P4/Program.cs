using System;
using System.IO;

namespace P4 {
	class Tablero{
		
		int FILS, COLS;
		
		enum Casilla {Blanco, Muro, Comida, Vitamina, MuroCelda};
		
		Casilla [,] cas;
		
		struct Personaje {
			public int posX, posY; // posicion del personaje
			public int dirX, dirY; // direccion de movimiento
			public int defX, defY; // posiciones de partida por defecto
		}

		// vector de personajes, 0 es Pacman y el resto fantasmas
		Personaje [] pers;
		// colores para los personajes
		ConsoleColor [] colors = { ConsoleColor.DarkYellow, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkBlue };
		int lapFantasmas; // tiempo de retardo de salida del los fantasmas
		int numComida; // numero de casillas retantes con comida o vitamina
		int numNivel; // nivel actual de juego
		
		Random rnd;
		// flag para mensajes de depuracion en consola
		private bool Debug = true;	


		Tablero(string archivo){
			FILS = getDim (archivo, out COLS);
			StreamReader leer = new StreamReader(archivo);
			for (int i = 0; i < COLS; i++) {
				for (int j = 0; j < FILS; j++) {
					switch (leer.Read ()) {
					case '0':
						cas [i, j] = Casilla.Blanco;
						break;
					case '1':
						cas[i,j]=Casilla.Muro;
						break;
					}
				}
			}

			leer.Close ();
		}
		static int getDim(string archivo,out int ancho){
			StreamReader leer = new StreamReader(archivo);
			string pal = leer.ReadLine ();
			ancho = pal.Length;
			int i = 1;
			while(!(leer.EndOfStream)){
				Console.ReadLine ();
				i++;
			}
			leer.Close ();
			return i-1;
		}

		public static void Main (string[] args) {
			Tablero t = new Tablero("");
			t.Dibuja();
			int lap = 200; // retardo para bucle ppal
			char c= ' ';
			while (true) {
				leeInput(ref c);
				if (c != ' ' && t.cambiaDir(c)) c=' ';
				t.muevePacman();
				// IA de los fantasmas: TODO
				t.Dibuja();
				System.Threading.Thread.Sleep (lap);
			}
		}

		public void Dibuja(){
			for (int i = 0; i < FILS; i++) {
				for (int j = 0; j < COLS; j++) {
					Console.ResetColor ();
					Console.SetCursorPosition (j, i);
					switch (cas[i,j]) {
					case Casilla.Blanco:
					case Casilla.Comida:
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write (".");
						break;
					case Casilla.Muro:
						Console.BackgroundColor = ConsoleColor.White;
						Console.Write(" ");
						break;
					case Casilla.MuroCelda:
						Console.BackgroundColor = ConsoleColor.Blue;
						Console.Write(" ");
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
			Console.BackgroundColor = colors[0];
			Console.Write ("C");

			for (int i = 1; i < pers.Length; i++) {
				Console.SetCursorPosition (pers [i].posY, pers [i].posX);
				Console.BackgroundColor = colors [i];
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write (i);
			}


		}

		public bool Siguiente(int x, int y, int dx, int dy, out int nx, out int ny){
			nx = 0; ny = 0;
			return false;
		}

		public void muevePacman(){
			
		}

		public bool cambiaDir(char c){return false;}

		public static void leeInput(ref char dir){
			char letra = ' ';
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
				default:
					break;
				}
			}
		}

		// determina si hay algún fantasma en la misma posición que Pacman.
		public void captura(){}

		// comprueba si Pacman se ha comido toda la comida del nivel. Esto es inme-
		// diato utilizando el atributo numComida.
		public void finNivel(){}

	}
}
