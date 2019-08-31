using System.Windows;
using MemberAppClient.ViewModel;

namespace MemberAppClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MemberViewModel();
        }
    }
}
