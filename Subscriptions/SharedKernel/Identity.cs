using System;

namespace Subscriptions.SharedKernel
{
    public abstract class Identity: IEquatable<Identity>
    {
        public Identity()
        {
            Value = Guid.NewGuid();
        }
        public Identity(Guid value)
        {
            this.Value = value;
        }
        
        public Guid Value { get; private set; }

        public bool Equals(Identity id)
        {
            if (object.ReferenceEquals(this, id)) return true;
            if (object.ReferenceEquals(null, id)) return false;
            return this.Value.Equals(id.Value);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return (this.GetType().GetHashCode() * 907) + this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.GetType().Name + " [Id=" + Value + "]";
        }
    }
}