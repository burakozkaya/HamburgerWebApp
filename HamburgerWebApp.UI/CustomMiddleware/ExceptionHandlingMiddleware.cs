namespace HamburgerWebApp.UI.CustomMiddleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Items["Exception"] = "Some Error Occured. You are redirected to Home Index";
            context.Response.Redirect("/Home/Index");
        }
    }
}