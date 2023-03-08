using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.ILogger;

namespace Logger
{
    internal class LogFile : ILog
    {
        public static string FilePath = "C:\\Users\\User\\source\\repos\\News";
        public static string FullFileName { get { return FilePath + DateTime.Now.ToString("dd-MM-yyyy") + "file" + Index + ".log"; } set { Index = int.Parse(value); } }

        public static int Index = 1;

        public int MaxFileSize = 5000000;
        public void Init()
        {

        }

        public void Log(LogItem item)
        {
            if (item == null) return;

            using (StreamWriter streamWriter = new StreamWriter(FullFileName, true))
            {
                string log;

                if (item.ExceptionSource == null)
                {
                    log = item.Type + " - " + item.DateTime + " - " + item.Message;
                }
                else
                {
                    log = item.Type + " - " + item.DateTime + " - " + item.ExceptionSource.StackTrace.ToString() + ", " + item.Message;
                }

                streamWriter.WriteLine(log);
                streamWriter.Close();

            }

        }
        public void LogCheckHoseKeeping()
        {
            CheckFileSize();
        }

        private void CreateNewFile()
        {

            while (true)
            {
                string newFileName = FilePath + DateTime.Now.ToString("dd-MM-yyyy") + "file" + Index + ".log";

                if (!File.Exists(FullFileName))
                {

                    File.Create(FullFileName).Close();
                    return;
                }
                else
                {
                    FileInfo newFileInfo = new FileInfo(FullFileName);

                    if (newFileInfo.Length >= MaxFileSize)
                    {
                        Index++;
                        continue;
                    }

                    if (newFileInfo.Length < MaxFileSize)
                    {
                        return;
                    }

                    FullFileName = newFileName;

                    File.AppendAllText(FullFileName, "FileOpen");

                }
            }
        }

        private void CheckFileSize()
        {
            if (!File.Exists(FullFileName))
            {
                CreateNewFile();
                return;
            }

            FileInfo fileInfo = new FileInfo(FullFileName);
            if (fileInfo.Length >= MaxFileSize)
            {
                CreateNewFile();
            }
        }
    
    }
}
