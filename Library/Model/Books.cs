using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Books : INotifyPropertyChanged
    {
        private string? autor;
        private short acr;
        private DateOnly age;
        private int count;
        public string? Autor
        {
            get { return autor; }
            set { autor = value; OnPropertyChanged("Autor"); }
        }
        public short Acr
        {
            get { return acr; }
            set { acr = value; OnPropertyChanged("Acr"); }
        }
        public DateOnly Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged("Age"); }
        }
        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged("Count"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
