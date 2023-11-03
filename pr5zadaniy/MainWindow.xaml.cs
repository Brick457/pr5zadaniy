using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static pr5zadaniy.MainWindow;

namespace pr5zadaniy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public class MyConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                string text = values[0] as string;
                bool isChecked = (bool)values[1];

                if (!string.IsNullOrEmpty(text) && isChecked)
                {
                    return true;
                }

                return false;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public RelayCommand(Action <object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }
            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
            public bool CanExecute(object parameter) {
                return _canExecute == null || _canExecute(parameter);
            }
            public void Execute(object parameter)
            {
                _execute(parameter);
            }
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private RelayCommand? _createCommand;

        public ICommand CreateCommand
        {
            get
            {
                return _createCommand ?? (_createCommand = new RelayCommand(param => CreateNewCommand()));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void CreateNewCommand()
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("log.txt", true))
            {
                writer.WriteLine("Выход из приложения: " + DateTime.Now.ToShortDateString() + " " +
                    DateTime.Now.ToLongDateString());
                writer.Flush();
            }
      
        }
      
    }
        
}
