using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PassportDatesCheck
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        private string _DateSecondPart = "";
        public string DateSecondPart
        {
            get { return _DateSecondPart; }
            set
            {
                if (value == _DateSecondPart) return;
                _DateSecondPart = value;
                OnPropertyChanged();
                OnPropertyChanged("DateFirstPart");
                OnPropertyChanged("Turns");
            }
        }

        public string DateFirstPart
        {
            get
            {
                if (Int32.TryParse(DateSecondPart, out int dateSecondPart) &&
                    DateSecondPart.Length == 2 &&
                    dateSecondPart <= DateTime.Now.Year % 100)
                {
                    return "20";
                }
                return "19";
            }
        }

        public int DateFull
        {
            get
            {
                if (Int32.TryParse(DateFirstPart, out int dateFirstPart) &&
                    DateSecondPart.Length == 2 &&
                    Int32.TryParse(DateSecondPart, out int dateSecondPart))
                    return dateFirstPart * 100 + dateSecondPart;

                return 0;
            }
        }


        public class turns_view_model
        {
            public string Description { get; set; }
            public string Year { get; set; }
            public string FontWeihht { get; set; }
        }
        public int[] ShowedDates = new int[] { 12, 14, 16, 18, 25, 45 }; //All dates we must to show

        public ObservableCollection<turns_view_model> Turns
        {
            get
            {
                var rezult = new ObservableCollection<turns_view_model>();
                int date = DateFull;
                int _year_last_value = DateTime.Now.Year+100;

                foreach (var d in ShowedDates.Reverse())
                {
                    string _description = Convert.ToInt32(d) + " р. - ";
                    string _year = "";
                    string _fontWeihht = "Normal";

                    int y = 0;

                    if (date != 0)
                    {
                        y = date + Convert.ToInt32(d);
                        _year = y.ToString();
                    }

                    if (_year_last_value > DateTime.Now.Year &&
                        y <= DateTime.Now.Year)
                        _fontWeihht = "Bold";

                    _year_last_value = y;
                    rezult.Insert(0, new turns_view_model { Description = _description, Year = _year, FontWeihht = _fontWeihht });
                }
                return rezult;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
