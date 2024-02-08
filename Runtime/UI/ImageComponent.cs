using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Components.UI
{
    [RequireComponent(typeof(Image))]
    public abstract class ImageComponent : MonoBehaviour
    {
        protected Image Image { get; private set; }

        protected void Awake()
        {
            Image = GetComponent<Image>();
        }
    }
}
