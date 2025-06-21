namespace PBF.WorkNotes.Application.UseCases.Interfaces;

public interface IUpdateToDoUseCase
{
    Task<bool> Execute(ToDo entity);
}
