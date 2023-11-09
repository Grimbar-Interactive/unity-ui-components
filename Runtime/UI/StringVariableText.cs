using System;
using GI.UnityToolkit.Variables;
using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    public class StringVariableText : TextComponent
    {
        [SerializeField] private StringVariable stringVariable;

        [SerializeField, Tooltip("Displays with as-is if left empty, otherwise uses string.Format().")]
        private string format;

        private void OnEnable()
        {
            stringVariable.AddListener(OnUpdated);
            OnUpdated();
        }

        private void OnDisable()
        {
            stringVariable.RemoveListener(OnUpdated);
        }

        private void OnUpdated()
        {
            try
            {
                Text.text = string.IsNullOrEmpty(format)
                    ? stringVariable.Value
                    : string.Format(format, stringVariable.Value);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
        }
    }
}