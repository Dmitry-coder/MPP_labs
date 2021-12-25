using System;
using System.Collections.Concurrent;
using System.IO;
using System.Timers;

namespace SixthTask
{
    class LogBuffer
    {
        private static ConcurrentQueue<string> Messages = new ConcurrentQueue<string>();
        private readonly StreamWriter _streamWriter;

        private const int Capacity = 30;
        private const int Limit = 1;
        private static readonly Timer timer = new Timer(Limit);

        public LogBuffer(string filePath = "C:\\MyLangC\\mpp_lab_6\\mpp_lab_6\\logbuffer.txt")
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("File doesn't exists: " + filePath);
            }

            _streamWriter = new StreamWriter(filePath, true);
            timer.Elapsed += CheckTime;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void Add(string item)
        {
            Messages.Enqueue(item);
            CheckCapacity();
        }

        private void CheckCapacity()
        {
            if (Messages.Count < Capacity)
            {
                return;
            }

            while (!Messages.IsEmpty)
            {
                Messages.TryDequeue(out string message);
                if (message != null)
                {
                    _streamWriter.WriteLineAsync(message);
                }
            }
        }

        private void CheckTime(object source, ElapsedEventArgs e)
        {
            Console.WriteLine(true);
            while (!Messages.IsEmpty)
            {
                Messages.TryDequeue(out var message);
                if (message != null)
                {
                    _streamWriter.WriteLineAsync(message);
                }
            }
        }

        public void Close()
        {
            _streamWriter.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            LogBuffer logBuffer = new LogBuffer();
            for (var i = 0; i < 100; i++)
            {
                logBuffer.Add(i.ToString());
            }
            logBuffer.Close();
        }
    }
}
