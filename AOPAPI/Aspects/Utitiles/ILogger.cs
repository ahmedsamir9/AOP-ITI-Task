using System;

namespace AOPAPI.Aspects.Utitiles
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogError(Exception exception);
    }
}