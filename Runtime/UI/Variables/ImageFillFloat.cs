using GI.UnityToolkit.Utilities;
using GI.UnityToolkit.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace GI.UnityToolkit.Components.UI.Variables
{
    [RequireComponent(typeof(Image))]
    public class ImageFill : ImageComponent
    {
        [SerializeField] private FloatVariable floatVariable;
        
        protected new void Awake()
        {
            base.Awake();
            if (Image.type == Image.Type.Filled) return;
            this.LogWarning("Object has ImageFill component but is not set to Filled type! Setting now...");
            Image.type = Image.Type.Filled;
        }

        private void OnEnable()
        {
            floatVariable.AddListener(OnChanged);
        }

        private void OnDisable()
        {
            floatVariable.RemoveListener(OnChanged);
        }

        private void OnChanged()
        {
            Image.fillAmount = floatVariable;
        }
    }
}
