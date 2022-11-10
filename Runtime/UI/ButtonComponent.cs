using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Components.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonComponent : MonoBehaviour
    {
        protected Button Button { get; private set; }

        protected void Awake()
        {
            Button = GetComponent<Button>();
        }
    }
}