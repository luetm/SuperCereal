using System;
using System.Text;
using Common.Extensions;

namespace SuperCereal.Tools
{
    public static class ExceptionExtensions
    {
        public static string GenerateInfo(this Exception err, bool stacktrace = true)
        {
            if (err == null) return "";

            var result = "";
            result += "Type: {0}\n".F(err.GetType().Name);
            result += "Message: {0}\n".F(err.Message);

            var inner = err.InnerException;
            while (inner != null)
            {
                result += "Inner-Type: {0}\n".F(inner.GetType().Name);
                result += "Inner-Message: {0}\n".F(inner.Message);
                inner = inner.InnerException;
            }

            if (stacktrace)
            {
                result += "Stracktrace:\n";
                if (err.StackTrace != null)
                {
                    result += err.StackTrace;
                }
            }
            return result;
        }

        public static string GenerateInfoOneLine(this Exception err, bool stacktrace = true)
        {
            if (err == null) return "";
            
            var result = "";
            result += "Type: {0} | ".F(err.GetType().Name);
            result += "Message: {0} | ".F(err.Message);

            var inner = err.InnerException;
            while (inner != null)
            {
                result += "Inner-Type: {0} | ".F(inner.GetType().Name);
                result += "Inner-Message: {0} | ".F(inner.Message);
                inner = inner.InnerException;
            }

            if (stacktrace)
            {
                result += "Stracktrace: ";
                if (err.StackTrace != null)
                {
                    result += Convert.ToBase64String(Encoding.UTF8.GetBytes(err.StackTrace));
                }
            }
            return result;
        }
    }
}
