using System;
using System.Reflection;
using System.IO;
namespace lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User("ilya", 18);
            Reflection.GetTYPE(user1);

            Reflection.PublicMethod(user1);

            Reflection.GetFieldsAndProperties(user1);

            Reflection.GetInterface(user1);

            Reflection.GetUserDefined(user1, "private");

            Reflection.Call(user1, "M1");
            Console.ReadKey();
        }
    }




    static class Reflection
    {
        static public void GetTYPE(Object obj)     // информаци о классе и вывод в файл
        {

            Type myType = obj.GetType();



            using (TextWriter sw = new StreamWriter("lab12.txt", false))
            {
                foreach (MemberInfo i in myType.GetMembers())
                {
                    sw.Write(i.DeclaringType + "  zzz");
                    sw.Write(i.MemberType + "  ");
                    sw.WriteLine(i.Name);
                }
            }

        }

        static public void PublicMethod(Object obj)
        {
            Type myType = obj.GetType();
            MethodInfo[] methods = myType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Console.WriteLine("Публичные методы данного класса : ");
            foreach (MethodInfo i in methods)
            {
                Console.WriteLine(i.Name);
            }

        }

        static public void GetFieldsAndProperties(Object obj)
        {
            Type myType = obj.GetType();
            Console.WriteLine("Информация о полях объекта : ");
            foreach (FieldInfo i in myType.GetFields())
            {
                Console.WriteLine($"{i.FieldType}  {i.Name}");
            }
            Console.WriteLine("Информация о свойствах объекта : ");
            foreach (PropertyInfo i in myType.GetProperties())
            {
                Console.WriteLine($"{i.PropertyType}  {i.Name}");
            }
        }

        static public void GetInterface(Object obj)
        {
            Type myType = obj.GetType();
            Console.WriteLine("Информация о реализованных интерфейсов классом : ");
            foreach (MemberInfo i in myType.GetInterfaces())
            {
                Console.WriteLine(i.Name);
            }
        }

        static public void GetUserDefined(Object obj, string nameMethod)
        {
            Type myType = obj.GetType();
            if (nameMethod.ToUpper() == "INTERFACE")
            {
                Console.WriteLine("Информация о реализованных интерфейсов классом : ");
                foreach (MemberInfo i in myType.GetInterfaces())
                {
                    Console.WriteLine(i.Name);
                }
            }
            if (nameMethod.ToUpper() == "PUBLIC")
            {
                MethodInfo[] methods = myType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                Console.WriteLine("Публичные методы данного класса : ");
                foreach (MethodInfo i in methods)
                {
                    Console.WriteLine(i.Name);
                }
            }
            if (nameMethod.ToUpper() == "PRIVATE")
            {
                MethodInfo[] methods = myType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                Console.WriteLine("Приватные методы данного класса : ");
                foreach (MethodInfo i in methods)
                {
                    Console.WriteLine(i.Name);
                }
            }

        }
        public static void Call(Object obj, string NameMethod)
        {
            if (NameMethod == "M1")
            {
                using (TextReader sw = new StreamReader("lab1_2.txt", false))
                {
                    string str = sw.ReadLine();
                    if (int.TryParse(str, out int result))
                    {

                        int a = Convert.ToInt32(str);
                        User.M1(a);
                    }
                    else
                        Console.WriteLine("Невозможно выполнить приведение типов !  ");
                }
            }

            if (NameMethod == "M2")
            {
                using (TextReader sw = new StreamReader("lab1_2.txt", false))
                {
                    string str = sw.ReadLine();
                    User.M2(str);
                }
            }
        }
    }


    class User : Print
    {
        public int a;
        public string Name { get; set; }
        public int Age { get; set; }
        public User(string n, int a)
        {
            Name = n;
            Age = a;
        }
        public static void M1(int a = 10)
        {
            Console.WriteLine($"M1 ! {a}");
        }
        public static void M2(string str)
        {
            Console.WriteLine($"M2 !{ str}");
        }
        private void M3()
        {
            Console.WriteLine("M3 !");
        }

        public void print()
        {
            Console.WriteLine("Это метод print !");
        }
        public void SUM(int a, int b)
        {
            Console.WriteLine($"Cумма 2-х элементов : a + b = {a + b}");
        }
    }

    interface Print
    {
        void print();
    }
}
