using AgencyService.Core.Application.Common.Interfaces;
using AgencyService.Core.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace AgencyService.Core.Application.Common.Exceptions;
[Serializable]
public sealed class BadRequestException : Exception, IExceptionStrategy
{
    private readonly int _statusCode;

    public BadRequestException(string? message) : base(message)
    {
        _statusCode = StatusCodes.Status400BadRequest;
    }

    private BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _statusCode = StatusCodes.Status400BadRequest;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("statusCode", _statusCode);
    }

    public async Task ModifyAndWriteAsJsonAsync(HttpResponse response)
    {
        response.ContentType = "application/json";
        response.StatusCode = _statusCode;
        await response.WriteAsJsonAsync(new ExceptionDto
        {
            Message = Message
        });
    }
}