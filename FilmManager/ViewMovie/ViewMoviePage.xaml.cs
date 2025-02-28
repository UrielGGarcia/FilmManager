using System.Windows.Controls;

namespace FilmManager.ViewMovie;

public partial class ViewMoviePage : Page
{
    public ViewMoviePage()
    {
        InitializeComponent();
        var viewModel = new ViewMovieViewModel();
        DataContext = viewModel;
        viewModel.LoadFilms();
    }
}