using UnityEditor;
using UnityEditor.Build;

namespace ProjectEssentials.Trace.Editor
{
    /// <summary>
    /// Contains all the MenuItems under the "Trace" option in the toolbar
    /// </summary>
    public sealed class TraceMenuItems : IActiveBuildTargetChanged
    {
        #region Constantes

        public const string ENABLE_LOGS_PATH = "Trace/Enable Logs";

        #endregion

        #region Propriétés

        /// <summary>
        /// Used by the IActiveBuildTargetChanged interface
        /// </summary>
        public int callbackOrder { get; }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Called when the targeted platform changes
        /// </summary>
        /// <param name="previousTarget">The old platform</param>
        /// <param name="newTarget">The new platform</param>
        /// 
        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            bool enableLogs = Menu.GetChecked(ENABLE_LOGS_PATH);

            if (enableLogs)
            {
                // If we need to add ENABLE_LOGS_IN_BUILD,
                // we first get the target platform, then we add the symbol
                // in its scripting defines list

                NamedBuildTarget targetPlatform = GetCurrentBuildTarget(EditorUserBuildSettings.activeBuildTarget);

                if (targetPlatform != NamedBuildTarget.Unknown)
                {
                    AddSymbolToBuildTarget(targetPlatform, Trace.ENABLE_LOGS_SYMBOL);
                }
            }
            else
            {
                // If we need to remove ENABLE_LOGS_IN_BUILD,
                // we first get the target platform, then we remove the symbol
                // in its scripting defines list

                NamedBuildTarget targetPlatform = GetCurrentBuildTarget(EditorUserBuildSettings.activeBuildTarget);

                if (targetPlatform != NamedBuildTarget.Unknown)
                {
                    RemoveSymbolFromBuildTarget(targetPlatform, Trace.ENABLE_LOGS_SYMBOL);
                }
            }
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Enables or disables logs in a standalone build (always logs in the editor)
        /// </summary>
        [MenuItem(ENABLE_LOGS_PATH)]
        private static void EnableLogsMenuBtn()
        {
            bool enableLogs = !Menu.GetChecked(ENABLE_LOGS_PATH);
            Menu.SetChecked(ENABLE_LOGS_PATH, enableLogs);

            if (enableLogs)
            {
                // If we need to add ENABLE_LOGS_IN_BUILD,
                // we first get the target platform, then we add the symbol
                // in its scripting defines list

                NamedBuildTarget targetPlatform = GetCurrentBuildTarget(EditorUserBuildSettings.activeBuildTarget);

                if (targetPlatform != NamedBuildTarget.Unknown)
                {
                    AddSymbolToBuildTarget(targetPlatform, Trace.ENABLE_LOGS_SYMBOL);
                }
            }
            else
            {
                // If we need to remove ENABLE_LOGS_IN_BUILD,
                // we first get the target platform, then we remove the symbol
                // in its scripting defines list

                NamedBuildTarget targetPlatform = GetCurrentBuildTarget(EditorUserBuildSettings.activeBuildTarget);

                if (targetPlatform != NamedBuildTarget.Unknown)
                {
                    RemoveSymbolFromBuildTarget(targetPlatform, Trace.ENABLE_LOGS_SYMBOL);
                }
            }
        }

        /// <summary>
        /// Returns the target platform set in the Build Settings
        /// </summary>
        /// <param name="currentBuildTarget">The active platform</param>
        /// <returns>The name of the active platform</returns>
        private static NamedBuildTarget GetCurrentBuildTarget(BuildTarget currentBuildTarget)
        {
            return currentBuildTarget switch
            {
                BuildTarget.StandaloneOSX or
                BuildTarget.StandaloneWindows64 or
                BuildTarget.StandaloneLinux64 => NamedBuildTarget.Standalone,
                BuildTarget.iOS => NamedBuildTarget.iOS,
                BuildTarget.Android => NamedBuildTarget.Android,
                BuildTarget.WebGL => NamedBuildTarget.WebGL,
                _ => NamedBuildTarget.Unknown,
            };
        }

        /// <summary>
        /// Adds the symbol to the active platform's scrpting define symbols list
        /// </summary>
        /// <param name="targetPlatform">The targeted platform</param>
        /// <param name="scriptingDefineSymbol">The scripting define to add</param>
        private static void AddSymbolToBuildTarget(NamedBuildTarget targetPlatform, string scriptingDefineSymbol)
        {
            // Forces the editor to recompile
            // in case automatic domain reload is disabled

            EditorApplication.UnlockReloadAssemblies();

            PlayerSettings.GetScriptingDefineSymbols(targetPlatform, out string[] defines);

            string[] newDefines = new string[defines.Length + 1];

            for (int i = 0; i < defines.Length; i++)
            {
                newDefines[i] = defines[i];
            }

            newDefines[^1] = scriptingDefineSymbol;
            PlayerSettings.SetScriptingDefineSymbols(targetPlatform, newDefines);
        }

        /// <summary>
        /// Removes the symbol from the active platform's scrpting define symbols list
        /// </summary>
        /// <param name="targetPlatform">The targeted platform</param>
        /// <param name="scriptingDefineSymbol">The scripting define to remove</param>
        private static void RemoveSymbolFromBuildTarget(NamedBuildTarget targetPlatform, string scriptingDefineSymbol)
        {
            // Forces the editor to recompile
            // in case automatic domain reload is disabled

            EditorApplication.UnlockReloadAssemblies();

            PlayerSettings.GetScriptingDefineSymbols(targetPlatform, out string[] defines);

            if (defines.Length > 0)
            {
                string[] newDefines = new string[defines.Length - 1];
                int count = 0;

                for (int i = 0; i < defines.Length; i++)
                {
                    if (defines[i] != scriptingDefineSymbol)
                    {
                        newDefines[count] = defines[i];
                        count++;
                    }
                }

                PlayerSettings.SetScriptingDefineSymbols(targetPlatform, newDefines);
            }
        }

        #endregion
    }
}