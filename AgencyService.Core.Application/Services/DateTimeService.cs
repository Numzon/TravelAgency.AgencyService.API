using AgencyService.Core.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyService.Core.Application.Services;
public sealed class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
