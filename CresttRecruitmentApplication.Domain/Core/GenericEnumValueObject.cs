using System;

namespace CresttRecruitmentApplication.Domain.Core
{
    public abstract class GenericEnumValueObject<TType> : GenericValueObjectWithValidation<TType> where TType : Enum// TODO CR super, że robisz VO dla enuma!
    {
        protected GenericEnumValueObject(TType value) : base(value)
        {
        }

        protected override void Validate(TType value)
        {
            if (!Enum.IsDefined(typeof(TType), value))
                throw new ArgumentException($"Value {value} is out of range");
        }
    }
}