using Dinotrack.Backend.Data;
using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class RefsController : GenericController<Ref>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public RefsController(IGenericUnitOfWork<Ref> unitOfWork, DataContext context, IFileStorage fileStorage) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [HttpGet]
         public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Refs
                .Include(r => r.RefImages)
                .Where(r => r.Brand!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(r => r.Name)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Refs
                .Where(r => r.Brand!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var refe = await _context.Refs
                .Include(x => x.RefImages)
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (refe == null)
            {
                return NotFound();
            }

            return Ok(refe);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(RefDTO refDTO)
        {   
            try
            {
                Ref newRef = new()
                {
                    Name = refDTO.Name,
                    Description = refDTO.Description,
                    Model = refDTO.Model,
                    BrandId = refDTO.BrandId,
                    RefImages = new List<RefImage>()
                };

                foreach (var refImage in refDTO.RefImages!)
                {
                    var photoRef = Convert.FromBase64String(refImage);
                    newRef.RefImages!.Add(new RefImage { Image = await _fileStorage.SaveFileAsync(photoRef, ".jpg", "motorcycles") });
                }

                _context.Add(newRef);
                await _context.SaveChangesAsync();
                return Ok(refDTO);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una referencia con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutFullAsync(RefDTO refDTO)
        {
            try
            {
                var refe = await _context.Refs.FirstOrDefaultAsync(x => x.Id == refDTO.Id);
                if (refe == null)
                {
                    return NotFound();
                }
                               
                refe.Name = refDTO.Name;    
                refe.Description = refDTO.Description;  
                refe.Model = refDTO.Model;
                refe.RefImages = new List<RefImage>();

                var httpClient = new HttpClient();
                foreach (var refImage in refDTO.RefImages!)
                {
                    if (!string.IsNullOrEmpty(refImage))
                    {
                        byte[] photoRef;
                        if (refImage.StartsWith("https://"))
                        {
                            photoRef = await httpClient.GetByteArrayAsync(refImage);
                        }
                        else
                        {
                            photoRef = Convert.FromBase64String(refImage);
                        }
                        refe.RefImages!.Add(new RefImage { Image = await _fileStorage.SaveFileAsync(photoRef, ".jpg", "motorcycles") });    
                    }
                }

                var result = _context.Update(refe);
                await _context.SaveChangesAsync();
                return Ok(refDTO);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una referencia con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("addImages")]
        public async Task<ActionResult> PostAddImagesAsync(ImageDTO imageDTO)
        {
            var refe = await _context.Refs
                .Include(x => x.RefImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.RefId);
            if (refe == null)
            {
                return NotFound();
            }

            if (refe.RefImages is null)
            {
                refe.RefImages = new List<RefImage>();
            }

            for (int i = 0; i < imageDTO.Images.Count; i++)
            {
                if (!imageDTO.Images[i].StartsWith("https://"))
                {
                    var photoRef = Convert.FromBase64String(imageDTO.Images[i]);
                    imageDTO.Images[i] = await _fileStorage.SaveFileAsync(photoRef, ".jpg", "motorcycles");
                    refe.RefImages!.Add(new RefImage { Image = imageDTO.Images[i] });
                }
            }

            _context.Update(refe);
            await _context.SaveChangesAsync();
            return Ok(imageDTO);
        }

        [HttpPost("removeImage")]
        public async Task<ActionResult> PostRemoveLastImageAsync(ImageDTO imageDTO)
        {
            var refe = await _context.Refs
                .Include(x => x.RefImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.RefId);
            if (refe == null)
            {
                return NotFound();
            }

            if (refe.RefImages is null || refe.RefImages.Count == 0)
            {
                return Ok();
            }

            var lastImage = refe.RefImages.LastOrDefault();
            await _fileStorage.RemoveFileAsync(lastImage!.Image, "motorcycles");
            refe.RefImages.Remove(lastImage);
            _context.Update(refe);
            await _context.SaveChangesAsync();
            imageDTO.Images = refe.RefImages.Select(x => x.Image).ToList();
            return Ok(imageDTO);
        }

    }
}
