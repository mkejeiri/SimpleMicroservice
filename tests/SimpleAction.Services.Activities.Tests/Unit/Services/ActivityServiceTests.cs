using System;
using System.Threading.Tasks;
using Moq;
using SimpleAction.Common.Commands;
using SimpleAction.Services.Activities.Domain.Models;
using SimpleAction.Services.Activities.Repositories;
using SimpleAction.Services.Activities.Services;
using Xunit;

namespace SimpleAction.Services.Activities.Tests.Unit.Services {
    public class ActivityServiceTests {
        [Fact]
        public async Task activity_service_add_async_should_succeed () {
            var category = "test";
            var activityRepositoryMock = new Mock<IActivityRepository> ();
            var categoryRepositoryMock = new Mock<ICategoryRepository> ();

            categoryRepositoryMock.Setup (x => x.GetAsync (category))
                .ReturnsAsync (new Category (category));
            var activityService = new ActivityService (activityRepositoryMock.Object,
                categoryRepositoryMock.Object);                

                var id = Guid.NewGuid();
                await activityService.AddAsync(id, Guid.NewGuid(), category, "activity", "description", DateTime.UtcNow);

                categoryRepositoryMock.Verify(x=> x.GetAsync(category),Times.Once);
                activityRepositoryMock.Verify(x=> x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}