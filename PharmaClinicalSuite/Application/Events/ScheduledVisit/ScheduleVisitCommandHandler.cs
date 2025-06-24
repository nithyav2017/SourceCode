using MediatR;
using Microsoft.EntityFrameworkCore;
using PharmaClinicalSuite.Application.Events.ScheduledVisit;
using PharmaClinicalSuite.Data;
using PharmaClinicalSuite.Domain.Models;
using PharmaClinicalSuite.Models.Events;
using PharmaClinicalSuite.Models.Interfaces;
using PharmaClinicalSuite.Utility;

namespace PharmaClinicalSuite.Application.Events.ScheduledVisit
{
    public class ScheduleVisitCommandHandler:IRequestHandler<SchduleVisitCommand, Result>
    {
        private readonly IRepository<Participants> repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PharmaClinicalSuiteContext _dbcontext;
        private readonly IMediator _mediator;

        public ScheduleVisitCommandHandler(IUnitOfWork uow,IMediator mediator, IRepository<Participants> repository, PharmaClinicalSuiteContext context)
        {
            _unitOfWork = uow;
            this.repository = repository;
            this._dbcontext = context;
            this._mediator = mediator;

        }

        public async Task<Result> Handle(SchduleVisitCommand request, CancellationToken cancellationToken)
        {
            var participant = await _dbcontext.Participants
                                    .Include(p => p.Visits)
                                    .FirstOrDefaultAsync(p => p.ParticipantId == request.ParticipantId);

            if (participant == null)
            {
                return Result.Failure("Participant Not Found");
            }

            var visit = new Visit
            {
                ParticipantId = request.ParticipantId,
                ScheduledDate = request.SchduleDate,
                VisitType=request.VisitType,
                Notes=request.Notes,
                Status = "Scheduled"
            };

            _dbcontext.Visit.Add(visit);

            var result = participant.ScheduleVisit(request.ParticipantId, request.SchduleDate, request.VisitType,request.Notes, "Scheduled");

            if (result.IsFailure)
                return Result.Failure(result.Error);

            await _dbcontext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new VisitScheduledEvent(request.ParticipantId, visit.Id,visit.ScheduledDate,visit.Notes, visit.Status), cancellationToken);
            return Result.Success();

        }
    }
}

