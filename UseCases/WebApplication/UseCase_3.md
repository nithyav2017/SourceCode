**UseCase**: Multi-server deployment loses sessions. Switch to SQL Server session state in web.config, handle timeouts in C#.

#### Problem Context: 
The default In-Proc session in ASP.NET application stores session in the server's process memory. In the multi                server environment , user requests are routed through a load balancer to different servers.Since each server                  maintains its own isolated memory and the session data is not shared across the servers. 
             
As a result,  if a user's subsequent request is handled by a different server than the one that originally                    created the session, the new server cannot find the session data in its memory and leading to session loss. For               example, user appears logged out .

#### SQL Server Session State Configuration:

Install exe (aspnet_regsql) for Database schema to handle session in sql server instance.

This will create ASPState database with necessary tables, Stored Procedures.

Basic: 
    `aspnet_regsql.exe -S servername -E -ssadd` [uses ASPState and tempdb databases]

    
To persist: 
    `aspnet_regsql.exe -S servername -E -ssadd -sstype p` [uses ASPState database and persists the same session during SQL server restarts]

#### Web.Config Configuration:
In the <System.Web> section ,
  
`<sessionState mode = "SQLServer" 
                SQLCOnnectionString=""
                timeout ="30"
                allowCustomSQlDatabase="true"
                cookieless="false"/>`

In addtion to these setup , 
      - use the same Machine Key in the Web.Config across the servers 
      - set ValidationKey and DecryptionKey explicitly.Each data session is encrypted.
      - use same site ID for the application instance across the servers 
      - Application path is also identical.

#### Handling Session Timeout in C#:
To handle the 'timeout' happens example redirect to login page,create a global action filter attribute to handle session 
expiration.

#### Example:
Create a action filter in the application .

`public class SessionExpireFilterAttribute : ActionFilterAttribute
{
  public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        HttpContext ctx = HttpContext.Current;

        if (ctx.Session != null)
        {
            if (ctx.Session.IsNewSession)
            {
                if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null 
                  && HttpContext.Current.Session.IsNewSession)
                {
                    // Session timed out; redirect to login
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    // Or for Web Forms: ctx.Response.Redirect("~/Login.aspx");
                }
            }
        }

        base.OnActionExecuting(filterContext);
    }
}`

#### How it works:

  - ctx.Session.IsNewSession: checks every time when the page is requested. It returns true when
        - the page is requested first time [no cookie sent and no prior session].
        - SQL server deletes the session id when `timeout` reached.  There is no previous session in the database.So, 
          it creates New Session.

  - However the `Cookie Header` is not null, it still retains the same SessionId. ASP.NET ignores this old/invalid SessionId from the
    cookie and generates new SessionId, then it sends this new Sessionid back to the browser via new Set-Cookie header.
         `if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null 
          && HttpContext.Current.Session.IsNewSession)'

    
  The above condition returns true and page is redirect to `Login`
