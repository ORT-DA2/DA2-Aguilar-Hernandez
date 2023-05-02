using Blog.Models.In;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("api/comments")]
[ExceptionFilter]
[ServiceFilter(typeof(AuthorizationFilter))]
public class CommentController : ControllerBase{

    private readonly ICommentLogic _commentLogic;
    private readonly INotificationLogic _notificationLogic;
    public CommentController(ICommentLogic commentLogic, INotificationLogic notificationLogic)
    {
        _commentLogic = commentLogic;
        _notificationLogic = notificationLogic;
    }


    [HttpPost]
    public IActionResult PostNewComment([FromBody] CommentInModel commentInModel, [FromHeader] Guid Authorization)
    {
        Comment comment = commentInModel.ToEntity();
        Comment result = _commentLogic.AddNewComment(comment,Authorization,articleId);
        _notificationLogic.SendNotification(comment);
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