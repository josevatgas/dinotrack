using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefsController : GenericController<Ref>
    {
        public RefsController(IGenericUnitOfWork<Ref> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
