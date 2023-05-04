namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

public class DataGridResponse<T>
{
    public List<T> Items { get; private set; }
    public int Count { get; private set; }

    public DataGridResponse(
        List<T> items,
        int count)
    {
        Items = items;
        Count = count;
    }
}