using UnityEngine;

namespace ProjectEssentials.Trace.Samples
{
    /// <summary>
    /// Test script to showcase the use of the Trace class.
    /// You can use it as you would with the UnityEngine.Debug class
    /// to display messages in the Console or in log files in builds.
    /// </summary>
    public class TraceTest : MonoBehaviour
    {
        #region Public methods

        void Start()
        {
            Trace.Log("Trace logs are enabled.");
        }

        #endregion
    }
}