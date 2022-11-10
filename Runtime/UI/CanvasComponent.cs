using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class CanvasComponent : MonoBehaviour
    {
        protected Canvas Canvas { get; private set; }
        
        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
        }
    }
}