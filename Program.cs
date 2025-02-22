using System;
using System.Threading;
using System.Diagnostics;

using System.Device.Gpio;
using Iot.Device.Button;

namespace ESP32S3_Button_Nuget
{
    public class Program
    {
        private static GpioPin led;
        private static Boolean sw;

        public static void Main()
        {
            GpioController controller = new GpioController();

            led = controller.OpenPin(2, PinMode.Output);

            sw = false;

            //Button�̐ݒ�
            GpioButton button = new GpioButton(35, controller, true, PinMode.InputPullDown);
            //�_�u���N���b�N��L���ɂ���
            button.IsDoublePressEnabled = true;
            //��������L���ɂ���
            button.IsHoldingEnabled = true;

            //event
            button.ButtonDown += Button_ButtonDown;         //�{�^�������������ɃC�x���g
            button.ButtonUp += Button_ButtonUp;             //�{�^���𗣂������ɃC�x���g
            button.Press += Button_Press;                   //�{�^���������Ă��痣�������ɃC�x���g
            button.DoublePress += Button_DoublePress;      //�{�^�����_�u���N���b�N�����ɃC�x���g
            button.Holding += Button_Holding;              //�{�^���𒷉����������ɃC�x���g
            Debug.WriteLine("Start!");

            Thread.Sleep(Timeout.Infinite);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Button_Holding(object sender, ButtonHoldingEventArgs e)
        {
            switch (e.HoldingState)
            {
                case ButtonHoldingState.Started:
                    Debug.WriteLine("Holding Start.");
                        break;
                case ButtonHoldingState.Completed:
                    Debug.WriteLine("Holding Completed.");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Button_DoublePress(object sender, EventArgs e)
        {
            Debug.WriteLine("Button DoublePress");
            if (sw == false)
            {
                led.Write(PinValue.High);
                sw = true;
            }
            else
            {
                led.Write(PinValue.Low);
                sw = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Button_Press(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Press");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Button_ButtonUp(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Up");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Button_ButtonDown(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Down");
        }
    }
}
