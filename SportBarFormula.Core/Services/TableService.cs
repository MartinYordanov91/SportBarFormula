using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Table;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services;

public class TableService(
    IRepository<Table> repository
    ) : ITableService
{
    private readonly IRepository<Table> _repository = repository;

    public async Task<IEnumerable<TableViewModel>> GetAllTablesAsync()
    {
        var allTables = await _repository.GetAllAsync();


        var tableToReturn = allTables
            .Where(t => t.IsAvailable == true)
            .Select(t => new TableViewModel()
            {
                TableId = t.TableId,
                TableNumber = t.TableNumber,
                IsAvailable = t.IsAvailable,
                Capacity = t.Capacity,
                Location = t.Location,
            }).ToList();

        return tableToReturn;
    }
}
