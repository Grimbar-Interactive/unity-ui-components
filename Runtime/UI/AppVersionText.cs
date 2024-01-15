using UnityEngine;

namespace GI.UnityToolkit.Components.UI
{
    /// <summary>
    /// Automatically sets the text to the application version at runtime.
    /// </summary>
    public class AppVersionText : TextComponent
    {
        [SerializeField, Tooltip("(Optional) Used to add text around the application number. Must contain \"{0}\" where the application number should appear.")]
        private string formattedText;
        
        private void OnEnable()
        {
            if (string.IsNullOrEmpty(formattedText) || formattedText.Contains("{0}") == false)
            {
                Text.text = Application.version;
            }
            else
            {
                Text.text = string.Format(formattedText, Application.version);
            }
        }
    }
}