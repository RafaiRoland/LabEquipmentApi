using FluentValidation;
using LabEquipmentApi.Contracts;
using LabEquipmentApi.Contracts.Equipment;
using LabEquipmentApi.Data;
using LabEquipmentApi.Domain;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabEquipmentApi.Controllers
{
    [Route("api/equipment")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly LabDbContext _context;
        private readonly IValidator<CreateEquipmentRequest> _createValidator;

        public EquipmentController(LabDbContext context, IValidator<CreateEquipmentRequest> createValidator)
        {
            _context = context;
            _createValidator = createValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipment()
        {
            return await _context.Equipment.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetEquipment(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return equipment;
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<EquipmentListItemResponse>>> GetAvailableEquipment()
        {
            var equipment = await _context.Equipment
                .Where(e => e.IsAvailable)
                .OrderBy(e => e.Name)
                .ProjectToType<EquipmentListItemResponse>()
                .ToListAsync();

            return Ok(equipment);
        }

        [HttpPost]
        public async Task<IActionResult> PostEquipment([FromBody] CreateEquipmentRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(ModelState);
            }

            var equipment = new Equipment
            {
                Name = request.Name,
                Room = request.Room,
                IsAvailable = true
            };

            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipment), new { id = equipment.Id }, equipment);
        }
    }
}