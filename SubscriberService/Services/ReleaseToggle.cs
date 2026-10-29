using Microsoft.EntityFrameworkCore;
using SubscriberService.Data;

namespace SubscriberService.Services;

public sealed class ReleaseToggle(SubscriberContext database)
{
    public async Task<bool> IsEnabledAsync(CancellationToken cancellationToken) =>
        await database.FeatureStates.AsNoTracking()
            .Where(state => state.Id == 1)
            .Select(state => state.Enabled)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task SetEnabledAsync(bool enabled, CancellationToken cancellationToken)
    {
        var state = await database.FeatureStates.SingleOrDefaultAsync(item => item.Id == 1, cancellationToken);
        if (state is null)
            database.FeatureStates.Add(new SubscriberFeatureState { Id = 1, Enabled = enabled });
        else
            state.Enabled = enabled;
        await database.SaveChangesAsync(cancellationToken);
    }
}
