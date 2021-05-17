using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.NET06.TwitterListener.Manager.React.Data.Models
{
    public enum ListenerTaskStatus
    {
        Undefined = 0,
        Started,
        Processing,
        Succeeded = 200,
        Failed = 400
    }
}
