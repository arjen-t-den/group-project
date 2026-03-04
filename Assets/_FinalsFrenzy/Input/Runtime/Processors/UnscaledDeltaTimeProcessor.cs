using UnityEngine;
using UnityEngine.InputSystem;

namespace Group8.FinalsFrenzy.Input
{
    /// <summary>
    /// Scales a <see cref="Vector2"/> input value by <see cref="Time.unscaledDeltaTime"/>.
    /// </summary>
    public class UnscaledDeltaTimeProcessor : InputProcessor<Vector2>
    {
        /// <summary>
        /// Scales the input <paramref name="value"/> by <see cref="Time.unscaledDeltaTime"/>.
        /// </summary>
        /// <param name="value">Input vector.</param>
        /// <returns>The time-scaled vector.</returns>
        public override Vector2 Process(Vector2 value, InputControl _) => value * Time.unscaledDeltaTime;
    }
}