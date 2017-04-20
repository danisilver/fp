using System.IO;
using System;
using System.Collections.Generic;

namespace Practica
{
	class MainClass
	{

		struct Alumno{
			public string Nombre, Apellido1, Apellido2;
			public int Telefono;
		}

		struct Listado{
			public int tam;
			public Alumno[] v;
		}

		static string nl = Environment.NewLine ;

		public static void Main (string[] args)
		{
			bool salir = false;

			Listado listado;
			Crea(out listado, 10);
			while (!salir) {
				Console.Write(
					"1)Introducir Alumno" + nl +
					"2)listar alumnos" + nl +
					"3)eliminar alumno" + nl +
					"4)cambiar telefono" + nl +
					"5)guardar" + nl +
					"6)cargar" + nl +
					"7)salir" + nl
				);

				char c = Console.ReadKey().KeyChar;
				Console.WriteLine ();
				switch (c) {
				case '1':
					Inserta(ref listado,CreaAlumno ());
					break;
				case '2':
					ListarAlumnos (listado);
					break;
				case '3':
					Console.WriteLine ("nombre");
					string nombre1 = Console.ReadLine ();
					Elimina(ref listado, nombre1);
					break;
				case '4':
					Console.WriteLine ("nombre");
					string nombre = Console.ReadLine ();
					Console.WriteLine ("telefono");
					int tel = int.Parse (Console.ReadLine ());
					CambiaTelefono (ref listado, nombre, tel);
					break;
				case '5':
					Vuelca (listado);
					break;
				case '6':
					Recuperar (ref listado);
					break;
				case '7':
					salir = true;
					break;
				default:
					break;
				}

			}
		}

		static void Crea(out Listado lst, int n){
			lst.tam = 0;
			lst.v = new Alumno[n];
		}

		static Alumno CreaAlumno(){
			Alumno a;
			Console.Write ("introduce nombre:");
			a.Nombre = Console.ReadLine ();
			Console.Write ("introduce apellido1:");
			a.Apellido1 = Console.ReadLine ();
			Console.Write ("introduce apellido2:");
			a.Apellido2 = Console.ReadLine ();
			Console.Write ("introduce telefono:");
			a.Telefono = int.Parse(Console.ReadLine ());
			return a;
		}

		static int Compara(Alumno a1, Alumno a2){
			return String.Concat (a1.Nombre, a1.Apellido1, a1.Apellido2).CompareTo (
				String.Concat(a2.Nombre, a2.Apellido1, a2.Apellido2)
			);
		}

		static bool Inserta(ref Listado list, Alumno al){
			if (list.tam < list.v.Length) {
				list.v [list.tam++] = al;
			} else {
				return false;
			}
			return true;
		}

		static bool Elimina(ref Listado list, string al){
			bool mover = false;
			for (int i = 0; i < list.tam; i++) {
				if(list.v[i].Nombre.ToLower().Equals(al) ){
					mover = true;
				}
				if (mover) {
					if(i+1 <= list.v.Length){
						list.v [i] = list.v [i+1];
					}
				}
			}
			if (mover) {
				list.tam--;
			}
			return mover;
		}

		static bool CambiaTelefono(ref Listado lst, string al, int telefono){
			bool encontrado = false;
			for (int i = 0; i < lst.tam; i++) {
				if (lst.v [i].Nombre.ToLower().Equals(al)) {
					encontrado = true;
					lst.v [i].Telefono = telefono;
				}
			}
			return encontrado;
		}

		static bool Vuelca(Listado lst){
			StreamWriter sw = new StreamWriter ("salida.txt");
			for (int i = 0; i <= lst.tam; i++) {
				sw.WriteLine (lst.v [i].Nombre);
				sw.WriteLine (lst.v [i].Apellido1);
				sw.WriteLine (lst.v [i].Apellido2);
				sw.WriteLine (lst.v [i].Telefono);
			}
			sw.Close ();
			return false;
		}

		static void Recuperar(ref Listado lst){
			StreamReader sr = new StreamReader ("salida.txt");
			int i = 0;
			while (!sr.EndOfStream) {
				Alumno a;
				a.Nombre = sr.ReadLine ();
				a.Apellido1 = sr.ReadLine ();
				a.Apellido2 = sr.ReadLine ();
				a.Telefono = int.Parse(sr.ReadLine ());
				lst.v [i] = a;
				i++;
			}
			lst.tam = i;
			sr.Close ();
		}

		static void ListarAlumnos(Listado lst){
			for (int i = 0; i < lst.tam; i++) {
				Console.WriteLine ("Alumno["+i+"]" + nl+ 
					"Nombre: " + lst.v[i].Nombre + nl +
					"Apellido1: " + lst.v[i].Apellido1 + nl +
					"Apellido2: " + lst.v[i].Apellido2 + nl +
					"Telefono: " + + lst.v[i].Telefono + nl
				);
			}
		}

		public static int[] frequencia(string cadena){
			int tam = ((int)'z' - (int)'a');
			int[] arr = new int[tam];

			for (int i = 0; i < cadena.Length; i++) {
				if (cadena[i] <= 'z' && cadena[i] >= 'a') {
					arr [((int)cadena [i] - (int)'a')]++;
				}
			}
			return arr;
		}

	}
}
