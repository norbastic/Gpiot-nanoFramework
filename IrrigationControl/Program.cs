using IrrigationControl.Services;
using System;
using System.Diagnostics;
using System.Threading;

namespace IrrigationControl
{
    public class Program
    {
        public static void Main()
        {
            var ver = StateManager.GetInstance().GetState("ApplicationVersion");

            Debug.WriteLine($"Hello from nanoFramework, ver: {ver}");

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
