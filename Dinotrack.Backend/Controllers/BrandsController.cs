using Dinotrack.Backend.Data;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : GenericController<Brand>
    {
        private readonly DataContext _context;

        public BrandsController(IGenericUnitOfWork<Brand> unitOfWork, DataContext context) : base(unitOfWork)
        {
           _context = context;
        }

        [HttpGet]

        public override async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Brands
               .Include(c => c.Refs)
               .ToListAsync());
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var brands = await _context.Brands
                .Include(b => b.Refs!)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (brands == null)
            {
                return NotFound();
            }
            return Ok(brands);
        }
    }
}
