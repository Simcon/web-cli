using Newtonsoft.Json;

namespace WebCLI.Code.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly string Null = "null";
        private static readonly string Exception = "Exception";

        public static string ToJson(this object value, Formatting formatting = Formatting.None)
        {
            if (value == null) return Null;

            try
            {
                return JsonConvert.SerializeObject(value, formatting);
            }
            catch
            {
                return Exception;
            }
        }
    }
}
