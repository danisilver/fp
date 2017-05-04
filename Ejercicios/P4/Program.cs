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
			pers = new Personaje[5];
			cas = new Casilla[COLS, FILS];
			StreamReader leer = new StreamReader(archivo);
			for (int i = 0; i < COLS; i++) {
				for (int j = 0; j < FILS; j++) {
					switch (leer.Read ()) {
					case'0':
						cas[i,j]=Casilla.Blanco;
						break;
					case'1':
						cas [i, j] = Casilla.Muro;
						break;
					case'2':
						cas[i,j]=Casilla.Comida;
						break;
					case'3':
						cas[i,j]=Casilla.Vitamina;
						break;
					case'4':
						cas[i,j]=Casilla.MuroCelda;
						break;
					case'5':
						pers [1].posX = pers[1].defX = i;
						pers [1].posY = pers[1].defY = j;
						cas[i,j]=Casilla.Blanco;
						break;
					case'6':
						pers [2].posX=pers[2].defX = i;
						pers [2].posY = pers[2].defY = j;
						cas[i,j]=Casilla.Blanco;
						break;
					case'7':
						pers [3].posX = pers[3].defX = i;
						pers [3].posY = pers[3].defY = j;
						cas[i,j]=Casilla.Blanco;
						break;
					case'8':
						pers [4].posX = pers[4].defX = i;
						pers [4].posY = pers[4].defY = j;
						cas[i,j]=Casilla.Blanco;
						break;
					case'9':
						pers [0].posX = pers[0].defX = i;
						pers [0].posY = pers[0].defY = j;
						cas[i,j]=Casilla.Blanco;
						break;
					default:
						break;
					}
				}
			}
			if (Debug)
				rnd = new Random (100);
			else
				rnd = new Random ();
			leer.Close ();
		}
		static int getDim(string archivo,out int ancho){
			StreamReader leer = new StreamReader(archivo);
			string pal = leer.ReadLine ();
			ancho = pal.Length/2 +pal.Length%2;
			int i = 1;
			while(!(leer.EndOfStream)){
				leer.ReadLine ();
				i++;
			}
			leer.Close ();
			return i-1;
		}

		public static void Main (string[] args) {
			Tablero t = new Tablero("level00.dat");
			t.Dibuja();
			Console.Read ();
			/*int lap = 200; // retardo para bucle ppal
			char c= ' ';
			while (true) {
				leeInput(ref c);
				if (c != ' ' && t.cambiaDir(c)) c=' ';
				t.muevePacman();
				// IA de los fantasmas: TODO
				t.Dibuja();
				System.Threading.Thread.Sleep (lap);
			}*/
		}

		public void Dibuja(){
			for (int i = 0; i < COLS; i++) {
				for (int j = 0; j < FILS; j++) {
					Console.ResetColor ();
					Console.SetCursorPosition (i, j);
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

			Console.SetCursorPosition (pers [0].posX, pers [0].posY);
			Console.BackgroundColor = colors[0];
			Console.Write ("C");

			for (int i = 1; i < pers.Length; i++) {
				Console.SetCursorPosition (pers [i].posX, pers [i].posY);
				Console.BackgroundColor = colors [i];
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write (i);
			}


		}

		public bool siguiente(int x, int y, int dx, int dy, out int nx, out int ny){
			nx = x + dx;
			if (nx == COLS)
				nx = 0;
			else if (nx < 0)
				nx = COLS - 1;
			ny = y + dy;
			if (ny == FILS)
				ny = 0;
			else if (ny < 0)
				ny = FILS - 1;
			
			return (cas[nx,ny]!=Casilla.Muro && cas[nx,ny]!=Casilla.MuroCelda);
		}

		public void muevePacman(){
			int nx, ny;
			if (siguiente (pers [0].posX, pers [0].posY, pers[0].dirX, pers[0].dirY, out nx, out ny)) {
				pers [0].posX = nx;
				pers [0].posY = ny;
			}

			if (cas [pers [0].posX, pers [0].posY] == Casilla.Comida ||
				cas [pers [0].posX, pers [0].posY] == Casilla.Vitamina) {
				numComida--;
				cas [pers [0].posX, pers [0].posY] = Casilla.Blanco;
			}
		}

		public bool cambiaDir(char c){
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
			if(siguiente(pers[0].posX, pers[0].posY, dx, dy, out nx, out ny)){
				pers [0].dirX = dx;
				pers [0].dirY = dy;
				return true;
			};
			return false;
		}

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
		public void captura(){
			for (int i = 1; i < pers.Length; i++) {
				if (pers [0].posX == pers [i].dirX && pers [0].posY == pers [i].posY)
					return true;
			}
			return false;
		}

		// comprueba si Pacman se ha comido toda la comida del nivel. Esto es inme-
		// diato utilizando el atributo numComida.
		public void finNivel(){
			numComida <= 0;
		}

		//determina si la posición (x,y) contiene un fantasma.
		public bool hayFantasma(int x, int y){
			for (int i = 1; i < pers.Length; i++) {
				if (pers [i].posX == x && pers [i].posY == y)
					return true;
			}

			return false;
		}

		public void posiblesDirs(int fant, out ListaPares l, out int cont){
			l = new ListaPares ();
			cont = 0;
			for (int i = 1; i < 4; i++) {
				int nx, ny;
				if(siguiente(pers[i].posX, pers[i].posY, 2 - i, 0, out nx, out ny)){
					l.insertaFin(2 - i, 0);
					cont++;
				}
				if(siguiente(pers[i].posX, pers[i].posY, 0, 2 - i, out nx, out ny)){
					l.insertaFin(0, 2 - i);
					cont++;
				}
			}

		}

		public void seleccionaDir(int fant){
			ListaPares l;
			int cont;
			posiblesDirs (fant, out l, out cont);
			l.nEsimo (rnd.Next (0, cont), pers [fant].dirX, pers [fant].dirY);
		}

		public void eliminaMuroFantasmas(){
			for (int i = 0; i < cas.GetLength(0); i++) {
				for (int j = 0; j < cas.GetLength(1); j++) {
					if (cas [i, j] == Casilla.MuroCelda)
						cas [i, j] = Casilla.Blanco;
				}
			}
		}

		void mueveFantasmas(int lap){
			for (int i = 1; i < pers.Length; i++) {
				if (!cas [pers [i].posX + pers [i].dirX, pers [i].posY += pers [i].dirY] == Casilla.Muro) {
					pers [i].posX += pers [i].dirX;
					pers [i].posY += pers [i].dirY;
				}
				seleccionaDir (i);
			}
		}

	}
}
