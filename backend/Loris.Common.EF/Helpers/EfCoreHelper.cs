using Loris.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Loris.Common.EF.Helpers
{
    public static class EfCoreHelper
    {
        public static DbContext GetContext<T>(DbSet<T> dbSet) where T : class
        {
            //var infrastructure = dbSet as IInfrastructure<IServiceProvider>;
            //var serviceProvider = infrastructure.Instance;
            //var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext)) as ICurrentDbContext;
            //currentDbContext.Context

            var dbContext = dbSet.GetService<ICurrentDbContext>().Context;

            return dbContext;
        }

        public static string GetTableName<T>(DbContext dbContext) where T : class
        {
            var model = dbContext.Model;
            var entityTypes = model.GetEntityTypes();
            var entityTypeOfT = entityTypes.First(t => t.ClrType == typeof(T));
            var tableNameAnnotation = entityTypeOfT.GetAnnotation("Relational:TableName");
            var tableName = tableNameAnnotation.Value.ToString();

            return tableName;
        }

        public static TableInfo GetTableInfo<T>(DbContext dbContext)
        {
            var entityType = dbContext.Model.FindEntityType(typeof(T));

            // Table info 
            var tableInfo = new TableInfo()
            {
                Name = entityType.GetTableName(),
                Schema = entityType.GetSchema()
            };

            // Column info 
            foreach (var property in entityType.GetProperties())
            {
                tableInfo.Columns.Add(new ColumnInfo()
                {
                    Name = property.Name,
                    Type = property.FieldInfo.FieldType,
                    ColumnName = property.GetColumnName(),
                    ColumnType = property.GetColumnType()
                });
            };

            return tableInfo;
        }

        public static TableAttribute GetTableAttribute<T>() 
        {
            var ta = (TableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
            return ta;
        }

        public static TContext GetContext<TContext>(string cnn) where TContext : DbContext
        {
            var optBuilder = new DbContextOptionsBuilder<TContext>();
            optBuilder.UseNpgsql(cnn);
            var context = (TContext)Activator.CreateInstance(typeof(TContext), optBuilder.Options);
            return context;
        }
    }
}
