namespace SportBarFormula.Core.ViewModels.Table;

public class TableViewModel
{
    public int TableId { get; set; }

    public  string TableNumber { get; set; } = string.Empty;

    public  int Capacity { get; set; }

    public string? Location { get; set; }

    public bool IsAvailable { get; set; } = true;
}
