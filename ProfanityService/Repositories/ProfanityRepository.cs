using ProfanityService.Data;
using ProfanityService.Models;
using Microsoft.EntityFrameworkCore;

namespace ProfanityService.Repositories;

public class ProfanityRepository(ProfanityContext dbcontext)
{
    public async Task<Profanity?> GetProfanityByIdAsync(int id)
        => await dbcontext.Profanities.FindAsync(id);

    public async Task<IEnumerable<Profanity>> GetProfanitiesAsync()
        => await dbcontext.Profanities.ToListAsync();
}


