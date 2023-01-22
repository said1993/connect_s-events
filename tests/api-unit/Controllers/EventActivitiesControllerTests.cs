using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using connect_s_event_api.Exceptions;
using connect_s_events_api.Controllers;
using connect_s_events_api.Handlers;
using FluentAssertions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

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
    [Theory(DisplayName = "Get All activiteies returns 200 ok"), AutoData]
    public async Task GetAllEventActivitiesreturnsOk(IEnumerable<GetEventActivitiesResponse> response)
    {
        _mockMediator.Setup(_ => _.Send(It.IsAny<GetEventActivitiesRequest>(), It.IsAny<CancellationToken>()))
            .Returns(new ValueTask<IEnumerable<GetEventActivitiesResponse>>(response));

        var result = await _sut.GetAll();

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();

    }

    [Theory(DisplayName = "Create an activity returns 201 Created"), AutoData]
    public async Task createEventActivitiesreturns_201Created(CreateEventActivityRequest request)
    {
        _mockMediator.Setup(_ => _.Send(request, It.IsAny<CancellationToken>()))
            .Returns(new ValueTask<Unit>(Unit.Value));

        var result = await _sut.Create(request);

        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedResult>();
    }

    [Theory(DisplayName = "Add paritcipiant to an activity returns 202 accepted"), AutoData]
    public async Task AddParicipiantToActivityreturns_202Accepted(AddParticipantCommand addParticipantCommand)
    {
        _mockMediator.Setup(_ => _.Send(addParticipantCommand, It.IsAny<CancellationToken>()))
            .Returns(new ValueTask<Unit>(Unit.Value));

        var result = await _sut.AddParticipant(addParticipantCommand);

        result.Should().NotBeNull();
        result.Should().BeOfType<AcceptedResult>();
    }

    [Theory(DisplayName = "Add paritcipiant to an activity returns BadRequest when the command is invalid"), AutoData]
    public async Task AddParicipiantToActivityreturns_BadRequest(AddParticipantCommand addParticipantCommand)
    {
        _mockMediator.Setup(_ => _.Send(addParticipantCommand, It.IsAny<CancellationToken>()))
            .Throws(new InvalidRequestExcepetion());

        var result = await _sut.AddParticipant(addParticipantCommand);

        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Theory(DisplayName = "Add paritcipiant to an activity returns BadRequest when the command is invalid"), AutoData]
    public async Task DeleteActivityreturns_202Accepted(Guid id)
    {
        _mockMediator.Setup(_ => _.Send(It.Is<DeleteEventActivityCommand>(_ => _.EventActivityId == id), It.IsAny<CancellationToken>()))
            .Returns(new ValueTask<Unit>(Unit.Value));

        var result = await _sut.Delete(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<AcceptedResult>();
    }
}
