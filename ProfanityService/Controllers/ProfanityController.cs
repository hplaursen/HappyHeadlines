using Microsoft.AspNetCore.Mvc;
using ProfanityService.Dto;
using ProfanityService.Services;

namespace ProfanityService.Controllers;

[ApiController]
[Route("api/profanity")]
public class CommentController(ProfanityFilterService profanityFilterService) : ControllerBase
{

    [HttpPost("filter")]
    public async Task<IActionResult> FilterComment(ProfanityFilterRequest profanityFilterRequest)
    {
        ProfanityFilterResponse? response = await profanityFilterService.FilterResponseAsync(profanityFilterRequest);
        if (response == null) return NotFound();
        return Ok(response);
    }
    
 
}