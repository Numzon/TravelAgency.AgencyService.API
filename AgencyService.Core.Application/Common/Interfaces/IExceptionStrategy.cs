using Microsoft.AspNetCore.Http;
using System.Net;

namespace AgencyService.Core.Application.Common.Interfaces;
public interface IExceptionStrategy
{
    Task ModifyAndWriteAsJsonAsync(HttpResponse response);
}
