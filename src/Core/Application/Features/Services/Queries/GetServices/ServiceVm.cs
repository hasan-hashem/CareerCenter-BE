using Application.Common.Mapping;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Services.Queries.GetServices
{
    public class ServiceVm : IMapFrom<Service>
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
