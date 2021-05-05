using Loris.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Loris.Common.Extensions;

namespace Loris.Common.EF.Helpers
{
    public static class PaginatedListHelper
    {
        public static async Task<PageResult<T>> GetAsync<T>(IQueryable<T> query, int pageNumber, int pageSize)
            where T : class
        {
            var count = await query.CountAsync();
            var paging = new PageResult<T>(count, pageNumber, pageSize);
            var items = await query
                .Skip(paging.NextRecord)
                .Take(pageSize)
                .ToListAsync();
            paging.Results = items;
            return paging;
        }

        public static async Task<PageResult<T>> GetAsync<T>(DbSet<T> dbSet, Expression<Func<T, bool>> where,
            RequestParameter param)
            where T : class
        {
            var query = dbSet.AsQueryable();
            if (where != null)
                query = query.Where(where);
            if (!string.IsNullOrEmpty(param.SortField))
                query = query.Sorting(param.SortField, param.SortOrder);
            return await GetAsync(query.AsQueryable(), param.PageIndex, param.PageSize);
        }

        public static async Task<PageResult<T>> GetAsync<T>(DbSet<T> dbSet, RequestParameter param)
            where T : class
        {
            var dbContext = EfCoreHelper.GetContext<T>(dbSet);
            var tableInfo = EfCoreHelper.GetTableInfo<T>(dbContext);

            // Nome da tabela e esquema
            var tableName = tableInfo.Name;
            if (!string.IsNullOrEmpty(tableInfo.Schema))
                tableName = $"{tableInfo.Schema}.{tableName}";

            // Monta o Sql
            var sql = $"SELECT * FROM {tableName}{param.SqlWhere(tableInfo.Columns)}{param.SqlOrderby(tableInfo.Columns)}";

            // Executa a consulta
            var query = dbSet.FromSqlRaw(sql).AsQueryable();
            return await GetAsync(query.AsQueryable(), param.PageIndex, param.PageSize);
        }
    }
}
