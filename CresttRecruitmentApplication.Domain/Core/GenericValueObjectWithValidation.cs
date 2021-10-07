namespace CresttRecruitmentApplication.Domain.Core
{
    public abstract class GenericValueObjectWithValidation<TType> : BaseGenericValueObject<TType>
    {
        public GenericValueObjectWithValidation(TType value)
        {
            Validate(value);

            Value = value;
        }

        protected abstract void Validate(TType value);
    }
}