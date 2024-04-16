using MediatR;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : Controller
{
    public readonly IMediator _mediator;
    public int result;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}


