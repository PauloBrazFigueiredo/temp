namespace PBF.WorkNotes.Application.UseCases.Interfaces;

public interface IGetPriorityUseCase
{
    Task<Priority> Execute(Guid id);
}
