using Common.Global;
using SqlSugar;

namespace Common.ConfigOptions;

public class DataBaseConnection
{
    public List<ConnectionItem> ConnectionItem { get; set; }
}

public class ConnectionItem
{
    public int Id { get; set; }
    public int DbType { get; set; }
    public string ConnectionString { get; set; }
    public bool Enabled { get; set; }
    public bool IsSalves { get; set; }
    public List<Salve> Salves { get; set; }
}

public class Salve
{
    
    public int HitRate { get; set; }
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; }

}