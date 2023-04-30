using Blog.Models.In;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentLogic _commentLogic;
        public CommentController(ICommentLogic commentLogic)
        {
            _commentLogic = commentLogic;
        }

        
        [HttpPost]
        public IActionResult PostNewComment([FromForm] CommentInModel commentInModel, [FromHeader] Guid Authorization, [FromBody] Guid articleId)
        {
            Comment comment = commentInModel.ToEntity();
            Comment result = _commentLogic.AddNewComment(comment,Authorization,articleId);
            return Created($"api/comments/{comment.Id}", new CommentOutModel(result));
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCommentById([FromRoute] Guid id)
        {
            try
            {
                _commentLogic.DeleteCommentById(id);
                return Ok($"Comment with the id {id} was deleted");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }    
}
