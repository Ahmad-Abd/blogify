using Blogify.Common;
using Blogify.Common.Repositories;
using Blogify.EntityFrameworkCore.Test.Common.DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Test.Common;

public class EfCoreBasicRepositoryTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly EfCoreBasicRepository<TestEntity, string> _repository;
    private readonly TestDbContextBase _dbContext;

    // Followed unit test naming convention : UnitThatIsBeingTested_ScenarioUnderWhichUnitBeingTested_ExpectedBehaviorWhenScenarioInvoked
    public EfCoreBasicRepositoryTests()
    {
        _connection = new SqliteConnection("Data Source = :memory:");
        _connection.Open();
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContextBase>().UseSqlite(_connection);
        _dbContext = new TestDbContextBase(optionsBuilder.Options);
        _dbContext.Database.EnsureCreated();
        _dbContext.Database.Migrate();
        _repository = new EfCoreBasicRepository<TestEntity, string>(_dbContext);
    }

    [Fact]
    public async Task EfCoreBasicRepository_GetQueryable_QueryableShouldBeExists()
    {
        // Arrange
        // done in constructor

        // Act
        var queryable = await _repository.GetQueryableAsync();

        // Assert
        Assert.NotNull(queryable);
    }

    [Fact]
    public async Task EfCoreBasicRepository_GetEntityById_EntityShouldBeRetrieved()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
        };
        await _dbContext.Set<TestEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrieved = await _repository.GetAsync(id);

        // Assert
        Assert.NotNull(retrieved);
    }

    [Fact]
    public async Task EfCoreBasicRepository_GetEntityByPredicate_EntityShouldBeRetrieved()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
        };
        await _dbContext.Set<TestEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrieved = await _repository.GetAsync(e => e.Id == id);

        // Assert
        Assert.NotNull(retrieved);
    }

    [Fact]
    public async Task EfCoreBasicRepository_GetTotalCount_TotalCountShouldBeZero()
    {
        // Arrange
        // done in constructor

        // Act
        var totalCount = await _repository.GetCountAsync();

        // Assert
        Assert.Equal(0, totalCount);
    }

    [Fact]
    public async Task EfCoreBasicRepository_GetTotalCountByPredicate_TotalCountShouldBeOne()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
        };
        await _dbContext.Set<TestEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var totalCount = await _repository.GetCountAsync(e => e.Id == id);

        // Assert
        Assert.Equal(1, totalCount);
    }

    [Fact]
    public async Task EfCoreBasicRepository_GetEntitiesList_TotalCountShouldBeFive()
    {
        // Arrange
        var entities = new List<TestEntity>();
        for (var i = 0; i < 5; i++)
        {
            entities.Add(new TestEntity { Id = Guid.NewGuid().ToString() });
        }

        await _dbContext.Set<TestEntity>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrieved = await _repository.GetListAsync();

        // Assert
        Assert.Equal(5, retrieved.Count());
    }

    [Fact]
    public async Task EfCoreBasicRepository_GetEntitiesListByPredicate_TotalCountShouldBeFive()
    {
        // Arrange
        var entities = new List<TestEntity>();
        for (var i = 0; i < 5; i++)
        {
            entities.Add(new TestEntity { Id = Guid.NewGuid().ToString() });
        }

        var ids = entities.Select(e => e.Id);

        await _dbContext.Set<TestEntity>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrieved = await _repository.GetListAsync(e => ids.Contains(e.Id));

        // Assert
        Assert.Equal(5, retrieved.Count());
    }

    [Theory]
    [InlineData(3, 0, 3)]
    [InlineData(3, 3, 2)]
    public async Task EfCoreBasicRepository_GetEntitiesPage_TotalCountShouldBeAsExpected(int maxResultCount,
        int skipCount, int expectedResultCount)
    {
        // Arrange
        var entities = new List<TestEntity>();
        for (var i = 0; i < 5; i++)
        {
            entities.Add(new TestEntity { Id = Guid.NewGuid().ToString() });
        }

        var input = new GetPageRequestInput()
        {
            MaxResultCount = maxResultCount,
            SkipCount = skipCount
        };

        await _dbContext.Set<TestEntity>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrievedPage = await _repository.GetPageAsync(input);

        // Assert
        Assert.Equal(retrievedPage.Items.Count, expectedResultCount);
    }

    [Fact]
    public async Task EfCoreBasicRepository_InsertNewEntity_EntityShouldBeInserted()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
        };

        // Act
        await _repository.InsertAsync(entity);

        // Assert
        var retrieved = await _dbContext.Set<TestEntity>().FindAsync(id);
        Assert.NotNull(retrieved);
    }

    [Fact]
    public async Task EfCoreBasicRepository_InsertEntitiesList_EntitiesShouldBeInserted()
    {
        // Arrange
        var entities = new List<TestEntity>();
        for (var i = 0; i < 5; i++)
        {
            entities.Add(new TestEntity { Id = Guid.NewGuid().ToString() });
        }

        // Act
        await _repository.InsertManyAsync(entities);

        // Assert
        foreach (var testEntity in entities)
        {
            var retrieved = await _dbContext.Set<TestEntity>().FindAsync(testEntity.Id);
            Assert.NotNull(retrieved);
        }
    }

    [Fact]
    public async Task EfCoreBasicRepository_UpdateEntity_EntityShouldBeUpdated()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
        };
        await _dbContext.Set<TestEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        entity.Description = "Updated Description";
        var updatedEntity = await _repository.UpdateAsync(entity);

        // Assert
        Assert.Equal("Updated Description", updatedEntity.Description);

    }

    [Fact]
    public async Task EfCoreBasicRepository_UpdateEntitiesList_EntitiesShouldBeUpdated()
    {
        // Arrange
        var entities = new List<TestEntity>();
        for (var i = 0; i < 5; i++)
        {
            entities.Add(new TestEntity { Id = Guid.NewGuid().ToString() });
        }

        await _dbContext.Set<TestEntity>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();

        // Act
        foreach (var testEntity in entities)
        {
            testEntity.Description = "Updated Description";
        }

        await _repository.UpdateManyAsync(entities);

        // Assert
        foreach (var testEntity in entities)
        {
            var retrieved = await _dbContext.Set<TestEntity>().FindAsync(testEntity.Id);
            Assert.NotNull(retrieved);
            Assert.Equal("Updated Description", retrieved.Description);
        }
    }

    [Fact]
    public async Task EfCoreBasicRepository_DeleteEntity_EntityShouldBeDeleted()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
        };
        var createdEntity = (await _dbContext.Set<TestEntity>().AddAsync(entity)).Entity;
        await _dbContext.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(createdEntity);

        // Assert
        var retrieved = await _repository.GetAsync(id);
        Assert.Null(retrieved);

    }

    [Fact]
    public async Task EfCoreBasicRepository_DeleteEntitiesList_EntitiesShouldBeDeleted()
    {
        // Arrange
        var entities = new List<TestEntity>();
        for (var i = 0; i < 5; i++)
        {
            entities.Add(new TestEntity { Id = Guid.NewGuid().ToString() });
        }

        await _dbContext.Set<TestEntity>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();

        // Act
        await _repository.DeleteManyAsync(entities);

        // Assert
        foreach (var testEntity in entities)
        {
            var retrieved = await _dbContext.Set<TestEntity>().FindAsync(testEntity.Id);
            Assert.Null(retrieved);
        }

    }

    [Fact]
    public async Task EfCoreBasicRepository_InsertDuplicatedEntity_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
            Description = "Non-existent Entity"
        };
        await _dbContext.Set<TestEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var exception = await Record.ExceptionAsync(() => _repository.InsertAsync(entity));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<DbUpdateException>(exception);
    }

    [Fact]
    public async Task EfCoreBasicRepository_UpdateNonExistentEntity_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
            Description = "Non-existent Entity"
        };

        // Act
        var exception = await Record.ExceptionAsync(() => _repository.UpdateAsync(entity));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<DbUpdateConcurrencyException>(exception);
    }

    [Fact]
    public async Task EfCoreBasicRepository_DeleteNonExistentEntity_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var entity = new TestEntity
        {
            Id = id,
            Description = "Non-existent Entity"
        };

        // Act
        var exception = await Record.ExceptionAsync(() => _repository.DeleteAsync(entity));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<DbUpdateConcurrencyException>(exception);

    }

    public void Dispose()
    {
        _connection?.Dispose();
        _dbContext?.Dispose();
    }
}