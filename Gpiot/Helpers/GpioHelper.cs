using Gpiot.Models;
using System;
using System.Device.Gpio;
using System.Diagnostics;

namespace Gpiot.Helpers
{
    internal static class GpioHelper
    {
        public static bool ActivatePinOutput(int pinNumber)
        {
            var _controller = new GpioController();
            try
            {
                if (!_controller.IsPinOpen(pinNumber))
                {
                    _controller.OpenPin(pinNumber);
                }

                _controller.SetPinMode(pinNumber, PinMode.Output);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool DeactivatePinOutput(int pinNumber)
        {
            var _controller = new GpioController();
            try
            {
                if (_controller.IsPinOpen(pinNumber))
                {
                    _controller.ClosePin(pinNumber);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool WriteOutputToPin(PinInfo pinInfo)
        {
            var _controller = new GpioController();
            try
            {
                var pinValue = pinInfo.Value == 0 ? PinValue.Low : PinValue.High;
                _controller.Write(pinInfo.PinNumber, pinValue);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool IsPinActive(int pinNumber)
        {
            var _controller = new GpioController();
            return _controller.IsPinOpen(pinNumber);
        }

        public static PinValue GetPinValue(int pinNumber)
        {
            var _controller = new GpioController();
            if (!_controller.IsPinOpen(pinNumber))
            {
                return PinValue.Low;
            }

            return _controller.Read(pinNumber);
        }
    }
}
