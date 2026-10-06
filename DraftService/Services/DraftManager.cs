using DraftService.Models;
using DraftService.Repositories;

namespace DraftService.Services;
public class DraftManager(DraftRepository draftRepository, ILogger<DraftManager> logger)
{

    public async Task<Draft> CreateDraftAsync(Draft draft)
    {
        var createdDraft = await draftRepository.CreateDraftAsync(draft);

        logger.LogInformation("Created draft with ID {DraftId}", createdDraft.Id);

        return createdDraft;
    }
 

    public async Task<Draft?> GetDraftByIdAsync(int id)
    {
        logger.LogInformation("Getting draft with ID {DraftId}", id);

        try
        {
            var draft = await draftRepository.GetDraftByIdAsync(id);

            if (draft == null)
                logger.LogWarning("Draft with ID {DraftId} was not found", id);

            return draft;
        }
        catch (Exception ex)
        {
            logger.LogError( ex, "Error getting draft with ID {DraftId}", id);
            throw;
        }
    }


    public async Task<Draft?> UpdateDraftAsync(Draft updatedDraft)
    {
        logger.LogInformation("Updating draft with ID {DraftId}", updatedDraft.Id);
        return await draftRepository.UpdateDraftAsync(updatedDraft);
    }

    public async Task<bool> DeleteDraftAsync(int id)
    {
        logger.LogInformation("Deleting draft with ID {DraftId}", id);
        return await draftRepository.DeleteDraftAsync(id);
    }
}


