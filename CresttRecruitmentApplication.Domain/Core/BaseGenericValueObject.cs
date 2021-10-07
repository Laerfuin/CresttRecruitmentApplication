namespace CresttRecruitmentApplication.Domain.Core
{
    public abstract class BaseGenericValueObject<TType>
    {
        public TType Value { get; protected set; }

        public override bool Equals(object obj) => Value.GetHashCode() == obj.GetHashCode();

        public override int GetHashCode() => Value.GetHashCode();
    }
}