using CommentService.Dto;

namespace CommentService.Services;

public class ProfanityClient(HttpClient client)
{
    public async Task<ProfanityFilterDto> FilterAsync(ProfanityFilterDto request)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/profanity/filter", request);
        
        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<ProfanityFilterDto>()
            ?? throw new Exception("ProfanityService returned no content");

    }   
}