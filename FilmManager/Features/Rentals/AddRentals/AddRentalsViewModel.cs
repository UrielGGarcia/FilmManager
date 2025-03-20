using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmManager.Core.Services.CustomerService;
using FilmManager.Core.Services.Films;
using FilmManager.Core.Services.inventoryService;
using FilmManager.Core.Services.Navigate;
using FilmManager.Core.Services.Rentals;
using FilmManager.Features.Rentals.ViewRentals;
using FilmManager.Models.Customer;
using FilmManager.Models.Film;
using FilmManager.Models.Inventory;
using FilmManager.Models.Rentals;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FilmManager.Features.Rentals.AddRentals;

public partial class AddRentalsViewModel : ObservableObject
{
    private readonly ICustomerService _customerService = App.Current.Services.GetService<ICustomerService>()!;

    private readonly IFilmService _filmService = App.Current.Services.GetService<IFilmService>()!;
    private readonly IInventoryService _inventoryService = App.Current.Services.GetService<IInventoryService>()!;
    private readonly INavigationService _navigationService = App.Current.Services.GetService<INavigationService>()!;
    private readonly IRentalsService _rentalService = App.Current.Services.GetService<IRentalsService>()!;
    [ObservableProperty] private RentalsModel? __newRental;
    [ObservableProperty] private ObservableCollection<CustomerModel> _customers;
    [ObservableProperty] private DateTime _fechaCreacion = DateTime.Now;
    [ObservableProperty] private DateTime _fechaRegreso;
    [ObservableProperty] private Film? _film;
    [ObservableProperty] private ObservableCollection<InventoryModel> _inventories;
    [ObservableProperty] private CustomerModel? _selectedCustomer;
    [ObservableProperty] private InventoryModel? _selectedInventory;
    [ObservableProperty] private List<int> _staffList;
    [ObservableProperty] private int staffIdSelected;


    public AddRentalsViewModel()
    {
        StaffList = [1, 2];
        Film = _filmService.GetFilmSelected();
        FechaRegreso = DateTime.Now.AddDays(4);
        FechaCreacion = DateTime.Now;
        LoadInventoriesAsync();
        LoadCustomersAsync();
    }

    private async Task LoadCustomersAsync()
    {
        Customers = await _customerService.GetListCustomers();
    }

    private async Task LoadInventoriesAsync()
    {
        Inventories = await _inventoryService.GetInventoryByFilmId(Film.FilmId);
    }


    [RelayCommand]
    public void OnCrearObjeto()
    {
        if (NewRental == null) NewRental = new RentalsModel();

        if (SelectedCustomer != null && SelectedInventory != null)
        {
            NewRental.CustomerId = SelectedCustomer.CustomerId;
            NewRental.RentalDate = FechaCreacion;
            NewRental.ReturnDate = FechaCreacion;
            NewRental.LastUpdate = DateTime.Now;
            NewRental.InventoryId = SelectedInventory.InventoryId;
            NewRental.StaffId = staffIdSelected;
            
            
            var result = MessageBox.Show("¿Está seguro que desea guardar la renta creado?",
                "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.OK)
            {
                _rentalService.AddRental(NewRental);

                _navigationService.Navigate(typeof(ViewRentalsPage));
            }
        }
        else
        {
            MessageBox.Show("Selecciona un cliente e inventario antes de crear la renta.",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        
    }
}