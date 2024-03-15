using AutoMapper;
using Domain.Entities;
using FirebaseAdmin.Messaging;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public  class NotificationProfile : Profile
    {
        public NotificationProfile() {

            CreateMap<Domain.Entities.Notification, NotificationDocument>().ReverseMap();

        }
    }
}
