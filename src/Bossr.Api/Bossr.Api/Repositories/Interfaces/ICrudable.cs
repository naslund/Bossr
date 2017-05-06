﻿using Bossr.Lib.Models.Interfaces;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface ICrudable<T> : 
        ICreateable<T>,
        IDeletableById,
        IListable<T>,
        IReadableById<T>,
        IUpdatable<T>
        where T : IEntity 
    {
    }
}
