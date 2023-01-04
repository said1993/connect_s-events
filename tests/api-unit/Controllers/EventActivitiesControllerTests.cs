using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using connect_s_events_api.Controllers;
using connect_s_events_api.Handlers;

namespace connect_s_eventss_api_unit.Controllers;
public class EventActivitiesControllerTests
{
    private readonly IFixture _fixture;
    private EventActivitiesController _sut;
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<ILogger<EventActivitiesController>> _mockLogger;
    public EventActivitiesControllerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockMediator = _fixture.Freeze<Mock<IMediator>>();
        _mockLogger = _fixture.Freeze<Mock<ILogger<EventActivitiesController>>>();
        _sut = _fixture.Build<EventActivitiesController>()
            .With(c => c.ControllerContext, new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            })
            .Create();
    }
    [Theory, AutoData]
    public async Task GetAllEventActivitiesreturnsOk(IEnumerable<GetEventActivitiesResponse> response)
    {
        _mockMediator.Setup(_ => _.Send(It.IsAny<GetEventActivitiesRequest>(), It.IsAny<CancellationToken>()))
            .Returns(new ValueTask<IEnumerable<GetEventActivitiesResponse>>(response));

        var result = await _sut.GetAll();

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();

    }
}
