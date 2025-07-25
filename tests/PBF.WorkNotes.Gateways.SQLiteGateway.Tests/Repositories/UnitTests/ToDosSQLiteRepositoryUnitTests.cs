﻿using PBF.WorkNotes.Domain.Entities;

namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories.UnitTests;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Gateways")]
public class ToDosSQLiteRepositoryUnitTests : BaseSQLiteRepositoryUnitTests
{
    private readonly IMapper _mapper;
    private readonly IGuidProvider _guidProvider;

    public ToDosSQLiteRepositoryUnitTests()
    {
        _mapper = CreateMapper();
        _guidProvider = CreateGuidProvider();
    }

    [Fact]
    public void ToDosSQLiteRepository_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();

        // Act
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<ToDosSQLiteRepository>();
        sut.Should().BeAssignableTo<IToDosRepository>();
    }

    [Fact]
    public async Task ToDosSQLiteRepository_GetAll_ShouldReturnEntities()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.QueryAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<ToDoModel>());
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetAll();

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QueryAsync("""
            SELECT
                Id,
                Title,
                Notes,
                StateId,
                PriorityId,
                IsPrivate,
                "Order",
                WorkDate,
                DueDate,
                CreatedDate
            FROM ToDos
        """), Times.Once);
    }

    [Fact]
    public async Task ToDosSQLiteRepository_GetByIdValidEntity_ShouldReturnEntity()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(new ToDoModel());
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Title,
                Notes,
                StateId,
                PriorityId,
                IsPrivate,
                "Order",
                WorkDate,
                DueDate,
                CreatedDate
            FROM ToDos
            WHERE Id = @Id
        """, 
            It.Is<DynamicParameters>(p =>
                p.Get<string>("Id") ==id.ToString())),
            Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ToDosSQLiteRepository_GetByIdInvalidEntity_ShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync((ToDoModel?)null);
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Title,
                Notes,
                StateId,
                PriorityId,
                IsPrivate,
                "Order",
                WorkDate,
                DueDate,
                CreatedDate
            FROM ToDos
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<string>("Id") == id.ToString())),
            Times.Once);
        result.Should().BeNull();
    }

    [Fact]
    public async Task ToDosSQLiteRepository_CreateValidEntity_ShouldReturnGuid()
    {
        // Arrange
        var stateId = Guid.NewGuid();
        var priorityId = Guid.NewGuid();
        var entity = new ToDo
        { 
            Title = "testTitle",
            Notes = "testNotes",
            StateId = stateId,
            State = new ToDoState
            {
                Id = stateId,
                Name = "testState"
            },
            PriorityId = priorityId,
            Priority = new Priority
            {
                Id = priorityId,
                Name = "testPriority",
                Level = "P1",
                Color = "#FFFFFF",
                IsDefault = false
            },
            IsPrivate = false,
            Order = 1,
            WorkDate = new DateTime(2023, 10, 1),
            DueDate = new DateTime(2023, 10, 2)
        };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Create(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync(It.Is<string>(sql => sql.Contains("""
            INSERT INTO ToDos ("Id", "Title", "Notes", "StateId", "PriorityId", "IsPrivate", "Order", "WorkDate", "DueDate", "CreatedDate")
            VALUES (@Id, @Title, @Notes, @StateId, @PriorityId, @IsPrivate, @Order, @WorkDate, @DueDate, @CreatedDate)
        """)),
        It.Is<DynamicParameters>(p =>
                p.Get<string>("Title") == entity.Title
                && p.Get<string>("Notes") == entity.Notes
                && p.Get<string>("StateId") == entity.StateId.ToString()
                && p.Get<string>("PriorityId") == entity.PriorityId.ToString()
                && p.Get<bool>("IsPrivate") == entity.IsPrivate
                && p.Get<int>("Order") == entity.Order
                && p.Get<DateTime>("WorkDate") == entity.WorkDate
                && p.Get<DateTime>("DueDate") == entity.DueDate)),
            Times.Once);
    }

    [Fact]
    public async Task ToDosSQLiteRepository_UpdateValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var stateId = Guid.NewGuid();
        var priorityId = Guid.NewGuid();
        var entity = new ToDo
        {
            Id = id,
            Title = "testTitle",
            Notes = "testNotes",
            StateId = stateId,
            State = new ToDoState
            {
                Id = stateId,
                Name = "testState"
            },
            PriorityId = priorityId,
            Priority = new Priority
            {
                Id = priorityId,
                Name = "testPriority",
                Level = "P1",
                Color = "#FFFFFF",
                IsDefault = false
            },
            IsPrivate = false,
            Order = 1,
            WorkDate = new DateTime(2023, 10, 1),
            DueDate = new DateTime(2023, 10, 2),
            CreatedDate = new DateTime(2023, 10, 3)
        };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE ToDos
            SET Title = @Title,
                Notes = @Notes,
                StateId = @StateId,
                PriorityId = @PriorityId,
                IsPrivate = @IsPrivate,
                "Order" = @Order,
                WorkDate = @WorkDate,
                DueDate = @DueDate,
                CreatedDate = @CreatedDate
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<string>("Id") == entity.Id.ToString()
                && p.Get<string>("Title") == entity.Title
                && p.Get<string>("Notes") == entity.Notes
                && p.Get<string>("StateId") == entity.StateId.ToString()
                && p.Get<string>("PriorityId") == entity.PriorityId.ToString()
                && p.Get<bool>("IsPrivate") == entity.IsPrivate
                && p.Get<int>("Order") == entity.Order
                && p.Get<DateTime>("WorkDate") == entity.WorkDate
                && p.Get<DateTime>("DueDate") == entity.DueDate
                && p.Get<DateTime>("CreatedDate") == entity.CreatedDate)),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ToDosSQLiteRepository_UpdateInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var stateId = Guid.NewGuid();
        var priorityId = Guid.NewGuid();
        var entity = new ToDo
        {
            Id = id,
            Title = "testTitle",
            Notes = "testNotes",
            StateId = stateId,
            State = new ToDoState
            {
                Id = stateId,
                Name = "testState"
            },
            PriorityId = priorityId,
            Priority = new Priority
            {
                Id = priorityId,
                Name = "testPriority",
                Level = "P1",
                Color = "#FFFFFF",
                IsDefault = false
            },
            IsPrivate = false,
            Order = 1,
            WorkDate = new DateTime(2023, 10, 1),
            DueDate = new DateTime(2023, 10, 2),
            CreatedDate = new DateTime(2023, 10, 3)
        };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE ToDos
            SET Title = @Title,
                Notes = @Notes,
                StateId = @StateId,
                PriorityId = @PriorityId,
                IsPrivate = @IsPrivate,
                "Order" = @Order,
                WorkDate = @WorkDate,
                DueDate = @DueDate,
                CreatedDate = @CreatedDate
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<string>("Id") == entity.Id.ToString()
                && p.Get<string>("Title") == entity.Title
                && p.Get<string>("Notes") == entity.Notes
                && p.Get<string>("StateId") == entity.StateId.ToString()
                && p.Get<string>("PriorityId") == entity.PriorityId.ToString()
                && p.Get<bool>("IsPrivate") == entity.IsPrivate
                && p.Get<int>("Order") == entity.Order
                && p.Get<DateTime>("WorkDate") == entity.WorkDate
                && p.Get<DateTime>("DueDate") == entity.DueDate
                && p.Get<DateTime>("CreatedDate") == entity.CreatedDate)),
            Times.Once);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ToDosSQLiteRepository_DeleteValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM ToDos
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<string>("Id") == id.ToString())),
        Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ToDosSQLiteRepository_DeleteInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new ToDosSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM ToDos
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<string>("Id") == id.ToString())),
        Times.Once);
        result.Should().BeFalse();
    }
}
