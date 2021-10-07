namespace CresttRecruitmentApplication.Domain.Core
{
    public abstract class GenericValueObject<TType> : BaseGenericValueObject<TType>
    {
        public GenericValueObject(TType value)
        {
            Value = value;
        }
    }
}