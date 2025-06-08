namespace PBF.WorkNotes.Application.UseCases.Interfaces;

public interface IGetAllToDoStatesUseCase
{
    Task<IEnumerable<ToDoState>> Execute();
}
