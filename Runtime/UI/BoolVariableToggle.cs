using GI.UnityToolkit.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Components.UI
{
    /// <summary>
    /// Handles syncing a bool variable with a Toggle component (Unity UI).
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class BoolVariableToggle : MonoBehaviour
    {
        [SerializeField] private BoolVariable boolVariable;

        private Toggle _toggle;
        
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.isOn = boolVariable.Value;
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
            boolVariable.AddListener(OnBoolVariableChanged);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            boolVariable.RemoveListener(OnBoolVariableChanged);
        }

        private void OnToggleValueChanged(bool value)
        {
            if (boolVariable.Value == value) return;
            boolVariable.SetValue(value);
        }

        private void OnBoolVariableChanged()
        {
            if (_toggle.isOn == boolVariable.Value) return;
            _toggle.isOn = boolVariable.Value;
        }
    }
}