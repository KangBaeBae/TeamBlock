using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using b_TerminalProject.Library;



namespace b_TerminalProject
{
    
	class MainClass
    {
        static string _SetClass = "None";
        static string _Adress = "b_TerminalProject.Library.";
        static List<string> nameClass = new List<string>();

        static object Instance;

        public static void Main(string[] args)
        {
            var arr = from _type in Assembly.GetExecutingAssembly().GetTypes()
                      where _type.IsClass && _type.Namespace == "b_TerminalProject.Library"
                      select _type;
            arr.ToList().ForEach(_type => nameClass.Add(_type.Name));

            Stand();
        }

        static void CallMethod(string MethodName)
		{
			//var Instance = Assembly.GetExecutingAssembly().CreateInstance(_Adress + _SetClass);
            //Type.GetType(_Adress + _SetClass).GetMethod(MethodName, null).Invoke(Instance, null);
        }


        static void Stand()
        {
            if (_SetClass != "None")
                Console.Write("block.Terminal$" + _SetClass + "$ ");

            else
                Console.Write("block.Terminal$ ");
                

            string key = Console.ReadLine();

            if (key == "quit")
            {
                if (_SetClass != "None")
                {
                    _SetClass = "None";
                    Instance = null;
                    Stand();
                }

                else
                { }
            }

            else if (key == "Help")
            {
                Help();
                Stand();
            }

            else
            {
                try
                {
                    if (_SetClass == "None")
                        typeof(MainClass).GetMethod(key, BindingFlags.Public | BindingFlags.Static).Invoke(null, null);

                    else
                    {
                        Type.GetType(_Adress + _SetClass).GetMethod(key, BindingFlags.Public | BindingFlags.Instance).Invoke(Instance, null);
                    }
                }

                catch
                {
                    Console.WriteLine("*There is no function with the same name as the name you entered");
                    Console.WriteLine("*If you don't know about Method name, Enter the Help");
                }
                Stand();

            }
        }

        public static void Help()
        {
            if (_SetClass == "None")
            {
				MethodInfo[] cf = typeof(MainClass).GetMethods(BindingFlags.Public | BindingFlags.Static);
				for (int i = 0; i < cf.Length; i++)
					if (cf[i].Name != "Help" && cf[i].Name != "Main")
						Console.WriteLine(cf[i].Name);    
            }

            else
            {
                var arr = Type.GetType(_Adress + _SetClass).GetMethods(BindingFlags.Public | BindingFlags.Instance);
                for (int i = 0; i < arr.Length; i++)
                    if (arr[i].Name != "Equals" && arr[i].Name != "ToString" && arr[i].Name != "GetHashCode" && arr[i].Name != "GetType")
                    Console.WriteLine(arr[i].Name);
            }

        }

        public static void List()
		{
			for (int i = 0; i < nameClass.Count; i++)
				Console.WriteLine(nameClass[i]);
        }

        public static void SetClass()
        {
            Console.Write("Enter the class's name : ");
            string val = Console.ReadLine();

            if (nameClass.Contains(val) == true)
            {
                _SetClass = val;
                Instance = Assembly.GetExecutingAssembly().CreateInstance(_Adress + _SetClass);

            }

            else
                Console.WriteLine("It is nonexistent data");
                
        }
    }
}
