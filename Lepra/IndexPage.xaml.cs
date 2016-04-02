﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Lepra.Annotations;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Lepra
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IndexPage : Page, INotifyPropertyChanged
    {
        private DataService _dataService;
        private ObservableCollection<Doc> _items;

        public IndexPage()
        {
            this.InitializeComponent();

            _dataService = ((App)App.Current).DataService;
        }

        public ObservableCollection<Doc> Items
        {
            get { return _items; }
            set
            {
                if(value == _items)
                    return;

                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var indexData = await _dataService.GetIndexPage();

            if(indexData.Status != "OK") return;

            Items = new ObservableCollection<Doc>(indexData.Docs);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
