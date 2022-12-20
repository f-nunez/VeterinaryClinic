namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

public class DataGridRequest
{
    public List<DataGridRequestSort>? Sorts { get; set; }
    public int? Skip { get; set; }
    public int? Take { get; set; }

    public override string ToString()
    {
        string sortProperties = string.Empty;
        if (Sorts != null)
            sortProperties = string.Join(',', Sorts.Select(s => s.PropertyName));

        return $"Skip: {Skip}, Take: {Take}, Sorts: {sortProperties}";
    }
}