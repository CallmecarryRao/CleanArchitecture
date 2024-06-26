using Common.Entities;

namespace Domain.Entity;

public class File : BaseEntity
{
    public Guid Id { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }

    private bool IsExist(string filename)
    {
        if (this.FileName == filename)
            return true;
        return false;
    }
}

public record FileId(Guid value);