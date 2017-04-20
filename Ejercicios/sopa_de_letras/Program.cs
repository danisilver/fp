using System;

namespace sopa_de_letras
{
	struct Par{
		public int x, y;
	}

	struct Sopa{
		public int alto, ancho;
		public string[] matriz;
	}

	class MainClass {
		public static void Main (string[] args) {
			Sopa s;
			s.alto = 6;
			s.ancho = 9;
			s.matriz = new String[]{"ABCDBARCO" , "EKLMNOPQR" , "HTAVIONOR" , "CGRTUITXB" , "OROHFOVAZ" , "CMPPMEVAN"};
			string[] pals = {"COCHE", "AVION", "BARCO", "MOTO", "PATINES"};
			Resuelve (ref s, pals);
			Console.ReadLine();
		}

		public static Par[] dirs(){
			return new Par[]{ 
				new Par{x=-1, y=-1},
				new Par{x=-1, y= 0},
				new Par{x=-1, y= 1},
				new Par{x= 0, y=-1},
				new Par{x= 0, y= 1},
				new Par{x= 1, y=-1},
				new Par{x= 1, y= 0},
				new Par{x= 1, y= 1}
			};
		}

		public static bool compruebaPosDir(ref Sopa s, string pal, Par pos, Par dir){
			bool encontrado = true;
			int indice = 0;

			int x = pos.x;
			int y = pos.y;
			while (encontrado && indice < pal.Length) {
				if (x < 0) { x = s.alto - 1; } else if(x >= s.alto) {x = 0;}
				if (y < 0) { y = s.ancho - 1; } else if(y >= s.ancho) { y = 0; }
				encontrado = s.matriz[x][y] == pal[indice];
				indice++;
				x += dir.x;
				y += dir.y;
			}

			return encontrado;
		}

		public static bool buscaDir(ref Sopa s, string pal, Par pos, out Par dir){
			Par[] direcciones = dirs ();
			bool dirEncontrada = false;
			int indice = 0;
			dir = new Par{ x=0,y=0};
			while (!dirEncontrada && indice < direcciones.Length) {
				Par dirActual = direcciones [indice++];
				if (compruebaPosDir (ref s, pal, pos, dirActual)) {
					dir = dirActual;
					dirEncontrada = true;
				}
			}
			return dirEncontrada;
		}

		public static bool buscaPal(ref Sopa s, string pal, out Par pos,out Par dir){
			Par p = new Par{x = 0, y = 0};
			bool posEncontrada = false;
			pos = p;
			dir = p;
			while(p.x < s.alto && !posEncontrada){
				p.y = 0;
				while (p.y < s.ancho && !posEncontrada) {
					if (buscaDir (ref s, pal, p, out dir)) {
						pos = p;
						posEncontrada = true;
					}
					p.y++;
				}
				p.x++;
			}
			return posEncontrada;
		}

		public static void Resuelve(ref Sopa s, string[] pals){
			for (int i = 0; i < pals.Length; i++) {
				string palabraActual = pals [i];
				Par pos, dir;
				if(buscaPal(ref s, palabraActual, out pos, out dir)){
					Console.WriteLine ("Encontrada "+ pals[i] + 
					                   " en posicion ("+ pos.x + ", " + pos.y + ")" + 
					                   " direccion (" + dir.x + ", " +dir.y + ")");
				}

			}

		}
	}
}
