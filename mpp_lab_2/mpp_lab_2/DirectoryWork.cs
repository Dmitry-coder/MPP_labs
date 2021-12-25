using System;
using System.Threading;
using System.IO;

namespace mpp_lab_2
{
    public class DirectoryWork
    {
        private string dirName1;
        private string dirName2;
        private LoggerDelegate logger;
        public static int filesCopyNum, dirsCreateNum;
        private ThreadQueue poolQueue;

        public DirectoryWork(string dirName1, string dirName2, ThreadQueue poolQueue, LoggerDelegate logger)
        {
            this.poolQueue = poolQueue;
            this.dirName1 = dirName1;
            this.dirName2 = dirName2;
            this.logger = logger;
        }

        public void CopyDir(string dirName1, string dirName2)
        {
            filesCopyNum = 0;
            dirsCreateNum = 0;
            DirectoryRecursion(dirName1, dirName2);
            while (this.poolQueue.FuncQueue.Count != 0) { Thread.Sleep(50); };
        }

        private void DirectoryRecursion(string dirName1, string dirName2)
        {
            string[] arr;
            if (!Directory.Exists(dirName1))
            {
                return;
            }
            if (!Directory.Exists(dirName2))
            {
                System.IO.Directory.CreateDirectory(dirName2);
            }
            string[] dirs = Directory.GetDirectories(dirName1);
            string[] files = Directory.GetFiles(dirName1);
            foreach (string dir in dirs)
            {
                arr = dir.Split('\\');
                if (!System.IO.Directory.Exists(dirName2 + "\\" + arr[arr.Length - 1]))
                {
                    System.IO.Directory.CreateDirectory(dirName2 + "\\" + arr[arr.Length - 1]);
                    dirsCreateNum++;
                }
                DirectoryRecursion(dir, dirName2 + "\\" + arr[arr.Length - 1]);
            }
            CopyFile(dirName1, dirName2, files);

        }


        private void CopyFile(string dirName1, string dirName2, string[] files)
        {
            string[] arr;
            foreach (string file in files)
            {
                arr = file.Split('\\');
                poolQueue.EnqueueTask(this, file, dirName2 + "\\" + arr[arr.Length - 1]);
            }
        }

        public void CopyFile(string sourceFieName, string destFileName)
        {
            System.IO.File.Copy(sourceFieName, destFileName);
            DirectoryWork.filesCopyNum++;
        }
    }
}