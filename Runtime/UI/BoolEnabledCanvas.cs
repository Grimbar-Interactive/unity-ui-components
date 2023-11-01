using GI.UnityToolkit.Variables;
using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    /// <summary>
    /// Enables and disables a canvas based on the value of a BoolVariable.
    /// </summary>
    public class BoolEnabledCanvas : CanvasComponent
    {
        [SerializeField] private BoolVariable boolVariable;

        private void OnEnable()
        {
            boolVariable.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            boolVariable.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged()
        {
            Canvas.enabled = boolVariable.Value;
        }
    }
}
