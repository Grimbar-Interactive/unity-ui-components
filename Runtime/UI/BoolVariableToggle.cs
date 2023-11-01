using GI.UnityToolkit.Variables;
using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    /// <summary>
    /// Handles syncing a bool variable with a Toggle component (Unity UI).
    /// </summary>
    public class BoolVariableToggle : ToggleComponent
    {
        [SerializeField] private BoolVariable boolVariable;

        private void OnEnable()
        {
            Toggle.isOn = boolVariable.Value;
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);
            boolVariable.AddListener(OnBoolVariableChanged);
        }

        private void OnDisable()
        {
            Toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            boolVariable.RemoveListener(OnBoolVariableChanged);
        }

        private void OnToggleValueChanged(bool value)
        {
            if (boolVariable.Value == value) return;
            boolVariable.SetValue(value);
        }

        private void OnBoolVariableChanged()
        {
            if (Toggle.isOn == boolVariable.Value) return;
            Toggle.isOn = boolVariable.Value;
        }
    }
}