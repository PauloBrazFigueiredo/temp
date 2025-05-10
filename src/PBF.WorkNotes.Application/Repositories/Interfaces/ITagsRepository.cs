namespace PBF.WorkNotes.Application.Repositories.Interfaces;

public interface ITagsRepository
{
    Task<IEnumerable<Tag>> GetAll();
    Task<Tag> GetById(Guid id);
    Task<Guid> Create(Tag model);
    Task<bool> Update(Tag model);
    Task<bool> Delete(Guid id);
}
