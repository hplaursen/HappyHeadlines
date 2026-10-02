using CommentService.Data;
using CommentService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Repositories;

public class CommentRepository(CommentContext dbcontext, CommentCache commentCache)
{

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        dbcontext.Comments.Add(comment);
        await dbcontext.SaveChangesAsync();
        return comment;
    }
    
    public async Task<Comment?> GetCommentByIdAsync(int id)
        => await dbcontext.Comments.FindAsync(id);

    public async Task<IEnumerable<Comment>> GetCommentsByArticleAsync(int articleId)
    {
        List<Comment>? comments = await commentCache.GetAsync(articleId);

        if (comments == null) {
            comments = await dbcontext.Comments
                .Where(x => x.ArticleId == articleId).ToListAsync();

            await commentCache.SetAsync(articleId, comments);
        }
        return comments;
    }


    public async Task<bool> DeleteCommentAsync(int id)
    {
        Comment? comment = await dbcontext.Comments.FindAsync(id);
        if (comment == null) return false;

        dbcontext.Comments.Remove(comment);
        await dbcontext.SaveChangesAsync();
        return true;
    }   
}


