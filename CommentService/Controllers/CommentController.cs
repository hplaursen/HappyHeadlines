using Microsoft.AspNetCore.Mvc;
using CommentService.Models;
using CommentService.Services;

namespace CommentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(CommentManager commentManager) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment(int id)
    {
        Comment? comment = await commentManager.GetCommentByIdAsync(id);
        if (comment == null) return NotFound();
        return Ok(comment);
    }

    [HttpGet("article/{id}")]
    public async Task<IActionResult> GetComments(int id)
    {
        IEnumerable<Comment?> comments = await commentManager.GetCommentsByArticleAsync(id);
        if (comments == null) return NotFound();
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(Comment comment)
    {
        Comment createdComment = await commentManager.CreateCommentAsync(comment);
        return CreatedAtAction(nameof(GetComment), new { id = createdComment.Id }, createdComment);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var result = await commentManager.DeleteCommentAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }
    
}