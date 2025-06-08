//TODO: To remove.
//namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Extensions;

//[ExcludeFromCodeCoverage]
//[Trait("Unit Tests", "Gateways")]
//public  class SQLiteGatewayAutoMapperExtensionsUnitTests
//{
//    [Fact]
//    public void SQLiteGatewayAutoMapperExtensions_AddSQLiteGatewayAutoMapper_ShouldAddAutomapperSingleton()
//    {
//        // Arrange
//        var sut = new ServiceCollection();

//        // Act
//        sut.AddSQLiteGatewayAutoMapper();

//        // Assert
//        sut.Count.Should().Be(1);
//        sut.Should().ContainSingle(service => 
//            service.ServiceType == typeof(IMapper) && 
//            service.Lifetime == ServiceLifetime.Singleton);
//    }
//}
