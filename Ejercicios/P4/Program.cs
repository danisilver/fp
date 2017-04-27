using System;
using System.IO;

namespace P4 {
	class Tablero{
		// dimensiones del tablero
		int FILS, COLS;
		// contenido de las casillas
		enum Casilla {Blanco, Muro, Comida, Vitamina, MuroCelda};
		// matriz de casillas (tablero)
		Casilla [,] cas;
		// representacion de los personajes (Pacman y fantasmas)
		struct Personaje {
			public int posX, posY; // posicion del personaje
			public int dirX, dirY; // direccion de movimiento
			public int defX, defY; // posiciones de partida por defecto
		}
		// vector de personajes, 0 es Pacman y el resto fantasmas
		Personaje [] pers;
		// colores para los personajes
		ConsoleColor [] colors = { ConsoleColor.DarkYellow, ConsoleColor.Red,
			ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkBlue };
		int lapFantasmas; // tiempo de retardo de salida del los fantasmas
		int numComida; // numero de casillas retantes con comida o vitamina
		int numNivel; // nivel actual de juego
		// generador de numeros aleatorios para el movimiento de los fantasmas
		Random rnd;
		// flag para mensajes de depuracion en consola
		private bool Debug = true;	


		Tablero(string archivo){
			StreamReader leer = new StreamReader(archivo);
			int alto = 
		}
		static int getDim(StreamReader leer,out int ancho){
			string pepe = leer.ReadLine ();
			ancho = pepe.Length;
			int i = 1;
			while(!(leer.EndOfStream)){
				Console.ReadLine ();
				i++;
			}
			return i-1;
		}

		public static void Main (string[] args) {
			
		}

		public void Dibuja(){}

		public bool Siguiente(int x, int y, int dx, int dy, out int nx, out int ny){
			return false;
		}

		public void MuevePacman(){}

		public bool CambiaDir(char c){return false;}

		public void LeeInput(ref char dir){}
			

	}
}
