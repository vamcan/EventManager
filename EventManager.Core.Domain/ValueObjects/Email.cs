using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Base.Exceptions;
using System.Text.RegularExpressions;

namespace EventManager.Core.Domain.ValueObjects
{
    public class Email : BaseValueObject<Email>
    {
        private Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("Please Enter The Email");
            }

            if (!IsValid(value))
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
        public static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value)
                   && Regex.IsMatch(value, @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$");
        }
        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }

        public static Email CreateIfNotEmpty(string email)
        {
          return new Email(email);
        }
    }

}
