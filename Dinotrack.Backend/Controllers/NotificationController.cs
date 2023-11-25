using Dinotrack.Backend.Data;
using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Interfaces;
using Dinotrack.Shared.DTOs;
using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dinotrack.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class NotificationsController : GenericController<Notification>
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IMailHelper _mailHelper;

        public NotificationsController(IUserHelper userHelper, IGenericUnitOfWork<Notification> unitOfWork, DataContext context, IMailHelper mailHelper) : base(unitOfWork, context)
        {
            _userHelper = userHelper;
            _context = context;
            _mailHelper = mailHelper;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }
            var queryable = _context.Notifications.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(x => x.UserId == user.Id);
            }


            return Ok(await queryable
                .OrderBy(x => x.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Notifications.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [AllowAnonymous]
        [HttpGet("count")]
        public async Task<ActionResult> GetCountAsync()
        {
            
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
                if (user == null)
                {
                    return BadRequest("User not valid.");
                }
                var queryable = _context.Notifications.AsQueryable()
                    .Where(x => x.UserId == user.Id);
                int count = await queryable.CountAsync();

                return Ok(count);
            
        }

        [HttpPost("sendNotification")]
        public async Task<ActionResult> SendNotification(Notification notification)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == notification.UserId);

            if (user == null)
            {
                return NotFound("Not found");
            }

            var response = _mailHelper.SendMail(user.FullName, user.Email!,
                $"Notificación Dinotrack",
                $"<h1>Dinotrack - Notificación Programada</h1>" +
                $"<p>Esta es tu notificación programada:{notification.Description}: {notification.Remarks} </p>" +
                $"<b>Dinotrack informativo</b>");

            if (response.WasSuccess)
            {
                notification.NotificationState = NotificationStateEnum.Enviada;
                _context.Update(notification);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(response.Message);
        }
    }
}
