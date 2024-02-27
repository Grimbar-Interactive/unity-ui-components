using TMPro;
using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class TextComponent : MonoBehaviour
    {
        protected TMP_Text Text { get; private set; }

        protected void Awake()
        {
            Text = GetComponent<TMP_Text>();
        }
    }
}
