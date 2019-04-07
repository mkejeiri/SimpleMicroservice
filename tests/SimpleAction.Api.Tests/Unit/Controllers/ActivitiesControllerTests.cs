using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using SimpleAction.Api.Controllers;
using SimpleAction.Api.Repositories;
using SimpleAction.Api.Services;
using SimpleAction.Common.Commands;
using Xunit;

namespace SimpleAction.Api.Tests.Unit.Controllers
{
    public class ActivitiesControllerTests
    {
        [Fact]        
        public async Task activities_controller_post_should_return_acceptedAsync(){

            var busClientMosck = new Mock<IBusClient>();
            var activityRepositoryMock = new Mock<IActivityService>();
            var controller = new ActivitiesController(busClientMosck.Object, activityRepositoryMock.Object);
            var userId = Guid.NewGuid();            
            controller.ControllerContext = new ControllerContext{
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal( 
                        new ClaimsIdentity(
                            new Claim[] {new Claim (ClaimTypes.Name, userId.ToString())}
                            ,"Test"))
                    }
                };
            var command = new CreateActivity {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await controller.Post(command);
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            // contentResult.Location.ShouldBeEquivalentTo($"activities/{command.Id}");
             contentResult.Location.Should().BeEquivalentTo($"activities/{command.Id}");
        }
    }
}