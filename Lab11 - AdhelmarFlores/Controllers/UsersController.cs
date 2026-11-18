using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab11___AdhelmarFlores.Application.UseCases.Users.Commands;
using Lab11___AdhelmarFlores.Application.UseCases.Users.Queries;

namespace Lab11___AdhelmarFlores.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    // GET /api/users — Lista todos los usuarios (Query)
    [HttpGet]
    public async Task<IActionResult> List() =>
        Ok(await _mediator.Send(new ListUsersQuery()));

    // POST /api/users — Crea un nuevo usuario (Command)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command) =>
        Ok(await _mediator.Send(command));
}
