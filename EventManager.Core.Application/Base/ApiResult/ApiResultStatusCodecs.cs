using System.ComponentModel;

namespace EventManager.Core.Application.Base.ApiResult
{
    public enum ApiResultStatusCode
    {
        [Description("Success")]
        Success = 200,

        [Description("Server Error")]
        ServerError = 500,

        [Description("Bad Request Error")]
        BadRequest = 400,

        [Description("Not Found")]
        NotFound = 404,

        [Description("Empty Error")]
        ListEmpty = 404,

        [Description("Process Error")]
        LogicError = 500,

        [Description("Authentication Error")]
        UnAuthorized = 401,

        [Description("Not Acceptable")]
        NotAcceptable = 406,

        [Description("Failed Dependency")]
        FailedDependency = 424
    }
}
