using DocPlanner.Availability.API.Dto;
using DocPlanner.Availability.Domain.Contract;
using DocPlanner.Availability.Domain.Model;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocPlanner.Availability.API.Controllers
{
    /// <summary>
    /// Availability controller for API calls.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly ILogger<AvailabilityController> _logger;
        private readonly IAvailabilityService _availabilityService;
        private readonly IValidator<TakeSlotRequestDto> _takeSlotRequestValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="availabilityService">The availability service.</param>
        public AvailabilityController(ILogger<AvailabilityController> logger, IAvailabilityService availabilityService, IValidator<TakeSlotRequestDto> takeSlotRequestValidator)
        {
            _logger = logger;
            _availabilityService = availabilityService;
            _takeSlotRequestValidator = takeSlotRequestValidator;
        }

        [HttpGet]
        [Route("WeekSlots/{date}")]
        public async Task<IActionResult> GetWeekSlots([FromRoute] DateTime date, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received call to {apiMethod} with parameters {date}.", "GetWeekSlots", date);
            var availableSlots = await _availabilityService.GetWeekAvailableSlots(date, cancellationToken);

            return Ok(WeekSlotsDto.Create(availableSlots));
        }

        [HttpPost]
        [Route("TakeSlot")]
        public async Task<IActionResult> TakeSlot([FromBody] TakeSlotRequestDto request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received call to {apiMethod} with parameters {@request}.", "TakeSlot", request);

            // We can create some middleware to intercept calls and make validation automatic reducing boiler plate
            var dtoValidator = await _takeSlotRequestValidator.ValidateAsync(request);
            if (!dtoValidator.IsValid)
            {
                AddToModelState(dtoValidator, ModelState);
                return BadRequest(ModelState);
            }
            var patient = new Patient(name: request.Name, secondName: request.SecondName, email: request.Email, phoneNumber: request.Phone);

            await _availabilityService.TakeSlot(request.SlotDate, request.Comments, patient, cancellationToken);

            return Ok();
        }

        private static void AddToModelState(ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}