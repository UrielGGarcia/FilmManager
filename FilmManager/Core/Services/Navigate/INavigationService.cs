using Wpf.Ui.Controls;

namespace FilmManager.Core.Services.Navigate;

public interface INavigationService
{
    void Navigate(Type pageType);

    void SetNavigationControl(INavigationView navigation);
}