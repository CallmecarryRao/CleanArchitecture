using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Api.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "SongManage")]
[Route("api/[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private Test t;
    private readonly ISqlSugarClient _sugar;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,Test t,ISqlSugarClient sugar)
    {
        
        _logger = logger;
        t = this.t;
        _sugar = sugar;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [ActionName("test")]
    public string Get()
    {
        var tenet = t.GetHeaderValue("tenant");
        // var s = new Song();
        // var id = new SongId(Guid.NewGuid());
        // s.CreateSong(id, "123", "wqeq");
        // _sugar.CodeFirst.InitTables(typeof(ArtistSong));
        // _sugar.CodeFirst.InitTables(typeof(Artist));
        // _sugar.CodeFirst.InitTables(typeof(Song));
        // _sugar.Insertable<Song>(s).ExecuteCommand();
        //
         var list = _sugar.Queryable<Song>().ToList();
  
        var temp= _sugar.AsTenant().GetConnection(1);
         using (SqlSugarScope uow33= _sugar as SqlSugarScope) 
         {
             uow33.BeginTran();
             
             uow33.CommitTran();

         }
        return "123";
    }
}