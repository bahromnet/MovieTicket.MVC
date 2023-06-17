using MediatR;

namespace Application.MVC.UseCases.Movies.Commands;

public record DeleteMovieCommand(int Id) : IRequest;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    public Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
