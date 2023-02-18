using UnityEditor;
using UnityEditor.Build;

namespace ProjectEssentials.Trace.Editor
{
    /// <summary>
    /// Contient les onglets du menu Trace dans la barre d'outils Unity
    /// </summary>
    public sealed class TraceMenuItems : IActiveBuildTargetChanged
    {
        #region Constantes

        public const string ENABLE_LOGS_PATH = "Trace/Enable Logs";

        #endregion

        #region Propriétés

        /// <summary>
        /// Utilisé par le IActiveBuildTargetChanged
        /// </summary>
        public int callbackOrder { get; }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Appelée quand la plateforme ciblée change
        /// </summary>
        /// <param name="previousTarget">L'ancienne plateforme</param>
        /// <param name="newTarget">La nouvelle plateforme</param>
        /// 
        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            bool enableLogs = Menu.GetChecked(ENABLE_LOGS_PATH);

            if (enableLogs)
            {
                // Si on doit ajouter ENABLE_LOGS,
                // on récupère la plateforme cible et on lui ajoute le symbole

                NamedBuildTarget targetPlatform = GetCurrentBuildTarget(EditorUserBuildSettings.activeBuildTarget);

                if (targetPlatform != NamedBuildTarget.Unknown)
                {
                    AddSymbolToBuildTarget(targetPlatform, Trace.ENABLE_LOGS_SYMBOL);
                }
            }
            else
            {
                // Si on doit ajouter ENABLE_LOGS,
                // on récupère la plateforme cible et on lui retire le symbole

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
        /// Permet d'activer ou non les logs dans l'éditeur et les builds
        /// </summary>
        [MenuItem(ENABLE_LOGS_PATH)]
        private static void EnableLogsMenuBtn()
        {
            bool enableLogs = !Menu.GetChecked(ENABLE_LOGS_PATH);
            Menu.SetChecked(ENABLE_LOGS_PATH, enableLogs);

            if (enableLogs)
            {
                // Si on doit ajouter ENABLE_LOGS,
                // on récupère la plateforme cible et on lui ajoute le symbole

                NamedBuildTarget targetPlatform = GetCurrentBuildTarget(EditorUserBuildSettings.activeBuildTarget);

                if (targetPlatform != NamedBuildTarget.Unknown)
                {
                    AddSymbolToBuildTarget(targetPlatform, Trace.ENABLE_LOGS_SYMBOL);
                }
            }
            else
            {
                // Si on doit ajouter ENABLE_LOGS,
                // on récupère la plateforme cible et on lui retire le symbole

                NamedBuildTarget targetPlatform = GetCurrentBuildTarget(EditorUserBuildSettings.activeBuildTarget);

                if (targetPlatform != NamedBuildTarget.Unknown)
                {
                    RemoveSymbolFromBuildTarget(targetPlatform, Trace.ENABLE_LOGS_SYMBOL);
                }
            }
        }

        /// <summary>
        /// Retourne la plateforme choisie dans les Build Settings
        /// </summary>
        /// <param name="currentBuildTarget">La plateforme active</param>
        /// <returns>Le nom de la plateforme active</returns>
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
        /// Ajoute le symbole à la liste des symboles de compilation de la plateforme active
        /// </summary>
        /// <param name="targetPlatform">La plateforme ciblée</param>
        /// <param name="scriptingDefineSymbol">Le symbole de compilation de scripts</param>
        private static void AddSymbolToBuildTarget(NamedBuildTarget targetPlatform, string scriptingDefineSymbol)
        {
            // Force l'éditeur à recompiler
            // si la recompilation auto est désactivée

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
        /// Ajoute le symbole à la liste des symboles de compilation de la plateforme active
        /// </summary>
        /// <param name="targetPlatform">La plateforme ciblée</param>
        /// <param name="scriptingDefineSymbol">Le symbole de compilation de scripts</param>
        private static void RemoveSymbolFromBuildTarget(NamedBuildTarget targetPlatform, string scriptingDefineSymbol)
        {
            // Force l'éditeur à recompiler
            // si la recompilation auto est désactivée

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