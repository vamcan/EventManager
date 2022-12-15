using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Base.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace EventManager.Core.Domain.ValueObjects
{
    public class PasswordHash : BaseValueObject<PasswordHash>
    {

        public string Value { get; }
        public PasswordHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("Please Enter The Password");
            }

            Value = HashPassword(value);
        }

        public string HashPassword(string password)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";
            byte[] bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(salt + password));
            string computedHash = BitConverter.ToString(bytes);
            return computedHash.Replace("-", "");
        }


        public override bool ObjectIsEqual(PasswordHash otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }
        public static PasswordHash CreateIfNotEmpty(string password)
        {
            return new PasswordHash(password);
        }
    }

}
