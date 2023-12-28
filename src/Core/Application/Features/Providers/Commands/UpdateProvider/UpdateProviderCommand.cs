using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Providers.Commands.UpdateProvider
{
    public record UpdateProviderCommand(Guid id, ProviderForUpdate provider) : IRequest;

    public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand>
    {

        private readonly IApplicationDbContext _context;

        public UpdateProviderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Providers
            .FindAsync(new object[] { request.id }, cancellationToken);

            if (entity != null)
            {
                entity.Title = request.provider.Title;
                entity.Skills = request.provider.Skills;
                entity.Description = request.provider.Description;
                entity.DurationTime = request.provider.DurationTime;
                entity.ServiceMode = request.provider.ServiceMode;
                entity.Phone = request.provider.Phone;
                entity.Telegram = request.provider.Telegram;

                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
