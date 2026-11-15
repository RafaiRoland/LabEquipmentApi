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

        public EquipmentController(LabDbContext context)
        {
            _context = context;
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
    }
}