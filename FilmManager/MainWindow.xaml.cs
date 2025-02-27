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
using CommunityToolkit.Mvvm.Input;
using FilmManager.Core.Services;
using FilmManager.ViewMovie;
using FilmManager.ViewPrueba;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace FilmManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow 
{
    private INavigationService _navigationService = App.Current.Services.GetService<INavigationService>()!; 
    
    public MainWindow()
    {
        InitializeComponent();
        _navigationService.SetNavigationControl(NavigationView);
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        NavigationView.Navigate(typeof(ViewMoviePage));
    }

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}