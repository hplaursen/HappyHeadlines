using DraftService.Data;
using DraftService.Models;

namespace DraftService.Repositories;
public class DraftRepository(DraftContext dbContext)
{

    public async Task<Draft> CreateDraftAsync(Draft draft)
    {

        dbContext.Drafts.Add(draft);
        await dbContext.SaveChangesAsync();
        return draft;
    }

    public async Task<Draft?> GetDraftByIdAsync(int id)
    {
        return await dbContext.Drafts.FindAsync(id);
    }

    public async Task<Draft?> UpdateDraftAsync(Draft updatedDraft)
    {
        Draft? draft = await dbContext.Drafts.FindAsync(updatedDraft.Id);
        if (draft == null) return null;

        draft.Title = updatedDraft.Title;
        draft.Content = updatedDraft.Content;
        draft.UpdatedAt = updatedDraft.UpdatedAt;
        draft.PublishedAt = updatedDraft.PublishedAt;

        await dbContext.SaveChangesAsync();
        return draft;
    }

    public async Task<bool> DeleteDraftAsync(int id)
    {
        Draft? draft = await dbContext.Drafts.FindAsync(id);
        if (draft == null) return false;

        dbContext.Drafts.Remove(draft);
        await dbContext.SaveChangesAsync();
        return true;
    }   
}


