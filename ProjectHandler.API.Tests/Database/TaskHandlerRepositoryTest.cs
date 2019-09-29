using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHandler.API.DatabaseLayer;
using ProjectHandler.API.Models;
using ProjectHandler.API.Tests.Database.Helper;
using Xunit;

namespace ProjectHandler.API.Tests.Database
{
    public class TaskHandlerRepositoryTest : IDisposable
    {
        IQueryable<User> userList = null;
        IQueryable<TaskItem> taskDetailsList = null;
        Mock<TaskHandlerDbContext> mockContext = null;

        public TaskHandlerRepositoryTest()
        {
        }

        [Fact]
        public async Task TestGetAll_ReturnsTwoTaskDetails()
        {
            SetUpMockData();

            var taskRepository = new TaskHandlerRepository(mockContext.Object);

            var taskDetails = await taskRepository.GetAllAsync();

            Assert.Equal(2, taskDetails.Count());
        }

        [Fact]
        public async Task TestGet_VerifyTaskName()
        {
            SetUpMockData();

            var taskRepository = new TaskHandlerRepository(mockContext.Object);

            var taskDetails = await taskRepository.GetAsync(2);

            Assert.IsType<TaskItem>(taskDetails);
        }

        [Fact]
        public async Task TestCreateAsync_VerifySaveChangesCalledOnce()
        {
            SetUpMockData();

            var taskRepository = new TaskHandlerRepository(mockContext.Object);
            var taskDetail = new TaskItem() { Id = 1, Name = "Task 1 ", Priority = 10 };
            var mockSet = new Mock<DbSet<TaskItem>>();

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);
            var result = await taskRepository.CreateAsync(taskDetail);

            mockSet.Verify(m => m.Add(taskDetail), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task TestUpdateAsync_VerifySaveChangesCalledOnce()
        {
            var contextOptions = new DbContextOptions<TaskHandlerDbContext>();
            var mockContext = new Mock<TaskHandlerDbContext>(contextOptions);

            var taskRepository = new TaskHandlerRepository(mockContext.Object);

            var taskDetail = new TaskItem() { Id = 1, Name = "Task 1 ", Priority = 10 };

            var mockSet = new Mock<DbSet<TaskItem>>();

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);
            await taskRepository.UpdateAsync(1, taskDetail);

            mockSet.Verify(m => m.Update(taskDetail), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

        private void SetUpMockData()
        {
            var contextOptions = new DbContextOptions<TaskHandlerDbContext>();
            mockContext = new Mock<TaskHandlerDbContext>(contextOptions);
            userList = new List<User>().AsQueryable();

            taskDetailsList = new List<TaskItem>()
                {
                    new TaskItem() {Id = 1, Name ="Task 1 ", Priority = 10, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                    new TaskItem() {Id = 2, Name ="Task 2 ", Priority = 20, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) }
                }.AsQueryable();

            var mockSet = new Mock<DbSet<TaskItem>>();
            var mockUserSet = new Mock<DbSet<User>>();

            mockSet.As<IAsyncEnumerable<TaskItem>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<TaskItem>(taskDetailsList.GetEnumerator()));

            mockSet.As<IQueryable<TaskItem>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<TaskItem>(taskDetailsList.Provider));

            mockSet.As<IQueryable<TaskItem>>().Setup(m => m.Expression).Returns(taskDetailsList.Expression);
            mockSet.As<IQueryable<TaskItem>>().Setup(m => m.ElementType).Returns(taskDetailsList.ElementType);
            mockSet.As<IQueryable<TaskItem>>().Setup(m => m.GetEnumerator()).Returns(() => taskDetailsList.GetEnumerator());

            mockUserSet.As<IAsyncEnumerable<User>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<User>(userList.GetEnumerator()));

            mockUserSet.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<User>(userList.Provider));

            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.GetEnumerator());

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);
            mockContext.Setup(m => m.User).Returns(mockUserSet.Object);
        }

        public void Dispose()
        {
        }
    }
}
