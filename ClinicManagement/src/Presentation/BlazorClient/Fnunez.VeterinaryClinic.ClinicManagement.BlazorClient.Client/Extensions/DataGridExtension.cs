namespace Radzen.Blazor;

public static class DataGridExtension
{
    public static async Task ReloadAfterDeleteItemAsync<T>(this RadzenDataGrid<T> dataGrid)
    {
        if (dataGrid.CurrentPage > 0 && dataGrid.Data.Count() == 1)
            await dataGrid.PrevPage();
        else
            await dataGrid.Reload();
    }

    public static async Task ReloadAfterDeletePageAsync<T>(this RadzenDataGrid<T> dataGrid)
    {
        if (dataGrid.CurrentPage == 0)
        {
            await dataGrid.Reload();
            return;
        }

        int take = dataGrid.Query.Top ?? 0;

        int pageCount = (int)Math.Ceiling((decimal)dataGrid.Count / (decimal)take);

        int currentPage = dataGrid.CurrentPage + 1;

        bool isLastPage = currentPage == pageCount;

        if (isLastPage)
            await dataGrid.PrevPage();
        else
            await dataGrid.Reload();
    }
}