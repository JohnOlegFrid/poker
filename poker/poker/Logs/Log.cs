using System;
using System.IO;


namespace poker.Logs
{
    class Log
    {

        private static readonly string ERROR_PATH = "errorLog.txt";
        private static readonly string INFO_PATH = "infoLog.txt";

        public static void ErrorLog(string msg)
        {
            LogFile(ERROR_PATH, msg);
        }

        public static void InfoLog(string msg)
        {
            LogFile(INFO_PATH, msg);
        }

        private static void LogFile(string path, string msg)
        {
            StreamWriter log;
            if (!File.Exists(path))

            {
                log = new StreamWriter(path);
            }
            else
            {
                log = File.AppendText(path);
            }

            log.WriteLine("Data Time:" + DateTime.Now);
            log.WriteLine("MSG:" + msg);

            log.Close();
        }
    }
}
