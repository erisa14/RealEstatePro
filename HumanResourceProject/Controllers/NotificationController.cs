using DAL.Contracts;
using Domain.Contracts;
using Domain.UoW;
using DTO.NotificationDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IDomainUnitOfWork _domainUnitOfWork;
        public NotificationController(IDomainUnitOfWork domainUnitOfWork)
        {
            _domainUnitOfWork = domainUnitOfWork;
        }
        private INotificationDomain _notificationDomain =>_domainUnitOfWork.GetDomain<INotificationDomain>();
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var notifications = _notificationDomain.GetAll();
            return Ok(notifications);
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var notification = _notificationDomain.Get(id);
            if (notification == null)
                return NotFound();
            return Ok(notification);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(NotificationCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Guid entityId;
            try
            {
                entityId = _notificationDomain.CreateNotification(dto);
            }
            catch
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get) , new { id = entityId }, _notificationDomain.Get(entityId));
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _notificationDomain.DeleteNotification(id);
            return Ok();
        }
        [HttpGet]
        [Route("getNotificationsBySender/{id}")]
        public async Task<IActionResult> GetNotificationsBySender(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var model = _notificationDomain.GetNotificationsBySender(id);
            if (model.Count == 0)
                return NotFound();
            return Ok(model);
        }
        [HttpGet]
        [Route("getNotificationsByReceiver/{id}")]
        public async Task<IActionResult> GetNotificationsByReceiver(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var model = _notificationDomain.GetNotificationsByReceiver(id);
            if (model.Count == 0)
                return NotFound();
            return Ok(model);
        }
    }
}
