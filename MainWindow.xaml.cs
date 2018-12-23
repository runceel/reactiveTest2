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
using Reactive.Bindings;
using System.ComponentModel;
using Reactive.Bindings.Extensions;

namespace ReactiveTest
{

	public class MainModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public List<string> NamesSource{get; set;} = new List<string>();
		public MainModel()
		{
			NamesSource.Add( "AAAAAAAAAAAAAA" );
			NamesSource.Add( "BBBBBBBBBBBBBB" );
		}

		public void Add()
		{
			NamesSource.Add( "CCCCC" );

			// ここで新しくリストを生成すると表示が更新される
			// NamesSource = new List<string>( NamesSource );


			PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof(NamesSource) ) );
		}
	}

	public class MainWindowViewModel
	{
		public MainModel model = new MainModel();
		public ReactiveProperty<List<string> > Names { get; set;}
		public MainWindowViewModel()
		{
			Names = model.ObserveProperty( x => x.NamesSource ).ToReactiveProperty();
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

		private void Window_KeyDown( object sender, KeyEventArgs e )
		{
			viewModel.model.Add();
		}
	}
}
