using BookMachine.API.Contracts.Requests;
using BookMachine.API.Contracts.Responses;
using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMachine.API.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AuthorController(IAuthorService authorService) : ControllerBase
    {
        private readonly IAuthorService _authorService = authorService;

        [HttpGet]
        public async Task<ActionResult<List<AuthorResponse>>> GetAllAuthorsAsync()
        {
            List<Author> authors = await _authorService.GetAllAuthorsAsync();

            List<AuthorResponse> response = authors
                .Select(a => new AuthorResponse(a.AuthorId, a.Name))
                .ToList();

            return Ok(response);
        }
        [HttpGet("{authorId:guid}")]
        public async Task<ActionResult<AuthorResponse?>> GetAuthorByIdAsync(Guid authorId)
        {
            Author? author = await _authorService.GetAuthorByIdAsync(authorId);

            if (author == null)
            {
                return BadRequest();
            }

            AuthorResponse response = new(author!.AuthorId, author.Name);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAuthorAsync([FromBody] AuthorRequest request)
        {
            (Author Author, List<string> Errors) author = Author.Create(Guid.NewGuid(), request.Name);

            if (author.Errors.Count != 0)
            {
                return BadRequest(author.Errors);
            }

            Guid authorId = await _authorService.CreateAuthorAsync(author.Author);

            return Ok(authorId);
        }

        [HttpPut("{authorId:guid}")]
        public async Task<ActionResult<Guid>> UpdateAuthorAsync(Guid authorId, [FromBody] AuthorRequest request)
        {
            (Author Author, List<string> Errors) author = Author.Create(authorId, request.Name);

            if (author.Errors.Count != 0)
            {
                return BadRequest(author.Errors);
            }

            await _authorService.UpdateAuthorAsync(authorId, author.Author);

            return Ok(authorId);
        }

        [HttpDelete("{authorId:guid}")]
        public async Task<ActionResult<Guid>> DeleteAuthorAsync(Guid authorId)
        {
            await _authorService.DeleteAuthorAsync(authorId);

            return Ok(authorId);
        }

    }
}
