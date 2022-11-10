using System.Collections.Generic;
using GI.UnityToolkit.State;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace GI.UnityToolkit.Components.UI
{
    /// <summary>
    /// Enables and disables an attached Canvas based on the state.
    /// </summary>
    [AddComponentMenu("Grimbar Interactive/UI/State Canvas")]
    public class StateCanvas : CanvasComponent, IStateListener<State.State>
    {
        [SerializeField] private StateManager stateManager;
        
#if ODIN_INSPECTOR
        private bool StateManagerIsNull => stateManager == null;
        private List<State.State> StateOptions => stateManager != null ? stateManager.States : new List<State.State>();

        [SerializeField, HideIf(nameof(StateManagerIsNull)), ValueDropdown(nameof(StateOptions)), Space(4)]
#else
        [SerializeField, Space(4)]
#endif
        protected List<State.State> activeStates;

        private void OnEnable()
        {
            stateManager.RegisterListener(this);
            OnStateChanged(stateManager.PreviousState, stateManager.CurrentState);
        }

        private void OnDisable()
        {
            stateManager.UnregisterListener(this);
        }

        public void OnStateChanged(State.State previousState, State.State newState)
        {
            Canvas.enabled = activeStates.Contains(newState);
        }
    }
}