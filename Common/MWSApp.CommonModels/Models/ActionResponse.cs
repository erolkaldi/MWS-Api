
using MWSApp.CommonModels.Enums;

namespace MWSApp.CommonModels.Models
{
    public class ActionResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = "";
        public ResponseType ResponseType { get; set; } = ResponseType.OK;
    }
    public class ActionResponse
    {
        public string Id { get; set; } = "";
        public string Message { get; set; } = "";
        public ResponseType ResponseType { get; set; } = ResponseType.OK;
    }
}
