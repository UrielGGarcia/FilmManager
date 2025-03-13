using System.Windows.Controls;
using System.Windows.Input;

namespace FilmManager.ViewMovie;

public partial class ViewMoviePage : Page
{
    public ViewMoviePage()
    {
        InitializeComponent();
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var datacontext = DataContext as ViewMovieViewModel;
        datacontext?.FilmClickCommand.Execute(null);
    }
}