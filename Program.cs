using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _11_LINQ
{

    public class MyVector : IEquatable<MyVector>
    {
        public int[] Arr { get; }
        public int Capacity { get; }
        public string name { get; }
        public int Length { get; }
        public int Max { get; private set; }
        public int Min { get; private set; }


        //констрик еще один
        public MyVector(string _name, int n, int[] Arr)
        {
            Length = n;
            Capacity = Length + 1;
            Arr = new int[Capacity];
        }

        // Конструктор с передачей вместимости вектора
        public MyVector(int length)
        {
            Length = length;
            Arr = new int[Length];
        }

        // Конструктор с передачей вместимости вектора и значения по умолчанию
        public MyVector(int length, int defaultValue)
              : this(length)
        {
            for (int index = 0; index < Length; index++)
                Arr[index] = defaultValue;
            Max = defaultValue;
            Min = defaultValue;
        }
        // Конструктор с передачей значений массива
        public MyVector(IEnumerable<int> values)
              : this(values.Count())
        {
            int index = 0;
            foreach (int item in values)
                Arr[index++] = item;
            Max = Arr.Max();
            Min = Arr.Min();
        }

        public int this[int index]
        {
            get => Arr[index];
            set
            {
                Arr[index] = value;
                Max = Arr.Max();
                Min = Arr.Min();
            }
        }

        public bool Equals(MyVector other)
        {
            if (Length != other.Length || Max != other.Max || Min != other.Min)
                return false;

            for (int index = 0; index < Length; index++)
                if (Arr[index] != other.Arr[index])
                    return false;
            return true;
        }

        public override bool Equals(object obj)
              => (obj is MyVector vector) && Equals(vector);

        public override int GetHashCode()
        {
            int hash = Length.GetHashCode();
            for (int index = 0; index < Length; index++)
                hash ^= Arr[index].GetHashCode();
            return hash;
        }

        private static readonly char[] separators = { ',', ' ', '/', '\\' };
        public static MyVector Parse(string source, params char[] separators)
             => new MyVector(source.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
        public static MyVector Parse(string source)
             => Parse(source, separators);


        public override string ToString()
        {
            return $"Length : {Length }\r\n{string.Join(", ", Arr)}";
        }
    }

    class Program
    {
        static void Month(int _len) // по длине
        {
            string[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var result = month.Where(x => x.Length == _len).ToList();
            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
            Console.Read();
        }
        static void СoldHot(string _month, string _month1, string _month2) // ЗИМА&ЛЕТО
        {
            string[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var result = month.Where(x => x == _month).ToList();
            var result1 = month.Where(x => x == _month1).ToList();
            var result2 = month.Where(x => x == _month2).ToList();

            result.AddRange(result1);
            result.AddRange(result2);
            result = result.OrderBy(x => x).ToList();

            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
            Console.Read();

        }

        static void SeletAlphabet()                 // сортировка по алфавиту
        {
            string[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var selectedMonth = from t in month     // определяем каждый объект из month как t                              
                                orderby t           // упорядочиваем по возрастанию
                                select t;           // выбираем объект

            foreach (string s in selectedMonth)
                Console.WriteLine(s);

        }
        static void NameLen(int _len)               // месяцы, содержащие букву "u" с длинной >= 4
        {
            string[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            IEnumerable<string> AnotherMonths = month
                 .Where(s => (s.Contains('u')) && (s.Length >= 4));
            foreach (string x in AnotherMonths)
            {
                Console.WriteLine(x);
            }
            Console.WriteLine();
        }


        static void ClassLength(int _len) //Массив длины
        {
            Console.WriteLine("Введите целые числа через пробел");
            MyVector v1 = MyVector.Parse(Console.ReadLine());

            Console.WriteLine("Введите целые числа через двоеточие");
            MyVector v2 = MyVector.Parse(Console.ReadLine(), ':');

            Console.WriteLine("Введите целые числа через точку");
            MyVector v3 = MyVector.Parse(Console.ReadLine(), '.');

            List<MyVector> vc = new List<MyVector>() { };
            vc.Add(v1);
            vc.Add(v2);
            vc.Add(v3);


            var result = vc.Where(x => x.Length == _len).ToList();

            Console.WriteLine(string.Join("\r\n", result));
        }

        static void ClassZero() //Массив нуля(пустой вектор)
        {
            Console.WriteLine("Введите целые числа через пробел");
            MyVector v1 = MyVector.Parse(Console.ReadLine());

            Console.WriteLine("Введите целые числа через двоеточие");
            MyVector v2 = MyVector.Parse(Console.ReadLine(), ':');

            Console.WriteLine("Введите целые числа через точку");
            MyVector v3 = MyVector.Parse(Console.ReadLine(), '.');

            List<MyVector> vc = new List<MyVector>() { };
            vc.Add(v1);
            vc.Add(v2);
            vc.Add(v3);


            var result = vc.Where(x => x.Length == 0).ToList();

            Console.WriteLine(string.Join("\r\n", result));
        }


        static void ClassMin() //Массив миник
        {
            Console.WriteLine("Введите целые числа через пробел vector1");
            MyVector v1 = MyVector.Parse(Console.ReadLine());
            Console.WriteLine("Введите целые числа через пробел vector2");
            MyVector v2 = MyVector.Parse(Console.ReadLine());

            List<MyVector> vc = new List<MyVector>() { };

            vc.Add(v1);
            vc.Add(v2);
            Console.WriteLine("Min mass:");
            //v1 = vc.First(v => v.Arr[0] < 0);
            var result = vc.First(v => v.Arr.Any(n => n < 0));

            Console.WriteLine(string.Join("\r\n", result));

        }
        static void ClassMax() // 2
        {
            Console.WriteLine("Введите целые числа через пробел vector1");
            MyVector V1 = new MyVector(8, 10);
            List<MyVector> vc = new List<MyVector>() { };
            vc.Add(V1);
            Console.WriteLine("Max mass:");
            var result = vc.Max();
            Console.WriteLine("The largest number is {0}.", V1);
         }

        static void ClassCount() //Массив 
        {
            Console.WriteLine("Введите целые числа через пробел vector1");
            MyVector v1 = MyVector.Parse(Console.ReadLine());
            Console.WriteLine("Введите целые числа через пробел vector2");
            MyVector v2 = MyVector.Parse(Console.ReadLine());

            List<MyVector> vc = new List<MyVector>() { };

            vc.Add(v1);
            vc.Add(v2);

            int number = vc.Count();
            Console.WriteLine(
            "There are {0} vector in the collection.",
            number);



        }

        static void ClassIndex() //Массив миник
        {
            Console.WriteLine("Введите целые числа через пробел vector1");
            MyVector v1 = MyVector.Parse(Console.ReadLine());
            Console.WriteLine("Введите целые числа через пробел vector2");
            MyVector v2 = MyVector.Parse(Console.ReadLine());
            Console.WriteLine("Введите целые числа через пробел vector3");
            MyVector v3 = MyVector.Parse(Console.ReadLine());

            List<MyVector> vc = new List<MyVector>() { };

            vc.Add(v1);
            vc.Add(v2);
            vc.Add(v3);
            var result = vc.ElementAt(1);

            Console.WriteLine(string.Join("\r\n", result));


        }


        static void Classlen() //Массив Длина сорт
        {
            Console.WriteLine("Введите целые числа через пробел vector1");
            MyVector v1 = MyVector.Parse(Console.ReadLine());
            Console.WriteLine("Введите целые числа через пробел vector2");
            MyVector v2 = MyVector.Parse(Console.ReadLine());
            Console.WriteLine("Введите целые числа через пробел vector3");
            MyVector v3 = MyVector.Parse(Console.ReadLine());

            List<MyVector> vc = new List<MyVector>() { };

            vc.Add(v1);
            vc.Add(v2);
            vc.Add(v3);
            var result = vc.OrderBy(x => x.Length);

            Console.WriteLine(string.Join("\r\n", result));


        }


        static void Main(string[] args)
        {
            //Console.WriteLine("Выберите функцию: 1, 2, 3, 4");
            //var choose = Convert.ToInt32(Console.ReadLine());
            //switch (choose)
            //{
            //    case 1:
            //        {
            //            Console.WriteLine("Введите длину названия месяца:");
            //            var length_of_month = Convert.ToInt32(Console.ReadLine());
            //            Month(length_of_month);
            //            break;
            //        }
            //    case 2:
            //        {
            //            Console.WriteLine("Онли зима/лето:");
            //            СoldHot("December", "June", "February");
            //            break;
            //        }
            //    case 3:
            //        {
            //            Console.WriteLine("Месяцы в алфавитном порядке: ");
            //            SeletAlphabet();
            //            break;
            //        }
            //    case 4:
            //        {
            //            Console.WriteLine("");
            //            Console.WriteLine("Месяцы, содержащие букву 'u' с длинной >= 4: ");
            //            NameLen(4);
            //            break;
            //        }
            //    default: Console.WriteLine("Ничего не выбранно!"); break;
            //}



            // №2////////////////////////////////////////////////////////////////////////////////////
            //ClassLength(6);
            //ClassZero();
            //ClassMin();
            //ClassMax();
            //ClassCount();
            //ClassIndex();
            Classlen();
        }
    }
}