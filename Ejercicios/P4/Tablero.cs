using System;
using System.IO;

namespace P4 {
	public class Tablero {
		bool interactivo = false;
		int FILS, COLS;

		public enum Casilla {
			Blanco,
			Muro,
			Comida,
			Vitamina,
			MuroCelda}

		public Casilla[,] cas;

		public struct Personaje {
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
		public int numNivel;
		// nivel actual de juego
		ListaPares celdas;

		Random rnd;
		// flag para mensajes de depuracion en consola
		private bool Debug = true;

		public Tablero (string archivo)  {
			celdas = new ListaPares ();
			getDim (archivo, out COLS, out FILS);
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
						celdas.insertaIni (i, j);
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

		static void getDim (string archivo, out int ancho,out int alto) {
			StreamReader leer = new StreamReader (archivo);
			string pal = leer.ReadLine ();
			ancho = pal.Replace (" ", "").Length;
			int i = 1;
			while (!(leer.EndOfStream)) {
				leer.ReadLine ();
				i++;
			}
			leer.Close ();
			alto = i - 1;
		}


		public void Dibuja () {
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

			Console.SetCursorPosition (0, FILS);
			Console.WriteLine ("pulsa p para pausar");
			Console.WriteLine ("pulsa q para salir");
			Console.WriteLine ("NIVEL ACTUAL "+ numNivel);
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

			if (interactivo)
				return true;
			return (cas [nx, ny] != Casilla.Muro && cas [nx, ny] != Casilla.MuroCelda);
		}

		public void muevePacman () {
			int nx, ny;
			if (siguiente (pers [0].posX, pers [0].posY, pers [0].dirX, pers [0].dirY, out nx, out ny)) {
				pers [0].posX = nx;
				pers [0].posY = ny;
			}

			if (interactivo)
				return;

			if (cas [pers [0].posX, pers [0].posY] == Casilla.Comida ||
			    cas [pers [0].posX, pers [0].posY] == Casilla.Vitamina) {
				numComida--;
				if(cas [pers [0].posX, pers [0].posY] == Casilla.Vitamina){
					for (int i = 1; i < pers.Length; i++) {
						pers [i].posX = pers [i].defX;
						pers [i].posY = pers [i].defY;
					}

					int celdax, celday;
					celdas.iniciaRecorrido ();
					while (celdas.dame_actual_y_avanza (out celdax,out celday)) {
						cas [celdax, celday] = Casilla.MuroCelda;
					}
					lapFantasmas = 3000;
				}
				cas [pers [0].posX, pers [0].posY] = Casilla.Blanco;
			}
		}

		public bool cambiaDir (char c) {//cambiamos la direccion deacuerdo a la letra si se puede
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

		// determina si hay algún fantasma en la misma posición que Pacman.
		public bool captura () {
			return hayFantasma (pers [0].posX, pers [0].posY);
		}

		// comprueba si Pacman se ha comido toda la comida del nivel. Esto es inme-
		// diato utilizando el atributo numComida.
		public bool finNivel () {
			return numComida <= 0;
		}

		//determina si la posición (x,y) contiene un fantasma.
		public bool hayFantasma (int x, int y) {
			bool fantasma = false;
			int i = 1;
			while (!fantasma && i < pers.Length) {
				if (pers [i].posX == x && pers [i].posY == y) fantasma = true;
				i++;
			}return fantasma;
		}

		public void posiblesDirs (int fant, out ListaPares l, out int cont) {
			l = new ListaPares ();
			cont = 4;
			l.insertaFin (1,0);
			l.insertaFin (0,1);
			l.insertaFin (-1,0);
			l.insertaFin (0,-1);

			l.iniciaRecorrido ();

			int dx, dy;
			int nx, ny;
			while (l.dame_actual_y_avanza (out dx,out  dy)){
				if(!siguiente (pers [fant].posX, pers [fant].posY, dx, dy, out nx, out ny)){
					l.eliminaElto (dx, dy);
					cont--;
				}
			}

			if( cont > 1 ) 
				if (l.eliminaElto (-pers [fant].dirX, -pers [fant].dirY))
					cont--;
		}

		public void seleccionaDir (int fant) {
			ListaPares l;
			int cont;
			posiblesDirs (fant, out l, out cont);
			if(cont > 0) l.nEsimo (rnd.Next (0, cont), out pers [fant].dirX, out pers [fant].dirY);
		}

		public void eliminaMuroFantasmas () {
			for (int i = 0; i < cas.GetLength (0); i++) {
				for (int j = 0; j < cas.GetLength (1); j++) {
					if (cas [i, j] == Casilla.MuroCelda)
						cas [i, j] = Casilla.Blanco;
				}
			}
		}

		public void mueveFantasmas (int lap) {
			lapFantasmas -= lap;
			if (lapFantasmas <= 0)
				eliminaMuroFantasmas ();
			for (int i = 1; i < pers.Length; i++) {
				int nx, ny;
				if (siguiente (pers [i].posX, pers [i].posY, pers [i].dirX, pers [i].dirY, out nx, out ny)) {
                    if (!hayFantasma(nx, ny)) { 
                        pers [i].posX = nx;
					    pers [i].posY = ny;
                    }
				}
				seleccionaDir (i);
			}
		}

		public void guardar (string nombreArchivo) {
			StreamWriter sw = new StreamWriter (nombreArchivo);
			char[][] temp = new char[FILS][];
			for (int i = 0; i < FILS; i++) {
				temp[i] = new char[COLS];
				for (int j = 0; j < COLS; j++) {
					switch (cas [i, j]) {
					case	 Casilla.Blanco:
							temp[i][j]= '0';
						break;
					case	 Casilla.Muro:
							temp[i][j] = '1';
						break;
					case	 Casilla.MuroCelda:
							temp[i][ j] = '4';
						break;
					case	 Casilla.Comida:
							temp[i][j] = '2';
						break;
					case	 Casilla.Vitamina:
							temp[i][j] = '3';
						break;
					default:
						break;
					}
				}
			}

			temp[pers[0].posX][pers[0].posY] = '9';

			temp[pers[1].posX][pers[1].posY] = '5';
			temp[pers[2].posX][pers[2].posY] = '6';
			temp[pers[3].posX][pers[3].posY] = '7';
			temp[pers[4].posX][pers[4].posY] = '8';



			for (int i = 0; i < FILS; i++) {
				sw.WriteLine(new string(temp[i]));
			}
			sw.Write ("" + numNivel);
			sw.Close ();
			Console.WriteLine ();
		}

		public void cambiaCasilla(Casilla casilla){
			cas [pers [0].posX, pers [0].posY] = casilla;
		}

		public void cambiaPersonaje(int indice){
			pers [indice].posX = pers [0].posX;
			pers [indice].posY = pers [0].posY;
		}

		public void setInteractivo(bool set){
			interactivo = set;
		}

        #region Código para tests de unidad

        /// <summary>
        /// Crea un nuevo tablero de casillas vacías de las dimensiones indicadas.
        /// Crea también el array de personajes (vacío)
        /// </summary>
        /// <param name="nFils">Número de filas del tablero</param>
        /// <param name="nCols">Número de columnas del tablero</param>
        public Tablero(int nFils, int nCols)
        {
            cas = new Casilla[nFils, nCols];
            COLS = nCols;
            FILS = nFils;
            pers = new Personaje[5];
            rnd = new Random();
            celdas = new ListaPares();
            for (int i = 0; i < nFils; i++)
                for (int j = 0; j < nCols; j++)
                {
                    cas[i, j] = Casilla.Blanco;
                }
        }

        /// <summary>
        /// Cambia el tipo de una casilla del tablero
        /// </summary>
        /// <param name="fila">Fila de la casilla</param>
        /// <param name="columna">Columna de la casilla</param>
        /// <param name="tipoCasilla">Tipo que se quiere poner en la casilla</param>
        public void cambiaCasilla(int fila, int columna, Casilla tipoCasilla)
        {
            cas[fila, columna] = tipoCasilla;
        }

        /// <summary>
        /// Establece el número de comidas del tablero
        /// </summary>
        /// <param name="actComida">Número de comidas que queremos que haya en el tablero</param>
        public void setNumComida(int actComida)
        {
            numComida = actComida;
        }

        /// <summary>
        /// Consulta el número de comidas que hay en el tablero
        /// </summary>
        /// <returns>El número de comidas del tablero</returns>
        public int getNumComida()
        {
            return numComida;
        }

        /// <summary>
        /// Establece la posición y dirección de un personaje en el tablero,
        /// representado por la posición que ocupa en el array de personajes.
        /// Recuerda que el 0 es pacman y el resto [1,4] son fantasmas
        /// </summary>
        /// <param name="nPersonaje">Posición que ocupa el personaje a establecer</param>
        /// <param name="posX">Coordenada X</param>
        /// <param name="posY">Coordenada Y</param>
        /// <param name="dirX">Dirección X</param>
        /// <param name="dirY">Dirección Y</param>
        public void setPersonaje(int nPersonaje, int posX, int posY, int dirX, int dirY)
        {
            pers[nPersonaje].posX = posX;
            pers[nPersonaje].posY = posY;
            pers[nPersonaje].dirX = dirX;
            pers[nPersonaje].dirY = dirY;
        }

        /// <summary>
        /// Devuelve uno de los personajes del tablero,
        /// representado por la posición que ocupa en el array de personajes.
        /// Recuerda que el 0 es pacman y el resto [1,4] son fantasmas
        /// </summary>
        /// <param name="nPersonaje">Posición que ocupoa el personaje</param>
        /// <returns>El personaje que hay en la posición indicada</returns>
        public Personaje getPersonaje(int nPersonaje)
        {
            return pers[nPersonaje];
        }

        #endregion

    }
}
