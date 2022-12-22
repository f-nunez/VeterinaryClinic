using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Extensions;

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

        var dataGridRequest = new DataGridRequest
        {
            Skip = loadDataArgs.Skip,
            Sorts = sorts,
            Take = loadDataArgs.Top
        };

        return dataGridRequest;
    }
}