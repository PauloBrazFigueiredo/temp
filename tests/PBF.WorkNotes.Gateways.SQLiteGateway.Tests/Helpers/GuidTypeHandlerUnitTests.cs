namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Helpers;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Gateways")]
public class GuidTypeHandlerUnitTests
{
    [Fact]
    public void GuidTypeHandler_Parse_ShouldReturnGuid()
    {
        // Arrange
        var sut = new GuidTypeHandler();
        var id = Guid.NewGuid().ToString();

        // Act
        var result = sut.Parse(id);

        // Assert
        result.ToString().Should().Be(id);
        result.GetType().Should().Be(typeof(Guid));
    }
    
    [Fact]
    public void GuidTypeHandler_SetValue_ShouldSetGuidValue()
    {
        // Arrange
        var sut = new GuidTypeHandler();
        var id = Guid.NewGuid();
        IDbDataParameter parameter = new SqlParameter
        {
            ParameterName = "@Id",
            DbType = DbType.Guid
        };

        // Act
        sut.SetValue(parameter, id);

        // Assert
        parameter.Value.Should().Be(id.ToString());
        parameter.DbType.Should().Be(DbType.Guid);
    }
}
