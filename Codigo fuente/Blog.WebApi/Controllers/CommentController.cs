using Blog.Models.In;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
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
[AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
public class CommentController : ControllerBase{

    private readonly ICommentLogic _commentLogic;
    public CommentController(ICommentLogic commentLogic, INotificationLogic notificationLogic)
    {
        _commentLogic = commentLogic;
    }


    [HttpPost]
    public IActionResult PostNewComment([FromBody] CommentInModel commentInModel, [FromHeader] Guid Authorization)
    {
        Comment comment = commentInModel.ToEntity();
        Comment result = _commentLogic.AddNewComment(comment, commentInModel.ArticleId, Authorization);
        
        return Created($"api/comments/{comment.Id}", new CommentOutModel(result));

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCommentById([FromRoute] Guid id)
    {
        _commentLogic.DeleteCommentById(id);
        return Ok($"Comment with the id {id} was deleted");
    }

    [HttpPut]
    public IActionResult ReplyComment([FromBody]ReplyCommentDto reply)
    {
        Comment commentReplied = _commentLogic.ReplyComment(reply.CommentId, reply.Reply);
        return Ok(new CommentOutModel(commentReplied));
    }
}