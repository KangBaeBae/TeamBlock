using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KroglpProject
{

    class Kernel
    {
        #region Processing

        public Kernel(string path)
        {
            try
            {
                Path = path;
                Conscious = DeSerialize(path);
            }

            catch
            {
                Path = path;
                Conscious = new List<List<List<bool>>>();
            }

        }

        public void Process(string value)
        {
            List<bool> Event = Tobool(value);
            instance = null;

            if (Conscious.Count != 0)
            {
                for (int i = 0; i < Conscious.Count; i++)
                {
                    List<bool> Memory = DMConvert(Conscious[i]);

                    List<List<bool>> _eve = Formatting(Event, Memory);
                    List<List<bool>> _mem = Formatting(Memory, Event);

                    Conscious.Insert(i, _mem);
                    Conscious.RemoveAt(i + 1);

                    if (i == 0)
                        instance = Compare(_eve, _mem);

                    else
                        instance = Compare(Compare(_eve, _mem), instance);
                }

                Conscious.Add(instance);
            }

            else
            {
                instance = new List<List<bool>>();
                instance.Add(Event);

                Conscious.Add(instance);
            }
        }
        public void Process(int[] value)
        {
            List<bool> Event = Tobool(value);
            instance = null;

            if (Conscious.Count == 0)
            {
                for (int i = 0; i < Conscious.Count; i++)
                {
                    List<bool> Memory = DMConvert(Conscious[i]);

                    List<List<bool>> _eve = Formatting(Event, Memory);
                    List<List<bool>> _mem = Formatting(Memory, Event);

                    Conscious.Insert(i, _mem);
                    Conscious.RemoveAt(i + 1);

                    if (i == 0)
                        instance = Compare(_eve, _mem);

                    else
                        instance = Compare(Compare(_eve, _mem), instance);
                }

                Conscious.Add(instance);
            }


            else
            {
                instance = new List<List<bool>>();
                instance.Add(Event);

                Conscious.Add(instance);
            }
        }

        public List<List<List<bool>>> Return() { return _coc; }

        public void Close()
        {
            Serialize(Path, Conscious);
        }
        public void Close(string path)
        {
            Serialize(path, Conscious);
        }

        #endregion

        #region Properties

        List<List<List<bool>>> _coc { get { return Conscious; } }
        List<List<List<bool>>> Conscious;
        //List<List<List<bool>>> UnConscious;
        List<List<bool>> instance = new List<List<bool>>();

        bool IsProcess { get; set; }
        string Path { get; set; }

        #endregion

        #region Method

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

        int Similarity(string value1, string value2)
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
        int Similarity(List<bool> value1, List<bool> value2)
        {
            string val1 = BoolToInt(value1);
            string val2 = BoolToInt(value2);

            return Similarity(val1, val2);
        }
        int Similarity(List<List<bool>> _value1, List<List<bool>> _value2)
        {
            string value1 = BoolToInt(_value1);
            string value2 = BoolToInt(_value2);

            return Similarity(value1, value2);
        }

        string BoolToInt(List<bool> value)
        {
            string val = string.Empty;

            for (int i = 0; i < value.Count; i++)
            {
                if (value[i] == true)
                    val += "1";

                else
                    val += "0";
            }

            return val;
        }
        string BoolToInt(List<List<bool>> value)
        {
            List<bool> val1 = new List<bool>();

            for (int i = 0; i < value.Count; i++)
            {
                for (int j = 0; j < value[i].Count; j++)
                {
                    val1.Add(value[i][j]);
                }
            }

            return BoolToInt(val1);
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

        List<bool> DMConvert(List<List<bool>> value)
        {
            List<bool> ins = new List<bool>();

            for (int i = 0; i < value.Count; i++)
                for (int j = 0; j < value[i].Count; j++)
                    ins.Add(value[i][j]);

            return ins;
        }

        List<List<bool>> Formatting(List<bool> target, List<bool> criterion)
        {
            List<List<bool>> val = new List<List<bool>>();
            List<bool> ist = new List<bool>();

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
                        int _i = 0;

                        while (IsEqual == true)
                        {
                            if (IndexTarget + _i < target.Count && i + _i < criterion.Count)
                            {
                                if (target[IndexTarget + _i] == criterion[i + _i])
                                {
                                    ist.Add(target[IndexTarget]);
                                    _i++;
                                }

                                else
                                {
                                    IsEqual = false;
                                }
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

                    else if (IndexTarget + i >= target.Count)
                    {
                        IsDone = true;
                        break;
                    }

                    else if (IndexCrite + i >= criterion.Count)
                    {
                        IndexCrite = 0;
                        CountCrite = 0;

                        IndexTarget += i;
                        break;
                    }
                }
                // Three
                for (int i = 0; IsEqualfmt != true; i++)
                {
                    if (IndexTarget + i < target.Count)
                    {
                        if (target[IndexTarget + i] != criterion[IndexCrite])
                        {
                            ist.Add(target[IndexTarget + i]);
                        }

                        else
                        {
                            ist.Add(target[IndexTarget + i]);
                            IsEqualfmt = true;
                            IndexCrite = 0;
                            CountCrite = 0;
                            if (IndexTarget + i >= target.Count - 1)
                                IsDone = true;

                            else
                                IndexTarget += i + 1;
                        }
                    }

                    else
                    {
                        IsEqualfmt = true;
                        IsDone = true;
                    }
                }
                val.Add(ist);
                ist = null;
                ist = new List<bool>();

            }

            return val;
        }
        List<List<bool>> Compare(List<List<bool>> eve, List<List<bool>> mem)
        {

            List<List<bool>> rsl = new List<List<bool>>();

            int Count = 0;
            for (int i = 0; i < eve.Count; i++)
            {
                for (int j = 0; j < mem.Count; j++)
                    if (Count < Similarity(eve[i], mem[j]))
                        Count = j;
                    
                rsl.Add(Combine(eve[i], mem[Count]));
            }

            return rsl;
        }

        void Serialize(string path, Object obj)
        {
            Stream stream = File.Open(path, FileMode.Create);

            BinaryFormatter bF = new BinaryFormatter();
            bF.Serialize(stream, obj);

            stream.Close();
        }
        Object DeSerialize(string path, Object obj)
        {
            Stream st = File.Open(path, FileMode.Open);

            BinaryFormatter bF = new BinaryFormatter();
            obj = bF.Deserialize(st);

            st.Close();

            return obj;
        }
        List<List<List<bool>>> DeSerialize(string path)
        {
            Stream st = File.Open(path, FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();
            List<List<List<bool>>> mem = (List<List<List<bool>>>)bf.Deserialize(st);

            st.Close();

            return mem;
        }

        #endregion
    }

    static class KernelAPI
    {
        static Kernel _Kernel;

        public static void Initialize(string path)
        {
            _Kernel = new Kernel(path);
        }


        public static void Input(string data) { _Kernel.Process(data); }
        public static void Input(int[] data) { _Kernel.Process(data); }

        public static List<List<List<bool>>> Output() { return _Kernel.Return(); }

        public static void Save() { _Kernel.Close(); }
    }


    class MainClass
    {
        public static void Main(string[] args)
        {
            Stand();
        }

        public static void Stand()
        {
            Console.Write("block : ");
            string command = Console.ReadLine();

            if (command == "Path")
            {
                Console.Write("Enter the value : ");
                KernelAPI.Initialize(Console.ReadLine());
            }

            else if (command == "Input")
            {
                Console.Write("Enter the value : ");
                KernelAPI.Input(Console.ReadLine());
            }

            else if (command == "Output")
            {
                for (int i = 0; i < KernelAPI.Output().Count; i++)
                {
                    Console.WriteLine("Mem " + i);
                    for (int j = 0; j < KernelAPI.Output()[i].Count; j++)
                    {
                        Console.Write("\t" + j + " : ");

                        for (int k = 0; k < KernelAPI.Output()[i][j].Count; k++)
                        {
                            if (KernelAPI.Output()[i][j][k] == true)
                                Console.Write("1");

                            else
                                Console.Write("0");
                        }

                        Console.WriteLine();
                    }
                }
            }

            else if (command == "Save")
                KernelAPI.Save();

            else
                Console.WriteLine("Command is Path, Input, Output, Save");

            Stand();
        }
    }
}
