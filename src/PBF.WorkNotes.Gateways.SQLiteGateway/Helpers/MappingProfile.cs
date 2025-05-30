namespace PBF.WorkNotes.Gateways.SQLiteGateway.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ToDoState, ToDoStateModel>();
        CreateMap<ToDoStateModel, ToDoState>();
        CreateMap<Tag, TagModel>();
        CreateMap<TagModel, Tag>();
        CreateMap<Priority, PriorityModel>();
        CreateMap<PriorityModel, Priority>();
        CreateMap<ToDo, ToDoModel>();
        CreateMap<ToDoModel, ToDo>();
    }
}
