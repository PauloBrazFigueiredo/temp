﻿namespace PBF.WorkNotes.Gateways.SQLiteGateway.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ToDoState, ToDoStateModel>();
        CreateMap<ToDoStateModel, ToDoState>();
    }
}
