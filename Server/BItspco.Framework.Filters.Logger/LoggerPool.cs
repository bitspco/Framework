using System;
using System.Collections.Generic;
using System.Threading;

namespace Bitspco.Framework.Filters.Log
{
    public class LoggerPool
    {
        private readonly Queue<LogPackage> _logs = new Queue<LogPackage>();
        private readonly LoggerPool _parent;
        private Thread thread;
        private object monitor = new object();
        public List<ILogger> Loggers { get; } = new List<ILogger>();
        public LoggerPool(ILogger logger, LoggerPool parent = null)
        {
            Loggers.Add(logger);
            _parent = parent;
            try
            {
                thread = new Thread(Start);
                thread.Start();
            }
            catch (Exception) { }
        }
        ~LoggerPool()
        {
            try
            {
                thread.Abort();
            }
            catch (Exception) { }
        }
        private void Start()
        {
            while (true)
            {
                lock (monitor) Monitor.Wait(monitor);
                lock (_logs)
                {
                    foreach (var log in _logs)
                    {
                        foreach (var logger in Loggers)
                        {
                            try { logger.Log(log); }
                            catch (Exception)
                            {
                                try { foreach (var plogger in _parent.Loggers) plogger.Log(log); }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
        }
        public void Enqueue(LogPackage log)
        {
            lock (_logs) _logs.Enqueue(log);
            lock (monitor) Monitor.PulseAll(monitor);
        }
    }
}
