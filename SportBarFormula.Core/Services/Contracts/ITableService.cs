using SportBarFormula.Core.ViewModels.Table;

namespace SportBarFormula.Core.Services.Contracts;

public interface ITableService
{

    Task<IEnumerable<TableViewModel>> GetAllTablesAsync(); 
}
