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
		}

		public static Par[] dirs(){
			return new Par[]{ 
				new Par{x=-1, y=-1},
				new Par{x=-1, y= 0},
				new Par{x=-1, y= 1},
				new Par{x= 0, y=-1},
				new Par{x= 0, y= 0},
				new Par{x= 0, y= 1},
				new Par{x= 1, y=-1},
				new Par{x= 1, y= 0},
				new Par{x= 1, y= 1}
			};
		}

		public static bool compruebaPosDir(ref Sopa s, string pal, Par pos, Par dir){
			bool encontrado = true;
			int indice = 0;
			while (encontrado && indice < pal.Length) {
				encontrado = s.matriz[dir.x % s.ancho, dir.y % s.alto] = pal[indice];
				indice++;
				dir.x += dir.x;
				dir.y += dir.y;
			}
			return encontrado;
		}

		public static bool buscaDir(ref Sopa s, string pal, Par pos, out Par dir){
			
			return false;
		}

		public static bool buscaPal(ref Sopa s, string pal, Par pos, Par dir){

			return false;
		}

		public static void Resuelve(ref Sopa s, string[] pals){
			for (int i = 0; i < pals.Length; i++) {
				string actual = pals [i];
				Par dir;
				for (int j = 0; j < dirs().Length; j++) {
					dir = dirs()[j];
					if(buscaDir (s, actual, pos, out dir)){
						if (buscaPal (s, actual, pos, dir)) {
							Console.WriteLine ("Encontrada "+ pals[i] + "en posicion ("+ x + ", " + y + ")" + "direccion (" + dir.x + ", " +dir.y + ")");
						}
					}
				}
			}

		}
	}
}
