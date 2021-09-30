namespace CresttRecruitmentApplication.Domain.Core
{
    public abstract class GenericValueObject<TType>
    {
        public GenericValueObject(TType value)
        {
            Validate(value);

            Value = value;
        }

        public TType Value { get; private set; }

        public override bool Equals(object obj) => Value.GetHashCode() == obj.GetHashCode();

        public override int GetHashCode() => Value.GetHashCode();

        protected abstract void Validate(TType value);
    }
}