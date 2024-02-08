#if UNITY_EDITOR
using UnityEditor;
#else
using UnityEngine;
#endif

namespace GI.UnityToolkit.Components.UI
{
    public class QuitButton : ButtonComponent
    {
        protected override void OnClicked()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();      
#endif
        }
    }
}