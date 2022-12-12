using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Base.Exceptions;
using System.Text.RegularExpressions;

namespace EventManager.Core.Domain.ValueObjects
{
    public class Email : BaseValueObject<Email>
    {
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("Please Enter The Email");
            }

            if (!Regex.Match(value, @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$").Success)

            {
                throw new InvalidValueObjectStateException("Email Is Not Valid");
            }

            Value = value;
        }

        public string Value { get; }

        public override bool ObjectIsEqual(Email otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }

        public static Email CreateIfNotEmpty(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return new Email(email);
        }
    }

}
