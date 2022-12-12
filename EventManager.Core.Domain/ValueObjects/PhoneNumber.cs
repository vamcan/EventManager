using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Base.Exceptions;
using System.Text.RegularExpressions;

namespace EventManager.Core.Domain.ValueObjects
{
    public class PhoneNumber : BaseValueObject<PhoneNumber>
    {
        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("Please Enter The Phone Number");
            }
            if (!IsValid(value))
            {
                throw new InvalidValueObjectStateException("Invalid phone number.", nameof(value));
            }
            
            Value =value;
        }

        public string Value { get; }

        public override bool ObjectIsEqual(PhoneNumber otherObject)
        {
            return Value == otherObject.Value;
        }
        public static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value)
                   && Regex.IsMatch(value, @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
        }
        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }

        public static PhoneNumber CreateIfNotEmpty(string mobile)
        {
        return new PhoneNumber(mobile);
        }
    }
}
