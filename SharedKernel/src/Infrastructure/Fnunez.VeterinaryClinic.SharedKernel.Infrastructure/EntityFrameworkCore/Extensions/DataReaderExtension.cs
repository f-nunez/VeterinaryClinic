using System.Data;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public static class DataReaderExtension
{
    public static bool ColumnExists(this IDataReader reader, string columnName)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        for (int i = 0; i < reader.FieldCount; i++)
            if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                return true;

        return false;
    }
}