using System;

namespace ordenacion {
	class MainClass {
		public static void Main (string[] args) {
			int[] arr = new int[]{9, 8, 7, 6, 5, 4, 3, 2, 1, 0};
			bubble (arr);
			foreach (var item in arr) {
				Console.Write(item + ", ");
			}
			Console.WriteLine ();
		}

		public static void insercion(int[] arr){
			for (int i = 1; i < arr.Length; i++) {
				int temp = arr [i];
				int j = i - 1;
				while (j>=0 && arr[j] > temp) {
					arr[j+1] = arr[j];
					j--;
				}
				arr [j + 1] = temp;
			}
		}

		public static void selection(int[] arr){
			for (int i = 0; i < arr.Length - 1; i++) {
				int min = i;
				for (int j = i + 1; j < arr.Length; j++) {
					if (arr [j] < arr [min])
						min = j;
				}

				int temp = arr [i];
				arr [i] = arr[min];
				arr [min] = temp;
				
			}
		}

		public static void bubble(int[] arr){
			bool cont = true;
			int n = arr.Length;
			int i = 0;
			while ((i < n - 1) && cont) {
				cont = false;
				for (int j = n - 1; j > i; j--) {
					if(arr[j - 1] > arr[j]){
						int temp = arr[j];
						arr [j] = arr [j - 1];
						arr [j - 1] = temp;
						cont = true;
					}
				}
				i++;
			}
		}
	}
}
