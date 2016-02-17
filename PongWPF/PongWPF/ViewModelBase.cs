using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PongWPF
{
    class ViewModelBase: INotifyPropertyChanged
    {
        #region Attributes
        Ball ball = new Ball();
        Pad leftPad = new Pad();
        Pad rightPad = new Pad();
        const int CTE_HEIGHT = 400;
        const int CTE_WIDTH = 800;
        private double _angle = 155;
        private double _speed = 5;
        private int _padSpeed = 7;
        #endregion

        #region Properties
        public Ball Ball
        {
            get
            {
                return ball;
            }

            set
            {
                ball = value;
                OnPropertyChanged("Ball");
            }
        }

        public Pad LeftPad
        {
            get
            {
                return leftPad;
            }

            set
            {
                leftPad = value;
                OnPropertyChanged("LeftPad");
            }
        }

        public Pad RightPad
        {
            get
            {
                return rightPad;
            }

            set
            {
                rightPad = value;
                OnPropertyChanged("RightPad");
            }
        }
        #endregion

        #region Constructor
        public ViewModelBase()
        {
            NuevoJuego();
        }
        #endregion

        #region Methods
        internal void MoveLeftPadUp()
        {
            leftPad.YPosition -= _padSpeed;
        }

        internal void MoveLeftPadDown()
        {
            leftPad.YPosition += _padSpeed;
        }

        internal void MoveRightPadUp()
        {
            rightPad.YPosition -= _padSpeed;
        }

        internal void MoveRightPadDown()
        {
            rightPad.YPosition += _padSpeed;
        }

        private void NuevoJuego()
        {
            Ball.X = CTE_WIDTH / 2;
            Ball.Y = CTE_HEIGHT / 2;
            Ball.MovingRight = true;
            Ball.LeftResult = 0;
            ball.RightResult = 0;
            LeftPad.YPosition = CTE_HEIGHT / 2;
            RightPad.YPosition = CTE_HEIGHT / 2;
            _angle = 155;
        }

        public void Timer_Tick()
        {
            if (ball.Y <= 0) _angle = _angle + (180 - 2 * _angle);
            if (ball.Y >= CTE_HEIGHT - 20) _angle = _angle + (180 - 2 * _angle);

            if (CheckCollision())
            {
                ChangeAngle();
                ChangeDirection();
            }

            double radians = (Math.PI / 180) * _angle;
            Vector vector = new Vector { X = Math.Sin(radians), Y = -Math.Cos(radians) };
            ball.X += vector.X * _speed;
            ball.Y += vector.Y * _speed;

            if (ball.X >= 790)
            {
                ball.LeftResult += 1;
                GameReset();
            }
            if (ball.X <= 5)
            {
                ball.RightResult += 1;
                GameReset();
            }

        }

        private void GameReset()
        {
            Ball.X = CTE_WIDTH / 2;
            Ball.Y = CTE_HEIGHT / 2;
        }

        private void ChangeAngle()
        {
            if (ball.MovingRight == true) _angle = 270 - ((ball.Y + 10) - (rightPad.YPosition + 40));
            else if (ball.MovingRight == false) _angle = 90 + ((ball.Y + 10) - (leftPad.YPosition + 40));
        }

        private void ChangeDirection()
        {
            if (ball.MovingRight == true) ball.MovingRight = false;
            else if (ball.MovingRight == false) ball.MovingRight = true;
        }

        private bool CheckCollision()
        {
            bool collisionResult = false;
            if (ball.MovingRight == true)
                collisionResult = ball.X >= 760 && (ball.Y > rightPad.YPosition  && ball.Y < rightPad.YPosition + 80);

            if (ball.MovingRight == false)
                collisionResult = ball.X <= 20 && (ball.Y > leftPad.YPosition  && ball.Y < leftPad.YPosition + 80);

            return collisionResult;
        }

        #endregion

        #region ClickCommand

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), CanExecuteAction()));
            }
        }

        private bool CanExecuteAction()
        {
            return true;
        }

        public void MyAction()
        {
            NuevoJuego();
        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
