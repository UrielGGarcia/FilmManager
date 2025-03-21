﻿using System.Windows;
using FilmManager.Core.Services.CustomerService;
using FilmManager.Core.Services.CustomerService.Network;
using FilmManager.Core.Services.Films;
using FilmManager.Core.Services.Films.Network;
using FilmManager.Core.Services.inventoryService;
using FilmManager.Core.Services.inventoryService.Network;
using FilmManager.Core.Services.Navigate;
using FilmManager.Core.Services.Rentals;
using FilmManager.Core.Services.Rentals.Network;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services = InitServices();
        InitializeComponent();
    }

    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;

    private IServiceProvider InitServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<IFilmService, FilmService>();
        services.AddSingleton<FilmServiceNetwork>();

        services.AddSingleton<RentalsServiceNetwork>();
        services.AddSingleton<IRentalsService, RentalsService>();

        services.AddSingleton<CustomerServiceNetwork>();
        services.AddSingleton<ICustomerService, CustomerService>();

        services.AddSingleton<InventoryServiceNetwork>();
        services.AddSingleton<IInventoryService, InventoryService>();

        return services.BuildServiceProvider();
    }
}