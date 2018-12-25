﻿using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ReactiveTest
{

    public class MainModel
    {
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
        public ReadOnlyObservableCollection<string> Names { get; }
        public MainWindowViewModel()
        {
            Names = new ReadOnlyObservableCollection<string>(model.NamesSource);
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
