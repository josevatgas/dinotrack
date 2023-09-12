using Dinotrack.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferencesController : GenericController<Reference>
    {
        public ReferencesController(IGenericUnitOfWork<Reference> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
