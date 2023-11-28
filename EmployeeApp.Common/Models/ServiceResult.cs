using EmployeeApp.Common.Enums;

namespace EmployeeApp.Common.Models
{
    public class ServiceResult
    {
        public ResponseType ResponseType { get; set; }

        public ServiceResult(ResponseType type)
        {
            ResponseType = type;
        }
    }
}

