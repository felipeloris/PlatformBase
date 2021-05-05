using Loris.Common.Domain.Interfaces;
using System;
using System.Linq;

namespace Loris.Common.Helpers
{
    public static class EntityIdHelper
    {
        public static object ConvertId(Type typeOfEntity, object value)
        {
            var interfaces = typeOfEntity.GetInterfaces();

            if (interfaces.Contains(typeof(IEntityIdInt)))
                return Convert.ToInt32(value);
            else if (interfaces.Contains(typeof(IEntityIdLong)))
                return Convert.ToInt64(value);
            else if (interfaces.Contains(typeof(IEntityIdString)))
                return Convert.ToString(value);
            else if (interfaces.Contains(typeof(IEntityIdGuid)))
                return new Guid(Convert.ToString(value));

            return null;
        }
    }
}
