using UnityEngine;
using UnityEngine.Events;

namespace GI.UnityToolkit.Components
{
    public class ApplicationPauseListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPaused;
        [SerializeField] private UnityEvent onResumed;

        private void OnApplicationPause(bool paused)
        {
            if (paused)
            {
                onPaused?.Invoke();
            }
            else
            {
                onResumed?.Invoke();
            }
        }
    }
}