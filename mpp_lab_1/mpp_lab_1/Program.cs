using System;
using System.Threading;

namespace mpp_lab_1
{
	class Program
	{
		static void procID()
		{
			Console.WriteLine("текущий id = " + Thread.CurrentThread.ManagedThreadId);
			Thread.Sleep(100);
		}
		static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");
			TaskQueue taskQueue = new TaskQueue(3);
			Console.WriteLine("текущий id = " + Thread.CurrentThread.ManagedThreadId);
			for (int i = 0; i < 3; i++)
			{
				taskQueue.EnqueueTask(procID);
			}
			Console.ReadLine();
		}
	}
}