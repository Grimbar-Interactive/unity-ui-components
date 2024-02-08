using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Components.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonComponent : MonoBehaviour
    {
        protected Button Button { get; private set; }

        protected void Awake()
        {
            Button = GetComponent<Button>();
        }

        protected void OnEnable()
        {
            Button.onClick.AddListener(OnClicked);
        }

        protected void OnDisable()
        {
            Button.onClick.RemoveListener(OnClicked);
        }

        protected abstract void OnClicked();
    }
}