using System;
using System.Globalization;
using GI.UnityToolkit.Variables;
using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    public class FloatVariableText : TextComponent
    {
        [SerializeField] private FloatVariable floatVariable;
        [SerializeField] private bool isTimeSpan = false;
        
        [SerializeField, Tooltip("Displays with .ToString() if left empty, otherwise uses string.Format().")]
        private string format;

        private void OnEnable()
        {
            floatVariable.AddListener(OnUpdated);
            OnUpdated();
        }

        private void OnDisable()
        {
            floatVariable.RemoveListener(OnUpdated);
        }

        private void OnUpdated()
        {
            try
            {
                if (string.IsNullOrEmpty(format))
                {
                    Text.text = floatVariable.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Text.text = isTimeSpan
                        ? TimeSpan.FromSeconds(floatVariable).ToString(format)
                        : string.Format(format, floatVariable.Value);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
        }
    }
}
