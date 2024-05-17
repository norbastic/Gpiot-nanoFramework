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
            var stateManager = new StateManager();
            stateManager.Init();

            stateManager.SetState("ApplicationVersion", "0.0.1");

            var ver = stateManager.GetState("ApplicationVersion");


            Debug.WriteLine($"Hello from nanoFramework, ver: {ver}");

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
