using BookMachine.API.Contracts.Requests.AuthorRequests;
using BookMachine.API.Contracts.Responses.AuthorResponses;
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

        [HttpGet(Name = "GetAllAuthors")]
        public async Task<ActionResult<List<GetAllAuthorsResponse>>> GetAllAuthorsAsync()
        {
            List<Author> authors = await _authorService.GetAllAuthorsAsync();

            List<GetAllAuthorsResponse> response = authors
                .Select(a => new GetAllAuthorsResponse(a.AuthorId, a.Name, a.Books))
                .ToList();

            return Ok(response);
        }

        [HttpGet("Filter", Name = "GetAuthorByFilter")]
        public async Task<ActionResult<GetAuthorByFilterResponse>> GetAuthorByFilterAsync(GetAuthorByFilterRequest request)
        {
            List<Author> authors = await _authorService.GetAuthorByFilterAsync(request.Search, request.SortItem, request.SortOrder);

            List<GetAuthorByFilterResponse> response = authors
                .Select(a => new GetAuthorByFilterResponse(a.AuthorId, a.Name, a.Books))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id:guid}", Name = "GetAuthorById")]
        public async Task<ActionResult<GetAllAuthorsResponse?>> GetAuthorByIdAsync(Guid id)
        {
            Author? author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return BadRequest();
            }

            GetAuthorByIdResponse response = new(author!.AuthorId, author.Name, author.Books);

            return Ok(response);
        }

        [HttpPost(Name = "CreateAuthor")]
        public async Task<ActionResult<Guid>> CreateAuthorAsync([FromBody] CreateAuthorRequest request)
        {
            (Author Author, List<string> Errors) author = Author.Create(Guid.NewGuid(), request.AuthorName, []);

            if (author.Errors.Count != 0)
            {
                return BadRequest(author.Errors);
            }

            Guid authorId = await _authorService.CreateAuthorAsync(author.Author);

            return Ok(authorId);
        }

        [HttpPut("{id:guid}", Name = "UpdateAuthor")]
        public async Task<ActionResult<Guid>> UpdateAuthorAsync(Guid id, [FromBody] UpdateAuthorRequest request)
        {
            (Author Author, List<string> Errors) author = Author.Create(id, request.AuthorName, []);

            if (author.Errors.Count != 0)
            {
                return BadRequest(author.Errors);
            }

            await _authorService.UpdateAuthorAsync(id, author.Author);

            return Ok(id);
        }

        [HttpDelete("{id:guid}", Name = "DeleteAuthor")]
        public async Task<ActionResult<Guid>> DeleteAuthorAsync(Guid id)
        {
            await _authorService.DeleteAuthorAsync(id);

            return Ok(id);
        }

    }
}
