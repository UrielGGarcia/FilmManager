using System.Collections.ObjectModel;
using FilmManager.Core.Services.inventoryService.Network;
using FilmManager.Models.Inventory;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.Core.Services.inventoryService;

public class InventoryService : IInventoryService
{
    private ObservableCollection<InventoryModel> _inventories = new();
    private IInventoryService _inventoryServiceImplementation;

    private InventoryServiceNetwork? _inventoryServiceNetwork =
        App.Current.Services.GetService<InventoryServiceNetwork>();


    public async Task<ObservableCollection<InventoryModel>> GetInventoryByFilmId(int filmId)
    {
        _inventories = await InventoryServiceNetwork.GetInventoryByFilmId(filmId);
        return _inventories;
    }
}