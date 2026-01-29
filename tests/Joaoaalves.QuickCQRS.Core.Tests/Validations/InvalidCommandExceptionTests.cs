using Joaoaalves.QuickCQRS.Abstractions.Exceptions;

namespace Joaoaalves.QuickCQRS.Core.Tests.Validations
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
