namespace Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>
{
    public string? CreatedBy { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; } = DateTimeOffset.UtcNow;
    public string? UpdatedBy { get; private set; }
    public DateTimeOffset? UpdatedOn { get; private set; }

    public void SetCreatedBy(string? createdBy, DateTimeOffset? createdOn = null)
    {
        CreatedBy = createdBy;
        CreatedOn = createdOn ?? DateTimeOffset.UtcNow;
        UpdatedBy = CreatedBy;
        UpdatedOn = CreatedOn;
    }

    public void SetUpdatedBy(string? updatedBy, DateTimeOffset? updatedOn = null)
    {
        UpdatedBy = updatedBy;
        UpdatedOn = updatedOn ?? DateTimeOffset.UtcNow;
    }
}