//Gallardo Cruzado Mario D.
//Canellas Lluis
using System;
using System.IO;

namespace ejemplo
{
	class MainClass
	{
		enum Casilla { Muro, Libre, Destino };

		struct Tablero
		{
			public int fils, cols;
			public Casilla[,] fijas;
			public bool[,] cajas;
			public int jugX, jugY;
		}

		static void Main(string[] args)
		{
			Tablero tab;
			int movimientos = 0;
			CreaTableroVacio(out tab);
			Console.WriteLine("Introduce el nivel: ");
			tab = LeeNivel("levels.txt", int.Parse(Console.ReadLine()));
			bool juego = true;
			while (juego)
			{
				Dibuja(tab, movimientos);
				Mueve(ref tab, LeeEntrada());
				movimientos++;
				juego = !Terminado(tab);
			}
		}

		static void CreaTableroVacio(out Tablero tab)
		{
			tab.fils = 0;
			tab.cols = 0;
			tab.fijas = new Casilla[50, 50];
			tab.cajas = new bool[50, 50];
			int j = 0;
			for (int i = 0; i < 50; j++)
			{
				tab.fijas[i, j] = Casilla.Muro;
				tab.cajas[i, j] = false;
				if (j % 49 == 0)
				{
					i++;
					j = 0;
				}
			}
			tab.jugX = 0;
			tab.jugY = 0;
		}

		static Tablero LeeNivel(string file, int nivel)
		{
			Tablero tab;
			StreamReader sr = new StreamReader(file);
			bool nivelEncontrado = false;
			string cadenaNivel = "";
			bool leer = true;
			while (!sr.EndOfStream && leer)
			{
				string line = sr.ReadLine();
				if (!nivelEncontrado){
					nivelEncontrado = line.Equals("Level " + nivel);
				} else if (nivelEncontrado) {
					if (!line.StartsWith("Level",StringComparison.CurrentCulture))
						cadenaNivel += '|'+line;
					else
						leer = false;
				}
			}

			CreaTableroVacio(out tab);
			string[] filas = cadenaNivel.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			tab.fils = filas.Length;
			for (int i = 0; i < filas.Length; i++)
			{
				if (tab.cols < filas[i].Length)
					tab.cols = filas[i].Length;

				string actual = filas[i];
				for (int k = 0; k < actual.Length; k++)
				{
					switch (actual[k])
					{
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
			}


			return tab;
		}

		static void Dibuja(Tablero tab, int mov)
		{
			//Dibujar tablero
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			int j = 0;
			int i = 0;
			for (i=0 ; i < tab.fils; j++)
			{
				Console.SetCursorPosition(j * 2, i);
				switch (tab.fijas[i, j])
				{
					case Casilla.Muro:
						Console.BackgroundColor = ConsoleColor.Blue;
						Console.Write("  ");
						break;
					case Casilla.Libre:
						if (tab.cajas[i, j])
						{
							Console.BackgroundColor = ConsoleColor.Red;
							Console.Write("[]");
						}
						else {
							Console.BackgroundColor = ConsoleColor.Magenta;
							Console.Write("  ");
						}
						break;
					case Casilla.Destino:
						if (tab.cajas[i, j])
						{
							Console.BackgroundColor = ConsoleColor.Yellow;
							Console.Write("[]");
						}
						else {
							Console.BackgroundColor = ConsoleColor.Magenta;
							Console.Write("()");
						}
						break;
					default:
						break;
				}
				if ((j + 1) % (tab.cols) == 0)
				{
					i++;
					j = -1;
				}

			}
			Console.SetCursorPosition(tab.jugY * 2, tab.jugX);
			Console.BackgroundColor = ConsoleColor.Green;
			Console.Write("ºº");

			Console.BackgroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(0, tab.fils);
			Console.WriteLine("Numero de movimientos: " + mov);
		}

		static char LeeEntrada()
		{
			return Console.ReadKey().KeyChar;
		}

		static bool Siguiente(int x, int y, char dir, Tablero tab, out int nx, out int ny)
		{
			nx = x;
			ny = y;

			if (dir == 'd')
			{
				nx = x;
				ny = y + 1;
			}
			else if (dir == 'a')
			{
				nx = x;
				ny = y - 1;
			}
			else if (dir == 'w')
			{
				nx = x - 1;
				ny = y;
			}
			else if (dir == 's')
			{
				nx = x + 1;
				ny = y;
			}

			if (ny < 0) { ny = 0; return false; }
			if (nx < 0) { nx = 0; return false; }
			if (ny >= tab.cols) { ny = tab.cols - 1; return false; }
			if (nx >= tab.fils) { nx = tab.fils - 1; return false; }

			return true;
		}

		static void Mueve(ref Tablero tab, char dir)
		{
			int a1 = 0;
			int a2 = 0;
			int b1 = 0;
			int b2 = 0;

			if (Siguiente(tab.jugX, tab.jugY, dir, tab, out a1, out a2))
			{
				Casilla c1 = tab.fijas[a1, a2];
				if (c1 == Casilla.Libre || c1 == Casilla.Destino)
				{
					if (tab.cajas[a1, a2]) {
						if (Siguiente(a1, a2,dir,tab,out b1,out b2)) {
							Casilla c2 = tab.fijas[b1, b2];
							if ((c2 == Casilla.Libre || c2 == Casilla.Destino) && !tab.cajas[b1,b2])
							{
								tab.jugX = a1;
								tab.jugY = a2;
								tab.cajas[a1, a2] = false;
								tab.cajas[b1, b2] = true;
							}
						}
					}
					else {
						tab.jugX = a1;
						tab.jugY = a2;
					}
				}
			}

		}

		static bool Terminado(Tablero tab)
		{
			bool fin = false;
			int i = tab.fils, j = tab.cols;
			int destinos = 0;
			int aciertos = 0;
			while (!fin && i >= 0)
			{
				if (tab.cajas[i, j])
				{
					if (tab.fijas[i, j] == Casilla.Destino)
					{
						destinos++;
						if (tab.cajas[i, j])
						{
							aciertos++;
						}
					}

				}
				if (j == 0)
				{
					i--;
					j = tab.cols;
				}
				j--;
			}
			if (aciertos != destinos) fin = true;
			else fin = false;
			return fin;
		}
	}
}
