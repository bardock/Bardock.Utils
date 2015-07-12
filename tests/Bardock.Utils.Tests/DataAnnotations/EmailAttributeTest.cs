using Bardock.Utils.DataAnnotations;
using Bardock.Utils.Validation;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Bardock.Utils.Tests.DataAnnotations
{
    public class EmailAttributeTest
    {
        [Fact]
        public void ShouldBeRegularExpressionAttribute()
        {
            //Exercise
            var sut = new EmailAttribute();

            //Verify
            sut.Should().BeAssignableTo<RegularExpressionAttribute>();
        }

        [Fact]
        public void ShouldUseEmailValidatorRegex()
        {
            //Exercise
            var sut = new EmailAttribute();

            //Verify
            sut.Pattern.Should().Be(EmailValidator.REGEX_PATTERN);
        }
    }
}