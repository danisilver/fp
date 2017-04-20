using System;

namespace FP
{
	class MainClass
	{
		/*
		public static void Main (string[] args)
		{
			Random rnd = new Random (); //numeros aleatorios

			//declaracion de constantes y variables para el estado del juego
			const int ANCHO = 40;
			const int ALTO = 20;
			int jugadorX = rnd.Next (0, ANCHO), 
			jugadorY = ALTO - 1, 
			enemigoX = rnd.Next (0, ANCHO),
			enemigoY = rnd.Next (0, ALTO / 2),
			balaX = -1,
			balaY = 0,
			bombaX = -1,
			bombaY = 0;
			string enemigo = "***";
			string jugador = "@";
			string bala = "^";
			string bomba = "=";
			int delta = 30;



			//solicitud del valor delta

			//bucle principal del juego
			while (true) {
				Console.Clear ();
				if (Console.KeyAvailable) {
					string dir = (Console.ReadKey (true)).KeyChar.ToString ();


					//procesamiento del input de usuario
					if (dir.Equals ("a")) {
						jugadorX--;
					} else if (dir.Equals ("d")) {
						jugadorX++;
					} else if (dir.Equals ("1")) {
						if(balaY==-1){
							balaY = ALTO;
							balaX = jugadorX;
						}
					}
				}

				//logica del juego: movimiento del enemigo, bomba y bala

				if (enemigoX > 0 && enemigoX < ANCHO) {
					int mov = rnd.Next (-1, 2);
					enemigoX += mov;
				} else if (enemigoX <= 0) {
					int mov = rnd.Next (0, 2);
					enemigoX += mov;
				} else if (enemigoX >= ANCHO) {
					int mov = rnd.Next (-1, 1);
					enemigoX += mov;
				}

				if (enemigoY > 0 && enemigoY < ALTO / 2) {
					int mov = rnd.Next (-1, 2);
					enemigoY += mov;
				} else if (enemigoY <= 0) {
					int mov = rnd.Next (0, 2);
					enemigoY += mov;
				} else if (enemigoY >= ALTO / 2) {
					int mov = rnd.Next (-1, 1);
					enemigoY += mov;
				}

				if (balaY != -1) {
					balaY--;
				}


				if (bombaY >= ALTO) {
					bombaX = -1;
				} else {
					bombaY++;
				}

				if (bombaX == -1) {
					bombaX = enemigoX;
					bombaY = enemigoY;
				}




				//control de colisiones
				if (bombaY == jugadorY) {
					if (bombaX == jugadorX) {
						Console.Clear ();
						Console.WriteLine ("gana el enemigo");
						return;
					}
				}

				if (balaY == enemigoX) {
					if (balaX == enemigoY) {
						Console.Clear ();
						Console.WriteLine ("gana el jugador");
						return;
					}
				}


				//render
				for (int x = ALTO; x > 0; x--) {
					Console.SetCursorPosition (ANCHO, x);
					Console.Write ("|");
				}

				//dibujo del borde, enemigo, bomba, jugador, bala
				Console.SetCursorPosition (enemigoX, enemigoY);
				Console.Write (enemigo);

				if (balaY >=0 && balaX !=-1) {
					Console.SetCursorPosition (balaX, balaY);
					Console.Write (bala);
				}

				if (bombaX >=0) {
					Console.SetCursorPosition (bombaX, bombaY);
					Console.Write (bomba);
				}

				Console.SetCursorPosition (jugadorX, jugadorY);
				Console.Write (jugador);

				//retardo
				System.Threading.Thread.Sleep (delta);
			}//fin del bucle

			//informacion del ganador, despedida...
		}
*/
	}

}
