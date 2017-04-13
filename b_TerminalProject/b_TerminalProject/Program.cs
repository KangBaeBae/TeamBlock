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



namespace b_TerminalProject
{

    class StandBy
    {

        #region Kernel

        public void Combine()
        {
            Console.Write("Set format : ");
            string value = Console.ReadLine();

            if (value == "string")
                Combine_String();

            else if (value == "bool")
                Combine_Bool();

            else if (value == "quit"){}

            else
            {
                Console.WriteLine("The value you entered is invalid. Please re-enter");
                Combine();
            }

        }

        void Combine_String()
        {
            Console.Write("Enter value : ");
            string value1 = Console.ReadLine();

            Console.Write("Enter value : ");
            string value2 = Console.ReadLine();

            char[] arr1 = value1.Trim().ToCharArray();
            char[] arr2 = value2.Trim().ToCharArray();
            List<char> arr3 = new List<char>();

            Console.Write("Origin : ");
            for (int i = 0; i < arr1.Length; i++)
                Console.Write(arr1[i] + " ");
            Console.WriteLine();

            Console.Write("Origin : ");
            for (int i = 0; i < arr2.Length; i++)
                Console.Write(arr2[i] + " ");
            Console.WriteLine();

            for (int i = 0; i < arr1.Length; i++)
            {
                if (i >= arr2.Length)
                    arr3.Add(arr1[i]);

                else if (arr1[i] == arr2[i])
                    arr3.Add(arr1[i]);

                else
                {
                    arr3.Add(arr1[i]);

                    if (i < arr2.Length)

                        arr3.Add(arr2[i]);
                }
            }

            for (int i = arr1.Length; i < arr2.Length; i++)
            {
                if (i < arr2.Length)

                    arr3.Add(arr2[i]);
            }


            Console.Write("Convent : ");
            for (int i = 0; i < arr3.Count; i++)
                Console.Write(arr3[i] + " ");
            Console.WriteLine();
        }

        void Combine_Bool()
        {
            Console.Write("Enter value : ");
            string value1 = Console.ReadLine();

            Console.Write("Enter value : ");
            string value2 = Console.ReadLine();

            List<bool> arr1 = Tobool(value1);
            List<bool> arr2 = Tobool(value2);
            List<bool> arr3 = new List<bool>();


            Console.Write("Origin : ");
            for (int i = 0; i < arr1.Count; i++)
                Console.Write(arr1[i] + " ");
            Console.WriteLine();

            Console.Write("Origin : ");
            for (int i = 0; i < arr2.Count; i++)
                Console.Write(arr2[i] + " ");
            Console.WriteLine();

            for (int i = 0; i < arr1.Count; i++)
            {
                if (i >= arr2.Count)
                    arr3.Add(arr1[i]);

                else if (arr1[i] == arr2[i])
                    arr3.Add(arr1[i]);

                else
                {
                    arr3.Add(arr1[i]);

                    if (i < arr2.Count)

                        arr3.Add(arr2[i]);
                }
            }

            for (int i = arr1.Count; i < arr2.Count; i++)
            {
                if (i < arr2.Count)

                    arr3.Add(arr2[i]);
            }


            Console.Write("Convent : ");
            for (int i = 0; i < arr3.Count; i++)
                Console.Write(arr3[i] + " ");
            Console.WriteLine();
        }

        public void Compare()
        {
            int[,] array = new int[1001, 1001];

            Console.Write("Enter the data : ");
            string value1 = Console.ReadLine();

            Console.Write("Enter the data : ");
            string value2 = Console.ReadLine();
            Console.WriteLine("");

            char[] st_arr1 = value1.Trim().ToCharArray();
            char[] st_arr2 = value2.Trim().ToCharArray();

            for (int i = 1; i <= st_arr1.Length; i++)
                array[i, 0] = i;

            for (int i = 1; i <= st_arr2.Length; i++)
                array[0, i] = i;


            for (int i = 1; i <= st_arr1.Length; i++)
            {
                for (int j = 1; j <= st_arr2.Length; j++)
                {
                    if (st_arr1[i - 1] == st_arr2[j - 1]) array[i, j] = array[i - 1, j - 1];
                    else array[i, j] = Math.Min(array[i - 1, j - 1] + 1, Math.Min(array[i, j - 1] + 1, array[i - 1, j] + 1));
                }
            }


            for (int i = 0; i <= st_arr1.Length; i++)
            {
                for (int j = 0; j <= st_arr2.Length; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            // array[st_arr1.Length, st_arr2.Length] == 유사도 결과값.
            Console.WriteLine("Last Num : " + array[st_arr1.Length, st_arr2.Length]);
            Console.WriteLine("Return : " + Compare(value1, value2));
        }

        public void Format()
        {
            Console.Write("Enter value : ");
            string value1 = Console.ReadLine();

            Console.Write("Enter value : ");
            string value2 = Console.ReadLine();

            List<bool> arr1 = Tobool(value1);
            List<bool> arr2 = Tobool(value2);


            List<List<bool>> fm_arr1 = Formatting(arr1, arr2);
            List<List<bool>> fm_arr2 = Formatting(arr2, arr1);

            for (int i = 0; i < fm_arr1.Count; i++)
            {
                Console.WriteLine("Arr1 Index : " + i);
                for (int j = 0; j < fm_arr1[i].Count; j++)
                {
                    Console.Write(fm_arr1[i][j] + " ");
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine();

            for (int i = 0; i<fm_arr2.Count; i++)
            {
                Console.WriteLine("Arr2 Index : " + i);
                for (int j = 0; j<fm_arr2[i].Count; j++)
                {
                    Console.Write(fm_arr2[i][j] + " ");
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine();


        }

        #endregion

        #region General

        public void Add()
        {
            string num;
            double num1;
            double num2;
            bool isNum;

            Console.Write("Num1 = ");
            num = Console.ReadLine();
            isNum = double.TryParse(num, out num1);
            if (isNum != true)
            {
                Console.WriteLine("This isn't number");
                Add();
            }

            else
            {
                Console.Write("Num2 = ");
                num = Console.ReadLine();
                isNum = double.TryParse(num, out num2);
                if (isNum != true)
                {
                    Console.WriteLine("This isn't number");
                    Add();
                }

                else
                {
                    Console.Write("result is ");
                    Console.Write(num1 + num2);
                    Console.Write(" \n");
                }
            }
        }

        public void Minus()
        {
            string num;
            double num1;
            double num2;
            bool isNum;

            Console.Write("Num1 = ");
            num = Console.ReadLine();
            isNum = double.TryParse(num, out num1);
            if (isNum != true)
            {
                Console.WriteLine("This isn't number");
                Minus();
            }

            else
            {
                Console.Write("Num2 = ");
                num = Console.ReadLine();
                isNum = double.TryParse(num, out num2);
                if (isNum != true)
                {
                    Console.WriteLine("This isn't number");
                    Minus();
                }

                else
                {
                    Console.Write("result is ");
                    Console.Write(num1 - num2);
                    Console.Write(" \n");
                }
            }
        }

        public void Convent()
        {
            Console.Write("Enter the string : ");
            byte[] ascii = Encoding.ASCII.GetBytes(Console.ReadLine());

            foreach (byte b in ascii)
            {
                Console.Write(string.Format("0x{0:x8}", b));
                Console.Write(" ");
                Console.Write(string.Format("0x{0:X}", b));
                Console.Write(" ");
                Console.WriteLine(((Char)b).ToString());
            }

            foreach (byte b in ascii)
            {
                Console.Write(string.Format("0x{0:X}", b));
            }

            Console.WriteLine("");
            Console.WriteLine(Encoding.ASCII.GetString(ascii));

        }

        public void Clean()
        {
            for (int i = 0; i < 80; i ++)
            {
                Console.WriteLine("");
            }
        }

        public void ArrayToString()
        {
            string[] array = new string[] { "Hi", "My", "Name", "Apple" };



            StringBuilder builder = new StringBuilder();

            Console.Write("Array : ");

            foreach (string value in array)
            {
                Console.Write(value + " ");
                builder.Append(value);
                builder.Append("3");
            }

            Console.WriteLine("");
            Console.WriteLine("String : " + builder.ToString());
        }

        #endregion

        #region private

        bool IsInclude(char[] array, char value)
        {
            bool bl = false;
            for (int i = 0; i < array.Length; i++)
                if (array[i] == value)
                    bl = true;

            return bl;
        }

        bool IsInclude(List<char> array, char value)
        {
            bool bl = false;
            for (int i = 0; i < array.Count; i++)
                if (array[i] == value)
                    bl = true;

            return bl;
        }

        int Compare(string value1, string value2)
        {
            int[,] array = new int[1001, 1001];

            char[] arr1 = value1.Trim().ToCharArray();
            char[] arr2 = value2.Trim().ToCharArray();


            for (int i = 1; i <= arr1.Length; i++)
                array[i, 0] = i;

            for (int i = 1; i <= arr2.Length; i++)
                array[0, i] = i;


            for (int i = 1; i <= arr1.Length; i++)
            {
                for (int j = 1; j <= arr2.Length; j++)
                {
                    if (arr1[i - 1] == arr2[j - 1]) array[i, j] = array[i - 1, j - 1];
                    else array[i, j] = Math.Min(array[i - 1, j - 1] + 1, Math.Min(array[i, j - 1] + 1, array[i - 1, j] + 1));
                }
            }

            return array[arr1.Length, arr2.Length];
        }


        List<char> Combine(string value1, string value2)
        {
            char[] arr1 = value1.Trim().ToCharArray();
            char[] arr2 = value2.Trim().ToCharArray();
            List<char> arr3 = new List<char>();


            for (int i = 0; i < arr1.Length; i++)
                if (IsInclude(arr3, arr1[i]) == false)
                    arr3.Add(arr1[i]);


            for (int i = 0; i < arr2.Length; i++)
                if (IsInclude(arr3, arr2[i]) == false)
                    arr3.Add(arr2[i]);



            for (int i = 0; i<arr1.Length; i++)
            {
                if (i >= arr2.Length)
                    arr3.Add(arr1[i]);

                else if (arr1[i] == arr2[i])
                    arr3.Add(arr1[i]);

                else
                {
                    arr3.Add(arr1[i]);

                    if (i < arr2.Length)
						arr3.Add(arr2[i]);
                }
            }

            for (int i = arr1.Length; i<arr2.Length; i++)
            {
                if (i<arr2.Length)
					arr3.Add(arr2[i]);
            }

            return arr3;
            
        }

        List<bool> Combine(List<bool> value1, List<bool> value2)
        {
            List<bool> arr = new List<bool>();

            for (int i = 0; i < value1.Count; i++)
            {
                if (i >= value2.Count)
                    arr.Add(value1[i]);

                else if (value1[i] == value2[i])
                    arr.Add(value1[i]);

                else
                {
                    arr.Add(value1[i]);

                    if (i < value2.Count)
                        arr.Add(value2[i]);
                }
            }

            return arr;
        }

        List<bool> Tobool(string value)
        {
            List<bool> arr = new List<bool>();

            List<char> _value = new List<char>(value.Trim().ToCharArray());

            for (int i = 0; i < _value.Count; i++)
            {
                if (_value[i].ToString() == "0")
                    arr.Add(false);

                else if (_value[i].ToString() == "1")
                    arr.Add(true);

                else
                    break;
            }

            if (arr.Count == value.Length)
                return arr;

            else
                return null;
        }

        List<bool> Tobool(int[] value)
        {
            List<bool> arr = new List<bool>();

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == 0)
                    arr.Add(false);

                else if (value[i] == 1)
                    arr.Add(true);

                else
                    break;
            }

            if (arr.Count == value.Length)
                return arr;

            else
                return null;
        }

        List<List<bool>> Formatting(List<bool> target, List<bool> criterion)
        {
            List<List<bool>> val = new List<List<bool>>();
            List<bool> ist = new List<bool>();

            /*
            bool IsDone = false;
            int IndexCrite = 0;

            int IndexTarget = 0;
            int CountTarget = 0;

            while (IsDone != true)
            {
                for (int i = 0; i < target.Count; i++)
                {
                    
                    if (target[i] == criterion[IndexCrite])
                    {
                        bool IsEqual = true;
                        int _index = i;

                        while (IsEqual == true)
                        {
                            if (_index < target.Count && _index < criterion.Count)
                            {
                                if (target[_index] == criterion[IndexCrite])
                                {
                                    ist.Add(target[_index]);
                                    _index++;
                                }

                                else
                                    IsEqual = false;
                            }

                            else
                                IsEqual = false;
                        }

                        if (CountTarget < ist.Count)
                        {
                            CountTarget = ist.Count;
                            IndexTarget = i;
                        }

                        ist = null;
                        ist = new List<bool>();
                    }
                }

                bool IsEqualfmt = true;

                for (int i = 0; IsEqualfmt == true; i++)
                {
                    
                    if (IndexCrite + i < criterion.Count || IndexCrite + i < target.Count)
                    {
                        if (criterion[IndexCrite + i] == target[IndexTarget + i])
                            ist.Add(target[IndexTarget + i]);

                        else
                        {
                            IsEqualfmt = false;
                            IndexTarget += i;
                            IndexCrite += i;
                        }
                    }

                    else
                    {
                        IsEqualfmt = false;
                        IndexTarget += i - 1;
                        IndexCrite += i - 1;
                    }
                }

                for (int i = 0; IsEqualfmt != true; i++)
                {
                    if (IndexCrite + i < criterion.Count || IndexCrite + i < target.Count)
                    {
                        if (criterion[IndexCrite + i] != target[IndexTarget + i])
                            ist.Add(target[IndexTarget + i]);

                        else
                        {
                            ist.Add(target[IndexTarget + i]);

                            IsEqualfmt = true;
                            IndexCrite += i;
                        }
                    }

                    else
                    {
                        IsEqualfmt = true;
                        IndexCrite += i - 1;



                    }
                }



                val.Add(ist);
                ist = null;
                ist = new List<bool>();
            }
            */


            bool IsDone = false;
            int IndexTarget = 0;

            int IndexCrite = 0;
            int CountCrite = 0;

            while (IsDone != true)
            {

                // One
                for (int i = 0; i < criterion.Count; i++)
                {
                    if (target[IndexTarget] == criterion[i])
                    {
                        bool IsEqual = true;
                        int _i = i;

                        while(IsEqual)
                        {
                            if (IndexTarget + _i < target.Count && i + _i < criterion.Count)
                            {
                                if (target[IndexTarget + _i] == criterion[i + _i])
                                {
                                    ist.Add(target[IndexTarget]);
                                    _i++;
                                }

                                else
                                    IsEqual = false;
                            }

                            else
                                IsEqual = false;
                        }

                        if (CountCrite < ist.Count)
                        {
                            CountCrite = ist.Count;
                            IndexCrite = i;
                        }

                        ist = null;
                        ist = new List<bool>();
                    }
                }

                // Two
                bool IsEqualfmt = true;
                for (int i = 0; IsEqualfmt == true; i++)
                {
                    if (IndexTarget + i < target.Count && IndexCrite + i < criterion.Count)
                    {
                        if (target[IndexTarget + i] == criterion[IndexCrite + i])
                            ist.Add(target[IndexTarget + i]);

                        else
                        {
                            IsEqualfmt = false;
                            IndexCrite += i;
                            IndexTarget += i;
                        }
                    }

                    else
                    {
                        IsEqualfmt = false;
                        IndexCrite += i;
                        IndexTarget += i;
                    }
                }

                // Three
                for (int i = 0; IsEqualfmt != true; i++)
                {
                    if (IndexTarget + i < target.Count && IndexCrite + i < criterion.Count)
                    {
                        if (target[IndexTarget + i] != criterion[IndexCrite + i])
                            ist.Add(target[IndexTarget + i]);

                        else
                        {
                            ist.Add(target[IndexTarget + i]);
                            IsEqualfmt = true;
                            IndexTarget += i;
                            IndexCrite = 0;
                            CountCrite = 0;
                        }
                    }

                    else
                    {
                        IsEqualfmt = true;

                        if (IndexTarget + i >= target.Count)
                            IsDone = true;

                        else if (IndexCrite + i >= target.Count)
                        {
                            IndexTarget += i;
                            IndexCrite = 0;
                            CountCrite = 0;
                        }
                        
                    }
                }

                val.Add(ist);
                ist = null;
                ist = new List<bool>();

            }


            return val;
        }


        #endregion

    }

  

    class MainClass
    {

        public static void Main(string[] args)
        {
            Stand();
        }

        static void Stand()
        {
            Console.Write("block.Terminal$ ");
            string key = Console.ReadLine();
            if (key != "quit")
            {
                Search(key);
                Stand();
            }

            else {}
        }

        static void Search(string value)
        {
            try
            {
                StandBy sb = new StandBy();
                Type type = sb.GetType();
                MethodInfo callFunc = type.GetMethod(value, BindingFlags.Instance | BindingFlags.Public);
                callFunc.Invoke(sb, null);
            }
            catch
            {
                Console.WriteLine("*There is no function with the same name as the name you entered");
            }



        }

    }
}
