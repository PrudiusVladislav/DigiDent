using DigiDent.InventoryManagement.Domain.Items;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.InventoryManagement.Persistence.Items;

public class InventoryItemsRepository
{
    private readonly InventoryManagementDbContext _dbContext;

    public InventoryItemsRepository(InventoryManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
}