using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Radzen;

public static class LoadDataArgsExtension
{
    public static DataGridRequest GetDataGridRequest(this LoadDataArgs loadDataArgs)
    {
        List<DataGridRequestSort> sorts = new();

        if (loadDataArgs.Sorts != null)
        {
            sorts.AddRange(
                loadDataArgs.Sorts.Select(x =>
                    new DataGridRequestSort(
                        x.Property,
                        x.SortOrder != null ? x.SortOrder == SortOrder.Ascending : true
                    )
                )
            );
        }

        if (loadDataArgs.Sorts is null && !string.IsNullOrEmpty(loadDataArgs.OrderBy))
        {
            string[] orderBy = loadDataArgs.OrderBy.Split(' ');
            sorts.Add(
                new DataGridRequestSort(
                    orderBy[0],
                    orderBy[1] == "asc"
                )
            );
        }

        var dataGridRequest = new DataGridRequest
        {
            Search = loadDataArgs.Filter,
            Skip = loadDataArgs.Skip ?? default,
            Sorts = sorts,
            Take = loadDataArgs.Top ?? default
        };

        return dataGridRequest;
    }
}