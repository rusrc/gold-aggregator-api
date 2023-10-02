using System;

using static System.Diagnostics.Debug;

namespace GoldAggregator.Parser.Logger
{
    public class DebugLogService : ILogService
    {
        public void Debug(string msg)
        {
            WriteLine($"Debug: {msg}");
        }

        public void Debug(string msg, Exception ex)
        {
            do
            {
                WriteLine($"Debug: {msg}, stackTrace: {ex.StackTrace}");
                ex = ex.InnerException;
            }
            while (ex != null);
        }

        public void Error(string msg)
        {
            WriteLine($"Error: {msg}");
        }

        public void Error(string msg, Exception ex)
        {
            do
            {
                WriteLine($"Error: {msg}, stackTrace:  {ex.StackTrace}");

                ex = ex.InnerException;
            }
            while (ex != null);
        }

        public void Info(string msg)
        {
            WriteLine($"Info: {msg}");
        }

        public void Info(string msg, Exception ex)
        {
            do
            {
                WriteLine($"Info: {msg}, stackTrace:  {ex.StackTrace}");
                ex = ex.InnerException;
            }
            while (ex != null);
        }

        public void Warn(string msg)
        {
            WriteLine($"Warn: {msg}");
        }

        public void Warn(string msg, Exception ex)
        {
            do
            {
                WriteLine($"Warn: {msg}, stackTrace:  {ex.StackTrace}");
                ex = ex.InnerException;
            }
            while (ex != null);
        }
    }
}
