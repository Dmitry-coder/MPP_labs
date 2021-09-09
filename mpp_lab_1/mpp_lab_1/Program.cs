using System;
using System.Threading;

namespace mpp_lab_1
{
	class Program
	{
		static int x = 0;
		static int y = 1;
		static object locker = new object();
		static void count()
		{
			lock (locker)
			{
				x = 1;
				for (int i = 1; i < 6; i++)
				{
					Console.WriteLine("Поток #{0}: {1}", y, x);
					x++;
					Thread.Sleep(100);
				}
				y++;
				
			}
		}
		static void Main(string[] args)
		{
			TaskQueue taskQueue = new TaskQueue(3);
			for (int i = 0; i < 3; i++)
			{
				taskQueue.EnqueueTask(count);
			}
			Console.ReadLine();
		}
	}
}
