using GI.UnityToolkit.Variables;
using UnityEngine;

namespace GI.UnityToolkit.Components.UI.Variables
{
    /// <summary>
    /// Handles syncing a bool variable with a Toggle component (Unity UI).
    /// </summary>
    public class BoolVariableToggle : ToggleComponent
    {
        [SerializeField] private BoolVariable boolVariable;

        private new void OnEnable()
        {
            base.OnEnable();
            Toggle.isOn = boolVariable.Value;
            boolVariable.AddListener(OnBoolVariableChanged);
        }

        private new void OnDisable()
        {
            base.OnDisable();
            boolVariable.RemoveListener(OnBoolVariableChanged);
        }

        protected override void OnValueChanged(bool value)
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