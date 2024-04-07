using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.NotificationDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class NotificationDomain : DomainBase, INotificationDomain
    {
        public NotificationDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private INotificiationRepository _notificationRepository => _unitOfWork.GetRepository<INotificiationRepository>();
        public List<NotificationReadDTO> GetAll()
        {
            var notificationEntities = _notificationRepository.GetAll();
            var dtos = _mapper.Map<List<NotificationReadDTO>>(notificationEntities);
            return dtos;
        }
        public NotificationReadDTO Get(Guid id)
        {
            var entity = _notificationRepository.GetById(id);
            var dto = _mapper.Map<NotificationReadDTO>(entity);
            return dto;
        }
        public Guid CreateNotification(NotificationCreateDTO dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            entity.NotificationId = Guid.NewGuid();
            entity.Date = DateTime.Now;
            entity.UserId = Guid.Parse("1C97AB14-A909-4C15-AA85-EC09720864E8");
            _notificationRepository.Add(entity);
            _unitOfWork.Save();
            SendEmail("hhatija@gmail.com", dto.Message);
            return entity.NotificationId;
        }
        public void DeleteNotification(Guid id)
        {

            _notificationRepository.Remove(id);
            _unitOfWork.Save();
        }
        public List<NotificationReadDTO> GetNotificationsBySender(Guid sender)
        {
            var notificationEntities = _notificationRepository.Find(x => x.UserIdSender.Equals(sender));
            return _mapper.Map<List<NotificationReadDTO>>(notificationEntities);
        }
        public List<NotificationReadDTO> GetNotificationsByReceiver(Guid receiver)
        {
            var notificationEntities = _notificationRepository.Find(x => x.UserIdReceiver.Equals(receiver));
            return _mapper.Map<List<NotificationReadDTO>>(notificationEntities);
        }
        private void SendEmail(string receiver, string emailMessage)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("notificationsrep@gmail.com");
                message.To.Add(new MailAddress(receiver));
                message.Subject = "Test";
                message.Body = emailMessage;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("notificationsrep@gmail.com", "thve sbqw lgrs mklk");
                //TestPassword12#$4
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
        
}
}
