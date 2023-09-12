using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : GenericController<Brand>
    {
        public BrandsController(IGenericUnitOfWork<Brand> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
