using BookMachine.API.Contracts.Requests;
using BookMachine.API.Contracts.Responses;
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

        [HttpGet]
        public async Task<ActionResult<List<BookResponse>>> GetAllBooksAsync()
        {
            List<Book> books = await _bookService.GetAllBooksAsync();

            List<BookResponse> response = books
                .Select(b => new BookResponse(b.BookId, b.Title))
                .ToList();

            return Ok(response);
        }
        [HttpGet("{bookId:guid}")]
        public async Task<ActionResult<BookResponse?>> GetBookByIdAsync(Guid bookId)
        {
            Book? book = await _bookService.GetBookByIdAsync(bookId);

            if (book == null)
            {
                return BadRequest();
            }

            BookResponse response = new(book!.BookId, book.Title);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBookAsync([FromBody] BookRequest request)
        {
            (Book Book, List<string> Errors) book = Book.Create(Guid.NewGuid(), request.Title);

            if (book.Errors.Count != 0)
            {
                return BadRequest(book.Errors);
            }

            Guid bookId = await _bookService.CreateBookAsync(book.Book);

            return Ok(bookId);
        }

        [HttpPut("{bookId:guid}")]
        public async Task<ActionResult<Guid>> UpdateBookAsync(Guid bookId, [FromBody] BookRequest request)
        {
            (Book Book, List<string> Errors) book = Book.Create(bookId, request.Title);

            if (book.Errors.Count != 0)
            {
                return BadRequest(book.Errors);
            }

            await _bookService.UpdateBookAsync(bookId, book.Book);

            return Ok(bookId);
        }

        [HttpDelete("{bookId:guid}")]
        public async Task<ActionResult<Guid>> DeleteBookAsync(Guid bookId)
        {
            await _bookService.DeleteBookAsync(bookId);

            return Ok(bookId);
        }
    }
}
