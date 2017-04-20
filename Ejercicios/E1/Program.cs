using System;


namespace E1
{

	/*
* Implementar un método que reciba una matriz cuadrada y determine si es simétrica
*/


	class MainClass
	{

		//1. BUCLE PRINCIPAL
		public static void Main (string[] args)
		{
			int[,] m1 = {{1,2,3,4},{2,5,6,7},{3,6,8,9},{4,7,9,10}};
			int[,] m2 = {{1,2,3,4},{5,6,7,8},{9,10,11,12},{13,14,15,16}};
			int[,] m3 = { {-8, -1, 3}, {-1, 7, 4}, {3, 4, 9}};

			DibujaMatriz (m2);
			bool simetrica = MatrizSimétrica2 (m2);

			if (simetrica)
				Console.WriteLine ("Matriz simétrica");
			else
				Console.WriteLine ("Matriz no simétrica");

		}

		//3. DIBUJA LA MATRIZ
		static void DibujaMatriz (int[,] matriz)
		{

			//Dibujamos filas
			for (int i = 0; i < matriz.GetLength (0); i++) {
				//Dibujamos columnas
				for (int j = 0; j < matriz.GetLength (1); j++)
					Console.Write (matriz [i, j] + " ");

				//Saltamos la línea
				Console.WriteLine ();
			}
		}


		//4. COMPRUEBA SI ES SIMÉTRICA
		/*
		 * Este método solo recorre la mitad superior derecha de la matriz, comprobando
		 * si cada elemento es igual o no al correspondiente en el otro lado de la diagonal
		*/

		static bool MatrizSimétrica (int[,] matriz)
		{
			bool simetrica = true;

			int numElementos = matriz.GetLength (0);
			int fila = 1, columna = 0;

			while ((fila < numElementos) && simetrica) {
				if (matriz [fila, columna] != matriz [columna, fila])
					simetrica = false;

				if ((columna + 1) % fila == 0) {
					fila++;
					columna = 0;
				} else {
					columna++;
				}
			}

			return simetrica;
		}

		static bool MatrizSimétrica2 (int[,] matriz)
		{
			bool simetrica = true;
			int numFilas = matriz.GetLength (0);
			int fila = 0, columna;

			while(fila < numFilas && simetrica){
				columna = 0;
				while(columna < fila && simetrica){
					simetrica = matriz [fila, columna] == matriz [columna, fila];
					columna++;
				}
				fila++;
			}

			return (simetrica);
		}

	}

}