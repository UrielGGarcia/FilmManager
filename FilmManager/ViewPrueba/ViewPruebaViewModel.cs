using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Input;

namespace FilmManager.ViewPrueba;

public partial class ViewPruebaViewModel : ObservableObject
{
    [RelayCommand]

    private void OnSaludo()
    {
        MessageBox.Show("¡ANGELLLLLLLL TE AMO!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
    }

}