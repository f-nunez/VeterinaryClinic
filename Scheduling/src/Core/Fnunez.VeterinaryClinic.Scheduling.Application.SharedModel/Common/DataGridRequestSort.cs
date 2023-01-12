namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

public class DataGridRequestSort
{
    public bool IsAscending { get; set; }
    public string PropertyName { get; set; }

    public DataGridRequestSort(string propertyName, bool isAscending = true)
    {
        if (string.IsNullOrEmpty(propertyName))
            throw new ArgumentNullException(nameof(propertyName));

        PropertyName = propertyName;
        IsAscending = isAscending;
    }
}