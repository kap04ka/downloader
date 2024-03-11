using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ILink> links = new ObservableCollection<ILink>();
        Downloader downloader;

        public MainWindow()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            downloader = new Downloader(links);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            links.Add(new Link("", 1));
            linksList.ItemsSource = links;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (linksList.SelectedIndex != -1)
                links.RemoveAt(linksList.SelectedIndex);
        }
    }
}
