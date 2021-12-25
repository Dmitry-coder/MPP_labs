using System;
using System.Threading;
using System.Collections.Concurrent;

namespace mpp_lab_2
{
    public class ThreadQueue
    {
        //Предоставляет возможности блокировки и ограничения для поточно- ориентированных коллекций с уникальным типом, реализующих IProducerConsumerCollection<T>
        public BlockingCollection<object[]> FuncQueue = new BlockingCollection<object[]>(new ConcurrentQueue<object[]>());
        //BlockingCollection<T> поддерживает ограничение и блокировку.Ограничение означает, что вы можете установить максимальную емкость коллекции.
        //Ограничение важно в определенных сценариях, потому что оно позволяет вам контролировать максимальный размер коллекции в памяти и предотвращает 
        //перемещение производящих потоков слишком далеко впереди потребляющих потоков.

        private int queueCount;

        public ThreadQueue(int queueCount)
        {
            this.queueCount = queueCount;
            for (int i = 0; i < queueCount; i++)
            {
                var thread = new Thread(ThreadWork);//создание нового потока
                thread.Start();
            }
        }

        public void ThreadWork()
        {
            while (true)
            {
                object[] task = FuncQueue.Take();//Удаляет элемент из коллекции BlockingCollection <T>
                try
                {

                    ((DirectoryWork)task[0]).CopyFile((string)task[1], (string)task[2]);
                }
                catch (ThreadStateException ex) //Исключение, которое выдается, когда Thread находится в недопустимом ThreadState для вызова метода.
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ThreadAbortException ex) //Исключение, которое выдается при вызове метода Abort (Object) . 
                //Этот класс не может быть унаследован. Когда вызывается метод Abort для уничтожения потока, среда CLR генерирует исключение ThreadAbortException
                {
                    Thread.ResetAbort();
                }
                catch (Exception ex)//Базовым для всех типов исключений является тип Exception. Этот тип определяет ряд свойств, с помощью которых можно получить информацию об исключении
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void EnqueueTask(object directoryWork,
            string file1, string file2)
        {
            this.FuncQueue.Add(new object[] { directoryWork, file1, file2 });//Добавляет элемент в коллекцию BlockingCollection <T>
        }
    }
}