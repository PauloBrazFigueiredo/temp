namespace PBF.WorkNotes.Application.UseCases.Interfaces;

public interface ICreateToDoUseCase
{
    Task<Guid> Execute(ToDo entity);
}
