using DataLibrary.Models;
using MOPSAPI.Validations.Models;
using System;

namespace MOPSAPI.Validations
{
    public interface IEntityUpdateHandler
    {
        EntityHandlerResult<TSuccess> Update<TSuccess>(IEntity entity) where TSuccess : class;
    }
}