using Microsoft.AspNetCore.Mvc;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.Entities;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : GenericController<State>
    {
        public StatesController(IGenericUnitOfWork<State> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
