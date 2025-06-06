﻿namespace PBF.WorkNotes.Gateways.SQLiteGateway.Helpers;

public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override Guid Parse(object value)
    {
        return new Guid(value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value.ToString();
    }
}
