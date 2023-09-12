using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        public CountriesController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
