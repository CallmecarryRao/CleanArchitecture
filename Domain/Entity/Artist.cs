using Common.Entities;
using SqlSugar;

namespace Domain.Entity;

public class Artist :ISoftDeleted, IAggregateRoot
{
    [SugarColumn(IsPrimaryKey = true)] public Guid Id { get; set; }

    [Navigate(typeof(ArtistSong), nameof(ArtistSong.ArtistId), nameof(ArtistSong.SongId))]
    public List<Song>? Songs { get; set; }

    [SugarColumn(Length = 5000)] public string Introduction { get; set; }

    public string Avator { get; set; }

    public bool IsDeleted { get; set; }
}

public record ArtistId(Guid id);