using Common.Entities;
using SqlSugar;

namespace Domain.Entity;

[SugarTable("ArtistSong")]
public class ArtistSong:BaseEntity
{
    [SugarColumn(IsPrimaryKey = true)] public Guid SongId { get; set; }

    [SugarColumn(IsPrimaryKey = true)] public Guid ArtistId { get; set; }

}