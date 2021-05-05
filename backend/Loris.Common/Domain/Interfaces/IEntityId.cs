using System;

namespace Loris.Common.Domain.Interfaces
{
    public interface IEntityIdBase { }

    public interface IEntityId<TypeId> : IEntityIdBase
    {
        TypeId Id { get; set; }
    }

    public interface IEntityIdInt : IEntityId<int> { }

    public interface IEntityIdLong : IEntityId<long> { }

    public interface IEntityIdString : IEntityId<string> { }

    public interface IEntityIdGuid : IEntityId<Guid?> { }
}
