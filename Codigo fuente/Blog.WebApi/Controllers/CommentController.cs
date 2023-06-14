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
public class CommentController : ControllerBase{

    private readonly ICommentLogic _commentLogic;
    public CommentController(ICommentLogic commentLogic, INotificationLogic notificationLogic)
    {
        _commentLogic = commentLogic;
    }

    [HttpGet]
    [AuthenticationRoleFilter(Roles = new[] { Role.Admin })]
    public IActionResult GetAll()
    {
        IEnumerable<Comment> comments = _commentLogic.GetAll();
        IEnumerable<Comment> offensiveComments = comments.Where(a => a.OffensiveContent.Any());
        List<CommentOutModel> commentsDTO = offensiveComments.Select(comment => new CommentOutModel(comment)).ToList();
        return Ok(commentsDTO);
    }


    [HttpPost]
    [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
    public IActionResult PostNewComment([FromBody] CommentInModel commentInModel, [FromHeader] Guid Authorization)
    {
        Comment comment = commentInModel.ToEntity();
        Comment result = _commentLogic.AddNewComment(comment, commentInModel.ArticleId, Authorization);
        
        return Created($"api/comments/{comment.Id}", new CommentOutModel(result));

    }

    [HttpDelete("{id}")]
    [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
    public IActionResult DeleteCommentById([FromRoute] Guid id)
    {
        _commentLogic.DeleteCommentById(id);
        return Ok($"Comment with the id {id} was deleted");
    }

    [HttpPut]
    [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
    public IActionResult ReplyComment([FromBody]ReplyCommentDto reply)
    {
        Comment commentReplied = _commentLogic.ReplyComment(reply.CommentId, reply.Reply);
        return Ok(new CommentOutModel(commentReplied));
    }
}