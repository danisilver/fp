﻿using System;
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
			
		}

		public void Dibuja(){}

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

		public void MuevePacman(){}

		public bool CambiaDir(char c){return false;}

		public void LeeInput(ref char dir){}
			

	}
}
