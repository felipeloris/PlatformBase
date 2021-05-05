using Loris.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loris.Common.Extensions
{
    public static class RequestParameterExtension
    {
        public static string SqlWhere(this RequestParameter param, List<ColumnInfo> columns = null)
        {
            var filters = param.Filters
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .ToList();
            
            if (!filters.Any())
                return string.Empty;

            var sbSql = new StringBuilder();

            foreach (var filter in filters)
            {
                if (sbSql.Length == 0)
                    sbSql.Append(" WHERE");
                else
                    sbSql.Append(" AND");

                var fieldName = filter.Field;
                var fieldValue = filter.Value;
                var separator = "'";

                ColumnInfo column = null;
                if (columns != null)
                {
                    column = columns.FirstOrDefault(x => x.Name
                        .Equals(fieldName, StringComparison.InvariantCultureIgnoreCase));
                    if (column != null)
                    {
                        fieldName = column.ColumnName;

                        if (column.Type.IsNumeric())
                            separator = "";
                    }
                }

                // Monta a condição para o campo
                switch (filter.Condition)
                {
                    case ReqParamCondition.Auto:
                        if (column != null && column.Type == typeof(string))
                            sbSql.AppendLine($" {fieldName} LIKE '%{fieldValue}%'");
                        else 
                            sbSql.AppendLine($" {fieldName} = {separator}{fieldValue}{separator}");
                        break;
                    case ReqParamCondition.Equal:
                        sbSql.AppendLine($" {fieldName} = {separator}{fieldValue}{separator}");
                        break;
                    case ReqParamCondition.Different:
                        sbSql.AppendLine($" {fieldName} <> {separator}{fieldValue}{separator}");
                        break;
                    case ReqParamCondition.Bigger:
                        sbSql.AppendLine($" {fieldName} > {separator}{fieldValue}{separator}");
                        break;
                    case ReqParamCondition.EqualOrBigger:
                        sbSql.AppendLine($" {fieldName} >= {separator}{fieldValue}{separator}");
                        break;
                    case ReqParamCondition.Less:
                        sbSql.AppendLine($" {fieldName} < {separator}{fieldValue}{separator}");
                        break;
                    case ReqParamCondition.LessOrEqual:
                        sbSql.AppendLine($" {fieldName} <= {separator}{fieldValue}{separator}");
                        break;
                    case ReqParamCondition.Like:
                        sbSql.AppendLine($" {fieldName} LIKE '%{fieldValue}%'");
                        break;
                    case ReqParamCondition.Null:
                        sbSql.AppendLine($" {fieldName} IS NULL");
                        break;
                    case ReqParamCondition.NotNull:
                        sbSql.AppendLine($" {fieldName} IS NOT NULL");
                        break;
                }
            }

            return sbSql.ToString();
        }

        public static string SqlOrderby(this RequestParameter param, List<ColumnInfo> columns = null)
        {
            if (string.IsNullOrEmpty(param.SortField))
                return string.Empty;
            
            var sortField = param.SortField;
            if (columns != null)
            {
                var column = columns.FirstOrDefault(x => x.Name
                    .Equals(sortField, StringComparison.InvariantCultureIgnoreCase));
                if (column != null)
                    sortField = column.ColumnName;
            }
            
            var sortOrder = param.SortOrder == ReqParamSortOrder.Asc ? " ASC" : " DESC";
            var sql = $" ORDER BY {sortField} {sortOrder}";

            return sql;
        }

        #region Código não usado
        /*
        protected WebPaging<T> SelectWithPaging(FlexiGrid flexiGrid, string nomeTabela = null)
        {
            var sbSql = new StringBuilder();
            try
            {
                Logger.LogDebug(GetType(), "-> SelectWithPaging - Inicio - " + typeof(T));

                var webPaging = new WebPaging<T>();
                var sqlWhere = SqlWhere(flexiGrid);
                var sqlOrderBy = SqlOrderby(flexiGrid);

                var sqlFrom = flexiGrid.QFrom;
                if (string.IsNullOrEmpty(flexiGrid.QFrom))
                {
                    sqlFrom = $"FROM {(nomeTabela ?? typeof(T).Name)} (NOLOCK)";
                }

                #region Total de Registros

                sbSql.AppendLine("SELECT COUNT(*) AS total");
                sbSql.AppendLine(sqlFrom);
                sbSql.Append(sqlWhere);

                var recDataRecord = ExecuteSqlReturningSingleList(sbSql.ToString(), CommandType.Text);

                webPaging.QtdTotalRegistros = DbTools.CheckDbNull<int>(recDataRecord[0]["total"]);

                if (webPaging.QtdTotalRegistros == 0)
                {
                    webPaging.ListWithPaging = new List<T>();
                    return webPaging;
                }
                sbSql.Clear();

                #endregion

                #region Lista de Registros

                // Monta o 'SQL'
                //  'SELECT'
                var sqlSelect = "SELECT *";
                if (!string.IsNullOrEmpty(flexiGrid.QSelect))
                {
                    sqlSelect = flexiGrid.QSelect;
                }
                sbSql.AppendLine(sqlSelect);

                //  'FROM'
                sbSql.AppendLine(sqlFrom);

                //  'WHERE'
                sbSql.Append(sqlWhere);

                //  'ORDER BY'
                sbSql.Append(sqlOrderBy);

                // 'PAGING' (SQL SERVER 2012 OU SUPERIOR)
                //      "OFFSET ((@PageNumber - 1) * @RowspPage) ROWS"
                sbSql.AppendLine($"OFFSET (({flexiGrid.Page} - 1) * {flexiGrid.Rp}) ROWS");
                //      "FETCH NEXT @RowspPage ROWS ONLY;"
                sbSql.AppendLine($"FETCH NEXT {flexiGrid.Rp} ROWS ONLY;");

                var rsList = ExecuteSqlReturningSingleList(sbSql.ToString(), CommandType.Text);

                webPaging.ListWithPaging = Parser(rsList).ToList();

                #endregion

                Logger.LogDebug(GetType(), GetType().Name + "-> SelectWithPaging - Fim");

                return webPaging;
            }
            catch (Exception ex)
            {
                Logger.LogDebug(GetType(), $"{GetType().Name} -> Erro: {ex.Message} - SQL: {sbSql}");
                if (DataAccessExceptionHandler.HandleException(ref ex))
                {
                    throw ex;
                }
                throw;
            }
        }         
         */
        #endregion
    }
}
