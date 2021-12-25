using System.Threading;

namespace mpp_lab_3
{
    public class Mutex
    {
        private int curId = -1;

        public void Lock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;//Получает уникальный идентификатор текущего управляемого потока.
            while (Interlocked.CompareExchange(ref this.curId, id, -1) != -1)//Сравнивает два значения на равенство и, если они равны, заменяет первое значение.
                                                                             //1.Пункт назначения, значение которого сравнивается comparandи, возможно, заменяется.
                                                                             //2.Значение, заменяющее целевое значение, если сравнение приводит к равенству.
                                                                             //3.Значение, которое сравнивается со значением в location1.
            {
                Thread.Sleep(10);
            }
        }

        public void Unlock()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            Interlocked.CompareExchange(ref this.curId, -1, id);
        }
    }
}