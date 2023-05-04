namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

public class DataGridRequest
{
    public string Search { get; set; } = string.Empty;
    public int? Skip { get; set; }
    public List<DataGridRequestSort>? Sorts { get; set; }
    public int? Take { get; set; }

    public override string ToString()
    {
        string sortProperties = string.Empty;
        if (Sorts != null)
            sortProperties = string.Join(',', Sorts.Select(s => $"{s.PropertyName} isAsc:{s.IsAscending.ToString()}"));

        return $"Sorts: {sortProperties}, Search: {Search}, Skip: {Skip}, Take: {Take}";
    }
}