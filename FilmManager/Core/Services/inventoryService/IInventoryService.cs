using System.Collections.ObjectModel;
using FilmManager.Models.Inventory;

namespace FilmManager.Core.Services.inventoryService;

public interface IInventoryService
{
    Task<ObservableCollection<InventoryModel>> GetInventoryByFilmId(int filmId);
}