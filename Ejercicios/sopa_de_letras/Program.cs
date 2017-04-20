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
			Resuelve (s, pals);
		}

		public static Par[] dirs(){
			
		}

		public static bool compruebaPosDir(Sopa s, string pal, Par pos, Par dir){

		}

		public static bool buscaDir(Sopa s, string pal, Par pos, Par dir){
			
		}

		public static void Resuelve(Sopa s, string[] pals){
			
		}
	}
}
