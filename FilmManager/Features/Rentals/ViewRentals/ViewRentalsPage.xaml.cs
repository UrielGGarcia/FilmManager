using System.Windows.Controls;
using System.Windows.Input;

namespace FilmManager.Features.Rentals.ViewRentals;

public partial class ViewRentalsPage : Page
{
    public ViewRentalsPage()
    {
        InitializeComponent();
    }
    //CREAR METODO DE DOBLE CLICK   
    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var datacontext = DataContext as ViewRentalsViewModel;
        datacontext?.FilmClickCommand.Execute(null);
    }
}