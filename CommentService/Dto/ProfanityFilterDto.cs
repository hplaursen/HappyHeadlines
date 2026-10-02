namespace CommentService.Dto;

public class ProfanityFilterDto(string content = "")
{
    public string Content {get; set;} = content;
}