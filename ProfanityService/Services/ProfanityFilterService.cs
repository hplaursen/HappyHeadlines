using ProfanityService.Data;
using ProfanityService.Dto;
using ProfanityService.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ProfanityService.Services;


public class ProfanityFilterService(ProfanityContext dbContext)
{
    
    public async Task<ProfanityFilterResponse> FilterResponseAsync(ProfanityFilterRequest profanityFilterRequest)
    {
        ProfanityFilterResponse response = new();
        IEnumerable<Profanity> profanities = await dbContext.Profanities.ToListAsync();
        response.Content =  profanityFilterRequest.Content;

        foreach (Profanity profanity in profanities)
        {
            response.Content = Regex.Replace(
            response.Content,
            Regex.Escape(profanity.Word),
            new string('*', profanity.Word.Length),
            RegexOptions.IgnoreCase);
        }

        return response;
    }

}