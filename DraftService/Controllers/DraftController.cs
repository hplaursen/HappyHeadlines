using DraftService.Service;
using DraftService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DraftService.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DraftController(DraftManager draftManager) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDraft(int id)
    {
        Draft? draft = await draftManager.GetDraftByIdAsync(id);
        if (draft == null) return NotFound();

        return Ok(draft);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDraft(Draft draft)
    {
        Draft createdDraft = await draftManager.CreateDraftAsync(draft);
        return CreatedAtAction(nameof(GetDraft), new { id = createdDraft.Id }, createdDraft);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDraft(Draft updatedDraft)
    {
        Draft? draft = await draftManager.UpdateDraftAsync(updatedDraft);
        if (draft == null) return NotFound();

        return Ok(draft);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDraft(int id)
    {
        var result = await draftManager.DeleteDraftAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }
}