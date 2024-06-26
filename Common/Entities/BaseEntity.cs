namespace Common.Entities;

public class BaseEntity
{
    /// <summary>
    /// 生成时间
    /// </summary>
    public DateTimeOffset CreateTime { get; set; }
    
    /// <summary>
    /// 最后操作时间
    /// </summary>
    public DateTimeOffset LastModifyTime { get; set; }


    /// <summary>
    /// 并发了乐观锁
    /// </summary>
    public byte[] RowVersion { get; set; }


    /// <summary>
    /// 创建人
    /// </summary>
    public Guid CreateUserId { get; set; }


    /// <summary>
    /// 修改人
    /// </summary>
    public Guid ModifyUserId { get; set; }
    
    

}