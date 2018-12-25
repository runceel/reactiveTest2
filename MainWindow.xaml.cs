using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ReactiveTest
{

    public class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> NamesSource { get; } = new ObservableCollection<string>();
        public MainModel()
        {
            NamesSource.Add("AAAAAAAAAAAAAA");
            NamesSource.Add("BBBBBBBBBBBBBB");
        }

        public void Add()
        {
            NamesSource.Add("CCCCC");
        }
    }

    public class MainWindowViewModel
    {
        public MainModel model = new MainModel();
        public ReactiveProperty<ObservableCollection<string>> Names { get; set; }
        public MainWindowViewModel()
        {
            Names = model
                .ObserveProperty(x => x.NamesSource)
                .ToReactiveProperty();
        }
    }
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            viewModel.model.Add();
        }
    }
}
