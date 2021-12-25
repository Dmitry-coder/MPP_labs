using System;
using System.Runtime.InteropServices;

//Создать класс на языке C#, который: 
//- называется OSHandle и обеспечивает автоматическое или принудительное освобождение заданного дескриптора операционной системы;
//- содержит свойство Handle, позволяющее установить и получить дескриптор операционной системы; 
//- реализует метод Finalize для автоматического освобождения дескриптора;
//- реализует интерфейс IDisposable для принудительного освобождения дескриптора; 

namespace lab
{
    class OsHandle : IDisposable
    {
        [DllImport("Kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        private bool _disposed = false;
        public IntPtr Handle { get; set; }

        public OsHandle()
        {
            Handle = IntPtr.Zero;
        }

        ~OsHandle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//подавляем чтобы не вызвалась дважды
        }

        public void Close()
        {
            Dispose();
        }

        protected virtual void Dispose(bool _disposed)
        {
                if (!_disposed && Handle != IntPtr.Zero)
                {
                    CloseHandle(Handle);
                    Handle = IntPtr.Zero;
                }
                _disposed = true;
        }
    }
    //dispose для неуправляемых ресурсов как дескриптор, finalize ддля управляемых
    //финализ вызывается при создании обьекта

    static class Program
    {
        static void Main(string[] args)
        {
            OsHandle os = new OsHandle();
            os.Dispose();
        }
    }
}