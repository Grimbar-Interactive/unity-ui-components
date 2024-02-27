using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Components.UI
{
    [RequireComponent(typeof(Toggle))]
    public abstract class ToggleComponent : MonoBehaviour
    {
        protected Toggle Toggle { get; private set; }

        protected void Awake()
        {
            Toggle = GetComponent<Toggle>();
        }

        protected void OnEnable()
        {
            Toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected void OnDisable()
        {
            Toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        protected abstract void OnValueChanged(bool value);
    }
}
