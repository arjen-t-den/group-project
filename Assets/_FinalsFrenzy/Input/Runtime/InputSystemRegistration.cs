using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy.Input
{
    /// <summary>
    /// Handles registration of custom input processors.
    /// </summary>
    internal static class InputSystemRegistration
    {
        /// <summary>
        /// Registers all custom Input System processors.
        /// </summary>
        internal static void Register()
        {
            InputSystem.RegisterProcessor<UnscaledDeltaTimeProcessor>();
        }

        /// <summary>
        /// Runtime hook that registers processors when the game starts.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterRuntime() => Register();
    }
}
