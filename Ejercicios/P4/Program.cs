﻿using System;
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
