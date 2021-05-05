using System.Collections.Generic;

namespace Loris.Common.Domain.Entities
{
    public class RequestParameter
    {
        public List<ReqParamFilter> Filters { get; set; } = new List<ReqParamFilter>();

        public string SortField { get; set; }

        public ReqParamSortOrder SortOrder { get; set; } = ReqParamSortOrder.Asc;

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class ReqParamFilter
    {
        public string Field { get; set; }

        public ReqParamCondition Condition { get; set; } = ReqParamCondition.Equal;

        public string Value { get; set; }
    }

    public enum ReqParamCondition
    {
        Auto = 0,
        Equal = 1,
        Different = 2,
        Bigger = 3,
        EqualOrBigger = 4,
        Less = 5,
        LessOrEqual = 6,
        Null = 7,
        NotNull = 8,
        Like = 9
    }

    public enum ReqParamSortOrder
    {
        Undefined = 0,
        Asc = 1,
        Desc = 2
    }
}
