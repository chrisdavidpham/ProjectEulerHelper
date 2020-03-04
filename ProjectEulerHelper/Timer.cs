using System;
using System.Diagnostics;

namespace ProjectEuler
{
    public static class Timer
    {
        /// <summary>
        /// Invokes the method <paramref name="func"/> and measures the time to execute the method.
        /// </summary>
        /// <param name="func">A method</param>
        /// <returns>The number of milliseconds a method required to return.</returns>
        static public int Time<T>(Func<T> func, out T funcOut)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            funcOut = func();
            stopwatch.Stop();
            return Convert.ToInt32(stopwatch.ElapsedMilliseconds);
        }
    }
}
