namespace PBF.WorkNotes.Application.UseCases.Interfaces;

public interface IGetToDoStateUseCase
{
    Task<ToDoState> Execute(Guid id);
}
