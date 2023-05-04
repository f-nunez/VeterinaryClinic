using System.Data.Common;
using System.Globalization;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.VeterinaryClinic.SharedKernel.Infrastructure.EntityFrameworkCore;

public static class SqlQueryExtension
{
    public static async Task<List<T>> GetFromQueryAsync<T>(
            this DbContext dbContext,
            string sql,
            IEnumerable<object> parameters,
            CancellationToken cancellationToken = default)
    {
        using DbCommand command = dbContext.Database
            .GetDbConnection()
            .CreateCommand();

        command.CommandText = sql;

        if (parameters != null)
        {
            int index = 0;
            foreach (object item in parameters)
            {
                DbParameter dbParameter = command.CreateParameter();
                dbParameter.ParameterName = "@p" + index;
                dbParameter.Value = item;
                command.Parameters.Add(dbParameter);
                index++;
            }
        }

        try
        {
            await dbContext.Database.OpenConnectionAsync(cancellationToken);

            using DbDataReader result = await command
                .ExecuteReaderAsync(cancellationToken);

            var list = new List<T>();

            T obj = default!;

            while (await result.ReadAsync(cancellationToken))
            {
                if (!(typeof(T).IsPrimitive || typeof(T).Equals(typeof(string))))
                {
                    obj = Activator.CreateInstance<T>();

                    foreach (PropertyInfo prop in obj!.GetType().GetProperties())
                    {
                        string propertyName = prop.Name;
                        bool isColumnExistent = result.ColumnExists(propertyName);

                        if (isColumnExistent)
                        {
                            object columnValue = result[propertyName];

                            if (!Equals(columnValue, DBNull.Value))
                                prop.SetValue(obj, columnValue, null);
                        }
                    }

                    list.Add(obj);
                }
                else
                {
                    obj = (T)Convert.ChangeType(
                        result[0],
                        typeof(T),
                        CultureInfo.InvariantCulture
                    );

                    list.Add(obj);
                }
            }

            return list;
        }
        finally
        {
            await dbContext.Database.CloseConnectionAsync();
        }
    }

    public static async Task<List<T>> GetFromQueryAsync<T>(
        this DbContext dbContext,
        string sql,
        IEnumerable<DbParameter> parameters,
        CancellationToken cancellationToken = default)
    {
        using DbCommand command = dbContext.Database
            .GetDbConnection()
            .CreateCommand();

        command.CommandText = sql;

        if (parameters != null)
            foreach (DbParameter dbParameter in parameters)
                command.Parameters.Add(dbParameter);

        try
        {
            await dbContext.Database.OpenConnectionAsync(cancellationToken);

            using DbDataReader result = await command
                .ExecuteReaderAsync(cancellationToken);

            var list = new List<T>();

            T obj = default!;

            while (await result.ReadAsync(cancellationToken))
            {
                if (!(typeof(T).IsPrimitive || typeof(T).Equals(typeof(string))))
                {
                    obj = Activator.CreateInstance<T>();

                    foreach (PropertyInfo prop in obj!.GetType().GetProperties())
                    {
                        string propertyName = prop.Name;
                        bool isColumnExistent = result.ColumnExists(propertyName);

                        if (isColumnExistent)
                        {
                            object columnValue = result[propertyName];

                            if (!Equals(columnValue, DBNull.Value))
                                prop.SetValue(obj, columnValue, null);
                        }
                    }

                    list.Add(obj);
                }
                else
                {
                    obj = (T)Convert.ChangeType(
                        result[0],
                        typeof(T),
                        CultureInfo.InvariantCulture
                    );

                    list.Add(obj);
                }
            }

            return list;
        }
        finally
        {
            await dbContext.Database.CloseConnectionAsync();
        }
    }
}