using Blog.Models.In;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    [ExceptionFilter]
    public class CommentController : ControllerBase
    {
        private readonly ICommentLogic _commentService;
        public CommentController(ICommentLogic commentService)
        {
            _commentService = commentService;
        }

        
        [HttpPost]
        public IActionResult PostNewComment([FromBody] CommentInModel commentInModel, [FromHeader] Guid Authorization)
        {
            Comment comment = commentInModel.ToEntity();
            Comment result = _commentService.AddNewComment(comment, commentInModel.Article, Authorization);
            CommentOutModel commentOut = new CommentOutModel(result);
            return Ok(commentOut);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCommentById([FromRoute] Guid id)
        {
            try
            {
                _commentService.DeleteCommentById(id);
                return Ok($"Comment with the id {id} was deleted");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }    
}
