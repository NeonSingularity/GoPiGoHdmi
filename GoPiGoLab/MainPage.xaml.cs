using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using GoPiGoLab.Controllers;
using GoPiGo;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GoPiGoLab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly IBuildGoPiGoDevices DeviceFactory = global::GoPiGo.DeviceFactory.Build;
        public IGoPiGo GoPiGo;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();
            GoPiGo = DeviceFactory.BuildGoPiGo();
            GoPiGo.MotorController().EnableServo();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            SendCommand(GoPiGoCommand.RotateLeft);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            SendCommand(GoPiGoCommand.Stop);
        }

        private void SendCommand(GoPiGoCommand cmd, int value = 0)
        {
            CommandParser.GoPiGo = GoPiGo;
            CommandParser.ParseCommand(cmd, value);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SendCommand(GoPiGoCommand.RotateRight);
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            SendCommand(GoPiGoCommand.Right);
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            SendCommand(GoPiGoCommand.Left);
        }

        private void Timer_Tick()
        {
            timer = new DispatcherTimer();
            // timer.Start();
            timer.Interval = TimeSpan.FromSeconds(3);
            // timer.Stop();
            Randoms();
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Randoms();
        }

        private void Randoms()
        {
            Random random = new Random();
            int num = random.Next(0, 5);
            if (num == 0)
            {
                SendCommand(GoPiGoCommand.Right);                               //Go left
                Timer_Tick();                
            }
            else if (num == 1)
            {
                SendCommand(GoPiGoCommand.RotateLeft);      //Go forward
                Timer_Tick();
            }
            else if (num == 2)
            {
                SendCommand(GoPiGoCommand.Stop);        //Stop
                Timer_Tick();
            }
            else if (num == 3)
            {
                SendCommand(GoPiGoCommand.RotateRight);     //Go backwards
                Timer_Tick();
            }
            else
            {
                SendCommand(GoPiGoCommand.Left);        //Go right
                Timer_Tick();
            }
        }
    }
}
