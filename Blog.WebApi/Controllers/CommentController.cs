using Blog.Models.In;
using Blog.Domain.Entities;
using Blog.Filters.Exceptions;
using Blog.IBusinessLogic;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentLogic _commentService;
        public CommentController(ICommentLogic commentService)
        {
            _commentService = commentService;
        }

        
        [HttpPost]
        public IActionResult PostNewComment([FromBody] CommentInModel commentInModel)
        {
            Comment comment = commentInModel.ToEntity();
            Comment result = _commentService.AddNewComment(comment);
            CommentOutModel commentOut = new CommentOutModel(result);
            return Ok();
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
