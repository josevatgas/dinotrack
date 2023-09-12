using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopsController : GenericController<Workshop>
    {
        public WorkshopsController(IGenericUnitOfWork<Workshop> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
