using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongWPF
{
    class Pad : INotifyPropertyChanged
    {
        #region Attributes
        private int _yPosition;
        #endregion

        #region Properties
        public int YPosition
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                OnPropertyChanged("YPosition");
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
