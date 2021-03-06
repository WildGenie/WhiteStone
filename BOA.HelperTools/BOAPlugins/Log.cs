using System;
using System.Runtime.CompilerServices;
using BOA.Common.Helpers;

namespace BOAPlugins
{
    public class Log
    {
        #region Public Methods
        public static void Push(string message, [CallerMemberName] string callerMemberName = null)
        {
            var filePath = Configuration.PluginDirectory + "Log.txt";

            var value = Environment.NewLine + $"{callerMemberName} -> {message}";

            FileHelper.AppendToEndOfFile(filePath, value);
        }
        #endregion
    }
}