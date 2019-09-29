using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using ProjectHandler.API.DatabaseLayer;
using ProjectHandler.API.Models;
using Xunit;

namespace ProjectHandler.API.Tests.Database
{
    public class TaskHandlerDbContextTest : IDisposable
    {
        public void Dispose()
        {
        }

        [Fact]
        public void OnModelCreating_VerifyModelCreation()
        {
            var mockModel = new Mock<ModelBuilder>(new ConventionSet());
            try
            {
                var contextOptions = new DbContextOptions<TaskHandlerDbContext>();
                var taskModel = new TaskItem();
                var taskDbContextStub = new TaskDbContextStub(contextOptions);

                var modelBuilder = new ModelBuilder(new ConventionSet());
                var model = new Model();
                var configSource = new ConfigurationSource();
                var entity = new EntityType("TaskModel", model, configSource);
                var internalModelBuilder = new InternalModelBuilder(model);
                var internalEntityTypeBuilder = new InternalEntityTypeBuilder(entity, internalModelBuilder);
                var entityTypeBuilder = new EntityTypeBuilder<TaskItem>(internalEntityTypeBuilder);
                mockModel.Setup(m => m.Entity<TaskItem>()).Returns(entityTypeBuilder);

                var property = new Property("Name", taskModel.GetType(), taskModel.GetType().GetProperty("Name"), taskModel.GetType().GetField("Name"), entity, configSource, null);
                var internalPropertyBuilder = new InternalPropertyBuilder(property, internalModelBuilder);
                var propertyBuilder = new PropertyBuilder<string>(internalPropertyBuilder);

                taskDbContextStub.TestModelCreation(modelBuilder);
            }
            catch (Exception ex)
            {
                mockModel.Verify(m => m.Entity<TaskItem>().HasKey("Id"), Times.Once);
                Assert.NotNull(ex);
            }
        }
    }

    public class TaskDbContextStub : TaskHandlerDbContext
    {
        public TaskDbContextStub(DbContextOptions options) : base(options)
        {

        }
        public void TestModelCreation(ModelBuilder model)
        {
            OnModelCreating(model);
        }
    }
}
