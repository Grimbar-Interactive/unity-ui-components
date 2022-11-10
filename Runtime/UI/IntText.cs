using System;
using System.Globalization;
using GI.UnityToolkit.Variables;
using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    public class IntText : TextComponent
    {
        [SerializeField] private IntVariable intVariable;
        [SerializeField] private bool isTimeSpan = false;

        [SerializeField, Tooltip("Displays with .ToString() if left empty, otherwise uses TimeSpan.toString() or string.Format().")]
        private string format;

        private void OnEnable()
        {
            intVariable.AddListener(OnUpdated);
            OnUpdated();
        }

        private void OnDisable()
        {
            intVariable.RemoveListener(OnUpdated);
        }

        private void OnUpdated()
        {
            try
            {
                if (string.IsNullOrEmpty(format))
                {
                    Text.text = intVariable.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Text.text = isTimeSpan
                        ? TimeSpan.FromSeconds(intVariable).ToString(format)
                        : string.Format(format, intVariable.Value);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
        }
    }
}