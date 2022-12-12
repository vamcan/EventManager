using EventManager.Core.Domain.Base.Exceptions;
using EventManager.Core.Domain.ValueObjects;
using Xunit;

namespace EventManager.UnitTests.ValueObjects
{
    public class PhoneNumberTests
    {
        [Fact]
        public void PhoneNumber_Should_Not_Allow_Empty_String()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new PhoneNumber(""));
        }

        [Fact]
        public void PhoneNumber_Should_Not_Allow_Null_String()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new PhoneNumber(null));
        }

        [Fact]
        public void PhoneNumber_Should_Not_Allow_Whitespace_String()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new PhoneNumber(" "));
        }

        [Fact]
        public void PhoneNumber_Should_Not_Allow_Invalid_Phone_Number()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new PhoneNumber("invalid phone number"));
        }

        [Fact]
        public void PhoneNumber_Should_Allow_Valid_Phone_Number()
        {
            var phoneNumber = new PhoneNumber("+1234567890");
            Assert.NotNull(phoneNumber);
        }

        [Fact]
        public void Check_Equality_Two_PhoneNumbers()
        {
            var phoneNumber1 = new PhoneNumber("+1234567890");
            var phoneNumber2 = new PhoneNumber("+1234567890");
            Assert.Equal(phoneNumber1, phoneNumber2);
        }
    }

}
