using BookMachine.API.Contracts.Requests.BookRequests;
using BookMachine.API.Contracts.Responses.BookResponses;
using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMachine.API.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class BookController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        [HttpGet(Name = "GetAllBooks")]
        public async Task<ActionResult<List<GetAllBooksResponse>>> GetAllBooksAsync()
        {
            List<Book> books = await _bookService.GetAllBooksAsync();

            List<GetAllBooksResponse> response = books
                .Select(b => new GetAllBooksResponse(b.BookId, b.Title, b.AuthorId, b.Author!.Name))
                .ToList();

            return Ok(response);
        }

        [HttpGet("Filter", Name = "GetBookByFilter")]
        public async Task<ActionResult<GetBookByFilterResponse>> GetBookByFilterAsync(GetBookByFilterRequest request)
        {
            List<Book> books = await _bookService.GetBookByFilterAsync(request.Search, request.SortItem, request.SortOrder);

            List<GetBookByFilterResponse> response = books
                .Select(b => new GetBookByFilterResponse(b.BookId, b.Title, b.AuthorId, b.Author!.Name))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id:guid}", Name = "GetBookById")]
        public async Task<ActionResult<GetBookByIdResponse?>> GetBookByIdAsync(Guid id)
        {
            Book? book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return BadRequest();
            }

            GetBookByIdResponse response = new(book!.BookId, book.Title, book.Author!);

            return Ok(response);
        }

        [HttpPost(Name = "CreateBook")]
        public async Task<ActionResult<Guid>> CreateBookAsync([FromBody] CreateBookRequest request)
        {
            (Book Book, List<string> Errors) book = Book.Create(Guid.NewGuid(), request.BookTitle, request.AuthorId, null);

            if (book.Errors.Count != 0)
            {
                return BadRequest(book.Errors);
            }

            Guid bookId = await _bookService.CreateBookAsync(book.Book);

            return Ok(bookId);
        }

        [HttpPut("{id:guid}", Name = "UpdateBook")]
        public async Task<ActionResult<Guid>> UpdateBookAsync(Guid id, [FromBody] UpdateBookRequest request)
        {
            (Book Book, List<string> Errors) book = Book.Create(id, request.BookTitle, request.AuthorId, null);

            if (book.Errors.Count != 0)
            {
                return BadRequest(book.Errors);
            }

            await _bookService.UpdateBookAsync(id, book.Book);

            return Ok(id);
        }

        [HttpDelete("{id:guid}", Name = "DeleteBook")]
        public async Task<ActionResult<Guid>> DeleteBookAsync(Guid id)
        {
            await _bookService.DeleteBookAsync(id);

            return Ok(id);
        }
    }
}
