using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PongWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModelBase();

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ViewModelBase vmb = (ViewModelBase)this.DataContext;
            vmb.Timer_Tick();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModelBase vmb = (ViewModelBase)this.DataContext;
            if (Keyboard.IsKeyDown(Key.W))
                vmb.MoveLeftPadUp();
            if (Keyboard.IsKeyDown(Key.S))
                vmb.MoveLeftPadDown();
            if (Keyboard.IsKeyDown(Key.Up))
                vmb.MoveRightPadUp();
            if (Keyboard.IsKeyDown(Key.Down))
                vmb.MoveRightPadDown();
        }
    }
}
