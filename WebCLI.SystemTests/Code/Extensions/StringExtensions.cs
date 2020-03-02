namespace WebCLI.SystemTests.Code.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Quick and dirty fix for Windows to Linux line endings
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NormalizeLineEndings(this string text)
        {
            return text.Replace("\r\n", "\n");
        }
    }
}
