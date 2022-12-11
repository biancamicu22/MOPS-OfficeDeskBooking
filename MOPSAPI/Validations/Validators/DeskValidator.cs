using DataLibrary.Models;
using MOPSAPI.Repository;
using MOPSAPI.Repository.Desk;
using MOPSAPI.Validations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOPSAPI.Validations.Validators
{
    public class DeskValidator : AbstractEntityHandler<Desk>
    {
        public IServiceProvider ServiceProvider { get; }
        public DeskValidator(Desk newEntity, IDeskRepository deskRepository, IServiceProvider serviceProvider) :
            base(newEntity, deskRepository)
        {
            ServiceProvider = serviceProvider;
        }

        protected override void applyChanges()
        {
            dbEntity.DeskNumber = newEntity.DeskNumber;
            dbEntity.NumberOfMonitors = newEntity.NumberOfMonitors;
        }

        protected override List<ValidationKeyValue> validate()
        {
            var validationResult = new List<ValidationKeyValue>();

            validationResult.ValidateNumber("deskNumber", dbEntity.DeskNumber, 0, 9999);
            validationResult.ValidateNumber("deskNumber", dbEntity.NumberOfMonitors, 0, 9999);

            return validationResult;
        }
    }
}
