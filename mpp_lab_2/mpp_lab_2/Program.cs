using System;

namespace mpp_lab_2
{
    public delegate void TaskDelegate(string file1, string file2);

    public delegate void LoggerDelegate(string logMessage);
    static class Program
    {
        public static void Logger(string logMessage)
        {

        }

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("введите только 2 аргумента");
                return;
            }

            string dir1 = args[0];
            string dir2 = args[1];

            ThreadQueue threadQueue = new ThreadQueue(3);
            DirectoryWork directoryWork = new DirectoryWork(dir1, dir2, threadQueue, null);
            directoryWork.CopyDir(dir1, dir2);
            Console.WriteLine("файлов скопировано: " + DirectoryWork.filesCopyNum);
            Console.WriteLine("подкаталогов создано: " + DirectoryWork.dirsCreateNum);
            Console.ReadLine();
        }
    }
}
/*
 * Поток — это по сути последовательность инструкций, которые выполняются параллельно с другими потоками. 
 * Каждая программа создает по меньшей мере один поток: основной, который запускает функцию main(). 
 * Программа, использующая только главный поток, является однопоточной; если добавить один или более потоков, она станет многопоточной.
 */