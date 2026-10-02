using System.IO.Compression;
using DraftService.Data;
using DraftService.Models;
using DraftService.Repositories;

namespace DraftService.Service;
public class DraftManager(DraftRepository draftRepository)
{

    public async Task<Draft> CreateDraftAsync(Draft draft)
        =>  await draftRepository.CreateDraftAsync(draft);

    public async Task<Draft?> GetDraftByIdAsync(int id)
        => await draftRepository.GetDraftByIdAsync(id);

    public async Task<Draft?> UpdateDraftAsync(Draft updatedDraft)
        => await draftRepository.UpdateDraftAsync(updatedDraft);

        
    public async Task<bool> DeleteDraftAsync(int id)
        => await draftRepository.DeleteDraftAsync(id);  
}


