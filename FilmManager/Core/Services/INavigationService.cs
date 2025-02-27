using Wpf.Ui.Controls;

namespace FilmManager.Core.Services;

public interface INavigationService
{
    void Navigate(Type pageType);

    void SetNavigationControl(INavigationView navigation); 
}