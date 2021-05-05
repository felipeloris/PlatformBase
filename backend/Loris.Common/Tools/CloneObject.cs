using Loris.Common.Domain.Interfaces;
using System.Runtime.Serialization;

namespace Loris.Common.Tools
{
    public static class CloneObject
    {
        public static T Clone<T, TId>(T obj) where T : ISerializable, IEntityId<TId>
        {
            var baClone = SerializeObject.ObjectToByteArray(obj);
            var clone = (T)SerializeObject.ByteArrayToObject(baClone);
            clone.Id = default;

            return clone;
        }
    }
}
