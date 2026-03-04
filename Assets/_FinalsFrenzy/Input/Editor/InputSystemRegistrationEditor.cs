using UnityEditor;

namespace Group8.FinalsFrenzy.Input.Editor
{
    /// <summary>
    /// Registers custom Input System processors in the editor.
    /// </summary>
    [InitializeOnLoad]
    internal static class InputSystemRegistrationEditor
    {
        /// <summary>
        /// Editor hook that registers processors when the editor loads.
        /// </summary>
        static InputSystemRegistrationEditor() => InputSystemRegistration.Register();
    }
}
