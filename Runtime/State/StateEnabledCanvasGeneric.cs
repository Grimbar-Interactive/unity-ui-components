#if !UNITY_2019
using GI.UnityToolkit.State;
using GI.UnityToolkit.State.Components;
using UnityEngine;
#endif

namespace GI.UnityToolkit.Components.UI
{
#if !UNITY_2019
    [RequireComponent(typeof(Canvas))]
    public abstract class StateEnabledCanvasGeneric<TState> : StateEnabledComponent<TState> where TState : StateBase
    {
        protected Canvas Canvas { get; private set; }
        
        protected new void Awake()
        {
            base.Awake();
            Canvas = GetComponent<Canvas>();
        }

        protected override void SetEnabled(bool enable)
        {
            Canvas.enabled = enable;
        }

        protected override bool IsEnabled => Canvas.enabled;
    }
#endif
}