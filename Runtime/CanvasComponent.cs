using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class CanvasComponent : MonoBehaviour
    {
        protected Canvas Canvas { get; private set; }
        
        protected void Awake()
        {
            Canvas = GetComponent<Canvas>();
        }
    }
}