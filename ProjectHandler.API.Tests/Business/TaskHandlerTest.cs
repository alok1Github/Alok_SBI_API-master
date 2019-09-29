using Microsoft.Extensions.Logging;
using Moq;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using ProjectHandler.API.DatabaseLayer;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.Tests.Business
{
    public class TaskHandlerTest : IDisposable
    {
        public ILogger<API.BusinessLayer.TaskHandler> Logger { get; private set; }

        public TaskHandlerTest()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            Logger = factory.CreateLogger<API.BusinessLayer.TaskHandler>();
        }

        [Fact]
        public async Task TestAddTaskAsync_VerifyCreateAsyncCalledOnce()
        {
            var mockRepository = new Mock<ITaskHandlerRepository>();
            var TaskHandler = new API.BusinessLayer.TaskHandler(mockRepository.Object, this.Logger);
            var taskDetail = new TaskItem();
            var result = await TaskHandler.AddTaskAsync(taskDetail);

            mockRepository.Verify(r => r.CreateAsync(taskDetail), Times.Once);
        }

        [Fact]
        public async Task TestEditTaskAsync_VerifyUpdateAsyncCalledOnce()
        {
            var mockRepository = new Mock<ITaskHandlerRepository>();
            var TaskHandler = new API.BusinessLayer.TaskHandler(mockRepository.Object, this.Logger);
            var taskDetail = new TaskItem();
            await TaskHandler.UpdateTaskAsync(10, taskDetail);

            mockRepository.Verify(r => r.UpdateAsync(10, taskDetail), Times.Once);
        }

        [Fact]
        public async Task TestViewTasksAsync_VerifyGetAllAsyncCalledOnce()
        {
            var mockRepository = new Mock<ITaskHandlerRepository>();
            var TaskHandler = new API.BusinessLayer.TaskHandler(mockRepository.Object, this.Logger);

            var result = await TaskHandler.GetAllTasksAsync();

            mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task TestGetTaskAsync_VerifyGetAsyncCalledOnce()
        {
            var mockRepository = new Mock<ITaskHandlerRepository>();
            var TaskHandler = new API.BusinessLayer.TaskHandler(mockRepository.Object, this.Logger);

            var result = await TaskHandler.GetTaskAsync(10);

            mockRepository.Verify(r => r.GetAsync(10), Times.Once);
        }

        [Fact]
        public void TestIsTaskValidToClose_ReturnFalseWhenTaskContainsChildTask()
        {
            var mockRepository = new Mock<ITaskHandlerRepository>();
            var TaskHandler = new API.BusinessLayer.TaskHandler(mockRepository.Object, this.Logger);
            var taskDetail = new TaskItem() { Id = 1, Name = "Task 1", Priority = 20 };

            var taskDetailsList = new List<TaskItem>()
            {
                taskDetail,
                new TaskItem() {Id = 2, Name ="Task 2 ", Priority = 20, ParentTaskId = 1},
            };

            mockRepository.Setup(r => r.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TaskItem>>(taskDetailsList));

            var result = TaskHandler.IsTaskItemValid(taskDetail);

            Assert.False(result);
        }

        [Fact]
        public void TestIsTaskValidToClose_ReturnTrueWhenTaskContainsChildTaskWhichIsNOtActive()
        {
            var mockRepository = new Mock<ITaskHandlerRepository>();
            var manageTask = new API.BusinessLayer.TaskHandler(mockRepository.Object, this.Logger);
            var taskDetail = new TaskItem() { Id = 1, Name = "Task 1", Priority = 20 };

            var taskDetailsList = new List<TaskItem>()
            {
                taskDetail,
                new TaskItem() {Id = 2, Name ="Task 2 ", Priority = 20, ParentTaskId = 1, EndTask = true},
            };

            mockRepository.Setup(r => r.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TaskItem>>(taskDetailsList));

            var result = manageTask.IsTaskItemValid(taskDetail);

            Assert.True(result);
        }

        [Fact]
        public void TestIsTaskValidToClose_ReturnTrueWhenTaskDoesNotContainsChildTas()
        {
            var mockRepository = new Mock<ITaskHandlerRepository>();
            var manageTask = new API.BusinessLayer.TaskHandler(mockRepository.Object, this.Logger);
            var taskDetail = new TaskItem() { Id = 1, Name = "Task 1", Priority = 20 };

            var taskDetailsList = new List<TaskItem>()
            {
                taskDetail,
                new TaskItem() {Id = 2, Name ="Task 2 ", Priority = 20},
            };

            mockRepository.Setup(r => r.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TaskItem>>(taskDetailsList));

            var result = manageTask.IsTaskItemValid(taskDetail);

            Assert.True(result);
        }

        public void Dispose()
        {
        }
    }
}
