using CommentService.Models;
using CommentService.Dto;
using CommentService.Repositories;

namespace CommentService.Services;


public class CommentManager(CommentRepository commentRepository, ProfanityClient profanityClient)
{
    public async Task<Comment?> GetCommentByIdAsync(int id)
        => await commentRepository.GetCommentByIdAsync(id);


    public async Task<IEnumerable<Comment?>> GetCommentsByArticleAsync(int articleId)
        => await commentRepository.GetCommentsByArticleAsync(articleId);


    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        try
        {
            ProfanityFilterDto? response = 
                await profanityClient.FilterAsync(new ProfanityFilterDto(comment.Content));
            comment.Content = response.Content;
        }
        catch
        {
            //log error
        }

        Comment createdComment = 
            await commentRepository.CreateCommentAsync(comment);

        return createdComment;
}

    public async Task<bool> DeleteCommentAsync(int id)
        => await commentRepository.DeleteCommentAsync(id);


}
