using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using Loris.Common.Helpers;
using System;

namespace Loris.Common.Extensions
{
    public static class EntityIdExtension
    {
        public static object GetId(this IEntityIdBase entityId)
        {
            if (entityId is IEntityIdInt)
                return (entityId as IEntityIdInt).Id;
            else if (entityId is IEntityIdLong)
                return (entityId as IEntityIdLong).Id;
            else if (entityId is IEntityIdString)
                return (entityId as IEntityIdString).Id;
            else if (entityId is IEntityIdGuid)
                return (entityId as IEntityIdGuid).Id;

            return null;
        }

        public static bool HasId(this IEntityIdBase entityId)
        {
            if (entityId is IEntityIdInt)
                return (entityId as IEntityIdInt).Id > 0;
            else if (entityId is IEntityIdLong)
                return (entityId as IEntityIdLong).Id > 0;
            else if (entityId is IEntityIdString)
                return !string.IsNullOrEmpty((entityId as IEntityIdString).Id?.Trim());
            else if (entityId is IEntityIdGuid)
            {
                var guid = (entityId as IEntityIdGuid).Id;
                if (string.IsNullOrEmpty(guid?.ToString()))
                    return false;
                return true;
            }

            return false;
        }

        public static object ConvertId(this IEntityIdBase entityId, object value)
        {
            return EntityIdHelper.ConvertId(entityId.GetType(), value);
        }
    }
}
