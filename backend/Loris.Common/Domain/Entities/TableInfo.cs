using System;
using System.Collections.Generic;

namespace Loris.Common.Domain.Entities
{
    public class TableInfo
    {
        public string Name { get; set; }

        public string Schema { get; set; }

        public List<ColumnInfo> Columns { get; set; } = new List<ColumnInfo>();
    }

    public class ColumnInfo
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public string ColumnName { get; set; }

        public string ColumnType { get; set; }
    }
}
