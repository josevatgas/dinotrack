using Microsoft.AspNetCore.Mvc;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.Entities;
using Dinotrack.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : GenericController<State>
    {
        private readonly DataContext _context;

        public StatesController(IGenericUnitOfWork<State> unitOfWork, DataContext context) : base(unitOfWork)
        {
            _context = context;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.States
                .Include(s => s.Cities)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var state = await _context.States
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
