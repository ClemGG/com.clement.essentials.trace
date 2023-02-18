using System.Diagnostics;
using UnityEngine;

namespace ProjectEssentials.Trace
{
    /// <summary>
    /// Used to write logs more easily
    /// and to discard them automatically in a build if needed
    /// </summary>
    public static class Trace
    {
        #region Constants

        public const string ENABLE_LOGS_SYMBOL = "ENABLE_LOGS_IN_BUILD";

        #endregion

        #region Public methods

        /// <summary>
        /// Identical to Debug.Log, but is only compiled in the editor
        /// or in a build with "ENABLE_LOGS_IN_BUILD" added as a scripting define symbol
        /// </summary>
        /// <param name="msg">The object to print in the console</param>
        [Conditional(ENABLE_LOGS_SYMBOL), Conditional("UNITY_EDITOR")]
        public static void Log(object msg)
        {
            UnityEngine.Debug.Log(msg);
        }

        /// <summary>
        /// Identical to Debug.Log, but is only compiled in the editor
        /// or in a build with "ENABLE_LOGS_IN_BUILD" added as a scripting define symbol
        /// </summary>
        /// <param name="msg">The object to print in the console</param>
        /// <param name="context">The UnityObject to highlight to trace the call back to its source</param>
        [Conditional(ENABLE_LOGS_SYMBOL), Conditional("UNITY_EDITOR")]
        public static void Log(object msg, Object context)
        {
            UnityEngine.Debug.Log(msg, context);
        }

        /// <summary>
        /// Identical to Debug.LogWarning, but is only compiled in the editor
        /// or in a build with "ENABLE_LOGS_IN_BUILD" added as a scripting define symbol
        /// </summary>
        /// <param name="msg">The object to print in the console</param>
        [Conditional(ENABLE_LOGS_SYMBOL), Conditional("UNITY_EDITOR")]
        public static void LogWarning(object msg)
        {
            UnityEngine.Debug.LogWarning(msg);
        }

        /// <summary>
        /// Identical to Debug.LogWarning, but is only compiled in the editor
        /// or in a build with "ENABLE_LOGS_IN_BUILD" added as a scripting define symbol
        /// </summary>
        /// <param name="msg">The object to print in the console</param>
        /// <param name="context">The UnityObject to highlight to trace the call back to its source</param>
        [Conditional(ENABLE_LOGS_SYMBOL), Conditional("UNITY_EDITOR")]
        public static void LogWarning(object msg, Object context)
        {
            UnityEngine.Debug.LogWarning(msg, context);
        }

        /// <summary>
        /// Identical to Debug.LogError, but is only compiled in the editor
        /// or in a build with "ENABLE_LOGS_IN_BUILD" added as a scripting define symbol
        /// </summary>
        /// <param name="msg">The object to print in the console</param>
        /// <param name="context">The UnityObject to highlight to trace the call back to its source</param>
        [Conditional(ENABLE_LOGS_SYMBOL), Conditional("UNITY_EDITOR")]
        public static void LogError(object msg)
        {
            UnityEngine.Debug.LogError(msg);
        }

        /// <summary>
        /// Identical to Debug.LogError, but is only compiled in the editor
        /// or in a build with "ENABLE_LOGS_IN_BUILD" added as a scripting define symbol
        /// </summary>
        /// <param name="msg">The object to print in the console</param>
        /// <param name="context">The UnityObject to highlight to trace the call back to its source</param>
        [Conditional(ENABLE_LOGS_SYMBOL), Conditional("UNITY_EDITOR")]
        public static void LogError(object msg, Object context)
        {
            UnityEngine.Debug.LogError(msg, context);
        }

        #endregion
    }
}