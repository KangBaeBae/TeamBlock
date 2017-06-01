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
		List<List<string>> db = new List<List<string>>();

		public void Add()
        {
			Console.Write("Enter Category : ");
			string category = Console.ReadLine();

			Console.Write("Enter value : ");
			string val = Console.ReadLine();

			if (db.Count >= 1)
			{
				for (int i = 0; i < db.Count; i++)
				{
					if (i == db.Count - 1 && db[i][0] != category)
					{
						db.Add(new List<string>());
						db[i + 1].Add(category);
						db[i + 1].Add(ToBinary(val));
						break;
					}

					else if (db[i][0] == category)
					{
						db[i].Add(ToBinary(val));
					}
				}
			}

			else
			{
				db.Add(new List<string>());
				db[0].Add(category);
				db[0].Add(ToBinary(val));
			}
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

	}

}
