using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Providers.Commands.CreateProvider
{
    public class ProviderDto
    {
        public Guid UserId { get; set; }
        public string AliasName { get; set; }
        public string Skills { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public string DurationTime { get; set; }
        public string ServiceMode { get; set; }
        public string Phone { get; set; }
        public string Telegram { get; set; }
    }
}
