using System;
using System.IO;
using System.Collections.Generic;

namespace P4 {
	public class MainClass {
		struct Jugador{
			public string nombre;
			public int nivel;
		}

		public static void Main (string[] args) {

			Console.WriteLine("Bienvenido a PACMAN\nPulsa c para cargar una partida\n" +
				"pulsa C para cargar tus logros\n" +
				"pulsa n para empezar una nueva partida\n" +
				"pulsa r para ver la lista de jugadores\n" +
				"pulsa d para abrir el diseñador de niveles\n" +
				"cualquier otra tecla para salir");
			char tecla = Console.ReadKey (true).KeyChar;
			Jugador player = new Jugador (){nombre = "Noob", nivel=0 };
			Tablero t = null;
			if (tecla == 'c') {
                while (t == null) {
				Console.Write ("Introduce el nombre de tu partida: ");
				string partida = Console.ReadLine ();
				Console.Clear ();
                    try {
                        t = new Tablero(partida);
                    }
                    catch (Exception) {
                        Console.WriteLine("partida no encontrada");
                    }
                }
			} 
            
            else if (tecla == 'C') {
				Console.Write ("Introduce tu nombre de jugador: ");
				string playername = Console.ReadLine ();
				if (buscarJugador (playername, out player)) {
					Console.WriteLine ("encontrado");
				} else { 
					Console.WriteLine ("creado nuevo jugador: " + playername);
				}
				Console.WriteLine ("pulsa enter para cargar el nivel" + player.nivel);
				Console.ReadLine ();
				Console.Clear ();
				t = new Tablero ("Levels/level0" + player.nivel +".dat");
			} 
            
            else if (tecla == 'n') {
				Console.Clear ();
				Console.Write ("Introduce tu nombre de jugador o enter para jugar como Noob: ");
				string playername = Console.ReadLine ();
                if (playername.Replace(" ", "") == "") {
                    Console.WriteLine("juegas como Noob");
                } else if (buscarJugador (playername, out player)) {
					Console.WriteLine ("encontrado");
				}
				t = new Tablero ("Levels/level00.dat");
                Console.Clear();
            } 
            
            else if (tecla == 'r') {
				mostrarJugadores ();
				return;
			} 
            
            else if (tecla == 'd') {
				Console.WriteLine ("Escribe el nombre del fichero");
				string fichero = Console.ReadLine ();
				Console.Write("FILAS: ");
				int filas = int.Parse(Console.ReadLine ());
				Console.Write("COLUMNAS: ");
				int columnas = int.Parse(Console.ReadLine ());
				creaFicheroVacio (fichero,filas, columnas);
				t = new Tablero (fichero);
				t.setInteractivo (true);
				constructorInteractivo (t);
				return;
			} else {
				return;
			}

			t.Dibuja();
			int lap = 200; // retardo para bucle ppal
			char c = ' ';
			bool capture = false;
			bool salir = false;
			while (!capture && !salir)
			{
				while (!t.finNivel()&& !capture && !salir)
				{
					leeInput(ref c);
					if(c == 'g'){
						Console.Write ("Escribe el nombre de tu partida para guardar: ");
						string partida = Console.ReadLine ();
						t.guardar (partida);
						c = ' ';
					}
					if (c == 'p') {
						Console.WriteLine ("Pausa");
						Console.ReadLine ();
						c = ' ';
					} if (c == 'q') {
						salir = true;
						c = ' ';
					}
					if (c != ' ' && t.cambiaDir(c)) c = ' ';
					t.muevePacman();
					capture = t.captura();
					t.mueveFantasmas(lap);
					if (!capture)
						capture = t.captura();
                    
					t.Dibuja();
					System.Threading.Thread.Sleep(lap);
				}
				if (!capture && !salir)
				{
					Console.Clear();
					Console.WriteLine("Has completado el nivel");
					Console.ReadKey();
					Tablero t2;
					t2 = t;
					try {
						t2 = new Tablero("Levels/level0" + (t.numNivel + 1) + ".dat");
					} catch (FileNotFoundException ex) {
						Console.WriteLine ("archivo no encontrado: " + ex.FileName);
					}
					t = t2;
					guardarJugador (player.nombre, t.numNivel);
				}
			}
			Console.Clear();
			Console.WriteLine("FIN DEL JUEGO");
			Console.ReadKey();
		}

		static void guardarJugador(string playername, int nivelJugador){
			var d = new Dictionary<string, int> ();
			FileStream fs = File.Open ("jugadores.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamReader sr = new StreamReader (fs);
			while (!sr.EndOfStream) {
				d.Add(sr.ReadLine (), int.Parse(sr.ReadLine ()));
			}
            fs.SetLength(0);
			StreamWriter sw = new StreamWriter (fs);
            if (!d.ContainsKey(playername)) d.Add(playername, nivelJugador);
            else d[playername] = nivelJugador;
			foreach (string key in d.Keys) {
				sw.WriteLine (key);
				sw.WriteLine ("" + d [key]);
			}
            sw.Flush();
            sw.Close();
			fs.Close ();
		}

		static bool buscarJugador(string playername, out Jugador jugador){
			jugador = new Jugador (){nombre = playername, nivel=0 };
			bool encontrado = false;
			StreamReader sr = new StreamReader ("jugadores.txt");
			while (!sr.EndOfStream && !encontrado) {
				if (sr.ReadLine () == playername) {
					jugador.nombre = playername;
					jugador.nivel = int.Parse (sr.ReadLine ());
					encontrado = true;
				}
			}
			sr.Close ();

			return encontrado;
		}
		static void mostrarJugadores(){
			try{
				StreamReader sr = new StreamReader ("jugadores.txt");
				Console.Clear ();
				Console.WriteLine ("=========JUGADORES=======");
				while (!sr.EndOfStream) {
					Console.WriteLine (sr.ReadLine() + " ,nivel: " + sr.ReadLine());
				}
				sr.Close ();
			} catch(FileNotFoundException){
				Console.WriteLine ("fichero no encontrado");
			}
            Console.ReadLine( );
		}

		public static void leeInput (ref char dir) {
			if (Console.KeyAvailable) {
				ConsoleKeyInfo k = Console.ReadKey(true);
				switch (k.Key.ToString()) {
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
					dir = k.KeyChar;
					break;
				}
			}
		}

		static void creaFicheroVacio(string nombre, int filas, int cols){
			StreamWriter sw = new StreamWriter (nombre);
			for (int i = 0; i < filas; i++) {
				for (int j = 0; j < cols; j++) {
					sw.Write ('0');
				}
				sw.WriteLine ();
			}
			sw.Write('0');
			sw.Close ();
		}

		static void constructorInteractivo(Tablero t){
			bool salir = false;
			char tecla = ' ';
			Console.Clear();
			t.Dibuja ();
			while (!salir) {
				leeInput(ref tecla);
				if (tecla == 'q') {
					salir = true;
				} else if (tecla == 'b') {
					t.cambiaCasilla (Tablero.Casilla.Blanco);
				} else if (tecla == 'm') {
					t.cambiaCasilla (Tablero.Casilla.Muro);
				} else if (tecla == 'v') {
					t.cambiaCasilla (Tablero.Casilla.Vitamina);
				} else if (tecla == 'c') {
					t.cambiaCasilla (Tablero.Casilla.Comida);
				} else if (tecla == 'x') {
					t.cambiaCasilla (Tablero.Casilla.MuroCelda);
				} else if (tecla == '1') {
					t.cambiaPersonaje (1);
				} else if (tecla == '2') {
					t.cambiaPersonaje (2);
				} else if (tecla == '3') {
					t.cambiaPersonaje (3);
				} else if (tecla == '4') {
					t.cambiaPersonaje (4);
				} else if(tecla == 'g'){
					Console.Write ("Escribe el nombre de tu partida para guardar: ");
					string partida = Console.ReadLine ();
					t.guardar (partida);
				} 

				if (t.cambiaDir (tecla) && tecla != ' ') {
					tecla = ' ';
					t.muevePacman ();
					t.Dibuja ();
					Console.WriteLine ("b blanco\n" +
						"m muro\n" +
						"v vitamina\n" +
						"c comida \n" +
						"x murocelda\n" +
						"1-4 fantasmas\n");
				}

				System.Threading.Thread.Sleep(200);
			}
		}
	}

}

