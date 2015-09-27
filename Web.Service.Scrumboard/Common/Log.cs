using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ScrumboardWebService.Common
{
    public abstract class Log
    {
        private static String getLogFileName()
        {
            return String.Format("{0}\\scrumboard.log", AppDomain.CurrentDomain.BaseDirectory);
        }

        public static void logMessage(String message)
        {
            try
            {
                flushLog();
                StreamWriter writer = File.AppendText(getLogFileName());
                writer.WriteLine("{0}\t{1}", DateTime.Now, message);
                writer.Flush();
                writer.Close();
            }
            catch
            {
                // just proceed
            }
        }
        public static void logException(Exception ex)
        {
            try
            {
                flushLog();
                StreamWriter writer = File.AppendText(getLogFileName());
                writer.WriteLine("{0}\t{1}", DateTime.Now, ex.Message);
                writer.WriteLine(ex.StackTrace);
                writer.WriteLine();
                writer.Flush();
                writer.Close();
            }
            catch
            {
                // just proceed
            }
        }
        private static void flushLog()
        {
            try
            {
                if (File.Exists(getLogFileName()))
                {
                    int compare = File.GetCreationTime(getLogFileName()).AddDays(1).CompareTo(DateTime.Now);
                    if (compare < 0)
                    {
                        File.Delete(getLogFileName());
                    }

                }
            }
            catch
            {
                // just proceed
            }
        }
    }
}