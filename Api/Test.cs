namespace Api;

public class Test
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Test(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetHeaderValue(string headerName)
    {
        // 获取当前的 HTTP 上下文
        var context = _httpContextAccessor.HttpContext;

        // 获取请求对象
        var request = context.Request;

        // 获取指定的请求头值
        int headerValue = Convert.ToInt32(request.Headers[headerName]);

        return headerValue;
    }
}