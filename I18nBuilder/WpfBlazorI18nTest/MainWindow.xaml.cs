using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfBlazorI18nTest.I18nBuilder.Extension;
namespace WpfBlazorI18nTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddWpfBlazorWebView();
            serviceCollection.AddI18nBuilderService(option =>
            {
                option.DefaultLanguage = "ja";
                option.Languages = ["ja", "en"];
            });
            Resources.Add("services", serviceCollection.BuildServiceProvider());
        }
    }
}