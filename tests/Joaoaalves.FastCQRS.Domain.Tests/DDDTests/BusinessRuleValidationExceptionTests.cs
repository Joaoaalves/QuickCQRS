using Joaoaalves.FastCQRS.Domain.DDD;
using Joaoaalves.FastCQRS.Domain.Tests.Fakes;

namespace Joaoaalves.FastCQRS.Domain.Tests.DDDTests
{
    public class BusinessRuleValidationExceptionTests
    {

        [Fact]
        public void Constructor_ShouldSetBrokenRule_AndDetails_AndMessage()
        {
            // Arrange
            var rule = new FakeBrokenRule();

            // Act
            var exception = new BusinessRuleValidationException(rule);

            // Assert
            Assert.Equal(rule, exception.BrokenRule);
            Assert.Equal(rule.Message, exception.Details);
            Assert.Equal(rule.Message, exception.Message);
        }

        [Fact]
        public void ToString_ShouldReturnTypeAndMessage()
        {
            // Arrange
            var rule = new FakeBrokenRule();
            var exception = new BusinessRuleValidationException(rule);

            // Act
            var result = exception.ToString();

            // Assert
            Assert.Contains(rule.GetType().FullName!, result);
            Assert.Contains(rule.Message, result);
        }
    }
}