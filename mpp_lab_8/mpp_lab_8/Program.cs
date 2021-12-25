using System;
using System.Linq;
using System.Reflection;

namespace SPP8
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\mpp_lab_8.dll";
            Assembly assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes().Where(t => t.IsPublic && t.IsDefined(typeof(ExportClass), false));//фолс не будет дочерних
            Console.WriteLine("ExportClass:");
            foreach (var type in types)
            {
                Console.WriteLine(type.FullName);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]//ограниченя для класса
    public class ExportClass : Attribute { }//наследуем чтобы экспорт юзать как атрибут

    [ExportClass]
    public class PublicClass { }
    [ExportClass]
    internal class InternalClass { }
}