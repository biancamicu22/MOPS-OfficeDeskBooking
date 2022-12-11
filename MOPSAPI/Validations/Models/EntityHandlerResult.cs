using DataLibrary.DTO;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MOPSAPI.Validations.Models
{
    [DataContract]
    public class EntityHandlerResult<TSuccess> where TSuccess: class
    {
        public EntityHandlerResult(List<ValidationKeyValue> validationResults, TSuccess successResult)
        {
            if (validationResults == null) validationResults = new List<ValidationKeyValue>();
            ValidationResults = validationResults;
            SuccessResult = successResult;
        }

        [DataMember]
        public List<ValidationKeyValue> ValidationResults { get; set; }

        [DataMember]
        public object Success { get; set; }

        public TSuccess SuccessResult { get; set; }

        public ObjectResult ToHttpResponse()
        {
            if (SuccessResult != null && ValidationResults.Count == 0)
            {
                Success = TryConvertToDto();
                return new ObjectResult(this) { StatusCode = 200 };
            }

            return new ObjectResult(this) { StatusCode = 400 };
        }

        private object TryConvertToDto()
        {
            try
            {
                var @switch = new Dictionary<Type, object> {
                { typeof(Desk), DeskDTO.FromModel(SuccessResult as Desk)},
                { typeof(Booking), BookingDTO.FromModel(SuccessResult as Booking)},
                { typeof(User), UserDTO.FromModel(SuccessResult as User)},
            };

                return @switch[typeof(TSuccess)];
            }
            catch 
            {
                return SuccessResult;
            }
        }
    }
}
