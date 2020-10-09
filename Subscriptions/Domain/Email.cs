using System;
using System.Collections.Generic;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class Email : ValueObject
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length > 320)
            {
                throw new ArgumentOutOfRangeException("value is too long");
            }
            //more validations
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}