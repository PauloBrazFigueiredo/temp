namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Attributes;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class FactOrderAttribute : Attribute
{
    public int Order { get; private set; }

    public FactOrderAttribute(int order)
    {
        Order = order;
    }
}