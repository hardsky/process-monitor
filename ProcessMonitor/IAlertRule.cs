using Infrastructure;
using System;

namespace ProcessMonitor
{
    public interface IAlertRule
    {
        string Check(ProcessData p);
    }
}
