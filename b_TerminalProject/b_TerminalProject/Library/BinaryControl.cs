using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Text;

namespace b_TerminalProject.Library
{

	class BinaryConvent
	{

		#region Public

		public void Add()
		{
			Console.Write("Enter Category : ");
			string category = Console.ReadLine();

			Console.Write("Enter value : ");
			string val = Console.ReadLine();

            if (CategoryIndex(category) == -1)
            {
                List<string> instance = new List<string>();
                instance.Add(category);
                instance.Add(val);
                db.Add(instance);
            }

            else
                Add(category,val);
            

		}

		public void ShowCategory()
		{
			for (int i = 0; i < db.Count; i++)
			{
				Console.Write("\t" + (i + 1) + " Category\t:\t" + db[i][0]);
				for (int j = 1; j < db[i].Count; j++)
				{
					Console.WriteLine();
					Console.Write("\t\t   Value : " + db[i][j]);
				}
				Console.WriteLine();
			}
		}


		public void Save()
		{
			Console.Write("Enter the path : ");
			string path = Console.ReadLine();

			try
			{
				Serialize(path, db);
			}

			catch
			{
				Console.WriteLine("Path Error");
			}

		}

		public void Load()
		{
			Console.Write("Enter the path : ");
			string path = Console.ReadLine();

			try
			{
				db = DeSerialize(path);
			}

			catch
			{
				Console.WriteLine("Path Error");
			}
		}


		#endregion



		#region Private


        // Array, Index Number is '0', is Category data
		List<List<string>> db = new List<List<string>>();

		string ToBinary(string data)
		{
			byte[] binary_byte = Encoding.Default.GetBytes(data);
			string binary_string = string.Empty;

			for (int i = 0; i < binary_byte.Length; i++)
				binary_string += binary_byte[i];

			return binary_string;
		}

		void Serialize(string path, Object obj)
		{
			Stream stream = File.Open(path, FileMode.Create);

			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stream, obj);

			stream.Close();
		}
		List<List<string>> DeSerialize(string path)
		{
			Stream st = File.Open(path, FileMode.Open);

			BinaryFormatter bf = new BinaryFormatter();
			List<List<string>> mem = (List<List<string>>)bf.Deserialize(st);

			st.Close();

			return mem;
		}

		int Similarity(string value1, string value2)
		{
			int[,] array = new int[value1.Count() + 1, value2.Count() + 1];

			char[] arr1 = value1.Trim().ToCharArray();
			char[] arr2 = value2.Trim().ToCharArray();


			for (int i = 1; i <= arr1.Length; i++)
				array[i, 0] = i;

			for (int i = 1; i <= arr2.Length; i++)
				array[0, i] = i;


			for (int i = 1; i <= arr1.Length; i++)
				for (int j = 1; j <= arr2.Length; j++)
					if (arr1[i - 1] == arr2[j - 1]) array[i, j] = array[i - 1, j - 1];
					else array[i, j] = Math.Min(array[i - 1, j - 1] + 1, Math.Min(array[i, j - 1] + 1, array[i - 1, j] + 1));

			return array[arr1.Length, arr2.Length];
		}

        string equlData(string Category)
        {
            List<string> Instance = new List<string>();

            for (int i = 0; i < db[CategoryIndex(Category)][1].Length; i++)
            {
                for (int j = 0; j < db[CategoryIndex(Category)][0].Length; j++)
                {
                    if ()
                }
            }

            return null;

        }

        void Add(string Category, string data)
        {
            int TargetIndex = CategoryIndex(Category);

            db[TargetIndex].RemoveAt(0);
            db[TargetIndex].Sort(delegate (string A, string B)
            {
                if (Similarity(data, A) > Similarity(data, B)) return 1;
                else if (Similarity(data, A) < Similarity(data, B)) return -1;
                else return 0;
            });

            db[TargetIndex].Insert(0, data);
            db[TargetIndex].Insert(0, Category);
        }

        int CategoryIndex(string Category)
        {
            int Index = -1;

            for (int i = 0; i < db.Count; i++)
                if (Category == db[i][0])
                    Index = i;

            return Index;
        }

		#endregion


	}

}
