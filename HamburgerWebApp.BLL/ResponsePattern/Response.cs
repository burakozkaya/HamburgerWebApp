namespace HamburgerWebApp.BLL.ResponsePattern;

public class Response
{
    public string Message { get; set; }
    public bool IsSuccess { get; set; }

    public static Response Success(string message)
    {
        return new Response { IsSuccess = true, Message = message };
    }

    public static Response Failure(string message)
    {
        return new Response { IsSuccess = false, Message = message };
    }
}

public class Response<T> : Response
{
    public T Data { get; set; }

    public static Response<T> Success(T data, string message)
    {
        return new Response<T> { IsSuccess = true, Data = data, Message = message };
    }

    public static Response<T> Failure(string message)
    {
        return new Response<T> { IsSuccess = false, Message = message };
    }
}