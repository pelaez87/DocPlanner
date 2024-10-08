using DocPlanner.Availability.API.Dto;
using DocPlanner.Availability.API.Validation;

namespace DocPlanner.Availability.API.Tests.Validation
{
    public class TakeSlotRequestValidatorTests
    {
        private readonly TakeSlotRequestDto _validRequest;
        private readonly TakeSlotRequestDtoValidator _validator;

        public TakeSlotRequestValidatorTests()
        {
            _validator = new TakeSlotRequestDtoValidator();

            _validRequest = new TakeSlotRequestDto
            {
                SlotDate = DateTime.UtcNow,
                Comments = "Some comments from patient",
                Name = "David",
                SecondName = "Peláez",
                Email = "me@provider.com",
                Phone = "123-456-789"
            };
        }

        /*
         Extend tests with all validator cases to ensure coded rules are tested and will not change by mistake
         */

        [Fact]
        public async Task TakeSlotRequestDtoValidator_HappyPath()
        {
            // Act
            var validationResult = await _validator.ValidateAsync(_validRequest);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public async Task TakeSlotRequestDtoValidator_EmptyName_IsNotValid()
        {
            // Arrange
            var expectedErrorMessage = "'Name' must not be empty.";
            var invalidDeleteRequest = _validRequest with { Name = null };

            // Act
            var validationResult = await _validator.ValidateAsync(invalidDeleteRequest);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Equal(expectedErrorMessage, validationResult.Errors.First().ErrorMessage);
        }
    }
}
