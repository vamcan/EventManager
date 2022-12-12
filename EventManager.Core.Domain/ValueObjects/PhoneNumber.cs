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

            //if (!Regex.Match(value, @"^(\+[0-9]{9})$").Success)
            //{
            //    throw new InvalidValueObjectStateException("Phone Number Is not Valid");
            //}
          Value=value;
        }

        public string Value { get; }

        public override bool ObjectIsEqual(PhoneNumber otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }

        public static PhoneNumber CreateIfNotEmpty(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return null;
            }

            return new PhoneNumber(mobile);
        }
    }
}
