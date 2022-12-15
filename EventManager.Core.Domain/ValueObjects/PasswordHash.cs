using EventManager.Core.Domain.Base;
using System.Security.Cryptography;

namespace EventManager.Core.Domain.ValueObjects
{
    public class PasswordHash : BaseValueObject<PasswordHash>
    {
        const int SaltSize = 16, HashSize = 20, HashIter = 10000;
        readonly byte[] _salt, _hash;
        public string Value { get; }
        private PasswordHash(string password)
        {
            new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
            _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
            Value = Convert.ToBase64String(_hash);
        }

        public bool Verify(string password)
        {
            byte[] test = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
            for (int i = 0; i < HashSize; i++)
                if (test[i] != _hash[i])
                    return false;
            return true;
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
