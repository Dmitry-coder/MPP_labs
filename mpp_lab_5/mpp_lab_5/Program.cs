using System;
using System.IO;
using System.Linq;
using System.Reflection;


namespace mpp_lab_5
{
    class Program
    {
        static void main(String dir)
        {
            var assemblyLoader = new AssemblyLoader(dir);
            assemblyLoader.ShowPublicTypes();
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                main("C:\\mpp_lab_3.dll");
            }
            else
            {
                if (File.Exists(args[0]))
                {
                    main(args[0]);
                }
                else
                {
                    Console.WriteLine("Fail");
                }
            }
        }
    }

    class AssemblyLoader
    {
        public Assembly Assembly { get; set; }

        public AssemblyLoader(string assemblyPath)
        {
            try
            {
                Assembly = Assembly.LoadFrom(assemblyPath);//загрузка сборки в проект
                Console.WriteLine("Assembly loaded successfully.");
            }
            catch (Exception e)
            {
                throw new Exception("Load error: " + e.Message);
            }
        }

        public void ShowPublicTypes()
        {
            var types = Assembly
                .GetTypes()
                .Where(t => t.IsPublic)
                .OrderBy(t => t.Namespace)
                .ThenBy(t => t.Name);
            foreach (var type in types)
            {
                Console.WriteLine(type.FullName);
            }
        }
    }
}