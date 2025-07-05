namespace PBF.WorkNotes.Application.UseCases.Interfaces;

public interface IGetToDoUseCase
{
    Task<ToDo> Execute(Guid id);
    Task<IEnumerable<ToDo>> Execute();
}
