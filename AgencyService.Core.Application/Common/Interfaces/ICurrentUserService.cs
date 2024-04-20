using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyService.Core.Application.Common.Interfaces;
public interface ICurrentUserService
{
    string? AccessToken { get; }
    string? Id { get; }
}
