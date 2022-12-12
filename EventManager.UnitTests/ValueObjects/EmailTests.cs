using EventManager.Core.Domain.Base.Exceptions;
using EventManager.Core.Domain.ValueObjects;
using Xunit;

namespace EventManager.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Email_Should_Not_Allow_Empty_String()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new Email(""));
        }

        [Fact]
        public void Email_Should_Not_Allow_Null_String()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new Email(null));
        }

        [Fact]
        public void Email_Should_Not_Allow_Whitespace_String()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new Email(" "));
        }

        [Fact]
        public void Email_Should_Not_Allow_Invalid_Email_Address()
        {
            Assert.Throws<InvalidValueObjectStateException>(() => new Email("invalid email address"));
        }

        [Fact]
        public void Email_Should_Allow_Valid_Email_Address()
        {
            var email = new Email("valid@email.com");
            Assert.NotNull(email);
        }

        [Fact]
        public void Check_Equality_Two_Emails()
        {
            var email1 = new Email("valid@email.com");
            var email2 = new Email("valid@email.com");
            Assert.Equal(email1, email2);
        }
    }

}
