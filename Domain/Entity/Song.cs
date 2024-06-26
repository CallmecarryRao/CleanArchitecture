using Common.Entities;
using SqlSugar;

namespace Domain.Entity;

[SugarTable("Song")]
public class Song : ISoftDeleted, IAggregateRoot
{
    [SugarColumn(IsPrimaryKey = true)] public Guid Id { get; set; }

    /// <summary>
    /// 歌名
    /// </summary>
    public string Name { get; set; }


    /// <summary>
    /// 歌词
    /// </summary>
    public string Lyrics { get; set; }


    /// <summary>
    ///  歌曲路径 
    /// </summary>
    public Guid FilePathid { get; set; }

    /// <summary>
    /// 歌手
    /// </summary>
    [Navigate(typeof(ArtistSong), nameof(ArtistSong.SongId), nameof(ArtistSong.ArtistId))]
    public List<Artist>? artists { get; set; }


    public bool IsDeleted { get; set; }

    public Song CreateSong(SongId id, string name, string lyrics)
    {
        this.Id = id.value;
        this.Name = name;
        this.Lyrics = lyrics;
        return this;
    }

    private Song BindArtist(Artist artist)
    {
        this.artists.Add(artist);
        return this;
    }

    private Song BindFile(FileId id)
    {
        this.FilePathid = id.value;
        return this;
    }
}

public record SongId(Guid value);