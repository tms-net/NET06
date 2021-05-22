using System;

namespace TMS.NET06.Parfume.MVC.Kovaleva.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
