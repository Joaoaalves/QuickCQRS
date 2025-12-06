using Joaoaalves.FastCQRS.Abstractions.Exceptions;

namespace Joaoaalves.FastCQRS.Core.Tests.Validations
{
    public class InvalidCommandExceptionTests
    {
        [Fact]
        public void Constructor_ShouldSetMessageAndDetails()
        {
            // Arrange
            var message = "Validation failed";
            var details = "Name is required";

            // Act
            var ex = new InvalidCommandException(message, details);

            // Assert
            Assert.Equal(message, ex.Message);
            Assert.Equal(details, ex.Details);
        }
    }
}
