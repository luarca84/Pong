using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongWPF
{
    class Ball : INotifyPropertyChanged
    {
        #region Attributes
        private double _x;
        private double _y;
        private bool _movingRight;
        private int _leftResult;
        private int _rightResult;
        #endregion

        #region Properties
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        public bool MovingRight
        {
            get { return _movingRight; }
            set
            {
                _movingRight = value;
                OnPropertyChanged("MovingRight");
            }
        }

        public int LeftResult
        {
            get { return _leftResult; }
            set
            {
                _leftResult = value;
                OnPropertyChanged("LeftResult");
            }
        }

        public int RightResult
        {
            get { return _rightResult; }
            set
            {
                _rightResult = value;
                OnPropertyChanged("RightResult");
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
