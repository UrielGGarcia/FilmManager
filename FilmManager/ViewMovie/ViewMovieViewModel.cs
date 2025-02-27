using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FilmManager.ViewMovie;

public partial class ViewMovieViewModel:ObservableObject
{
    [RelayCommand]
    private void OnView()
    {
        MessageBox.Show("Vamos a piste", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);

    }
    
}