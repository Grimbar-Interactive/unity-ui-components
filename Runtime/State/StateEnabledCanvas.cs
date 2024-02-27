using System.Collections;
using System.Collections.Generic;
using GI.UnityToolkit.State;
using GI.UnityToolkit.Utilities;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using GI.UnityToolkit.Attributes;
#endif

namespace GI.UnityToolkit.Components.UI
{
    /// <summary>
    /// Enables and disables an attached Canvas based on the state.
    /// </summary>
    [AddComponentMenu("Grimbar Interactive/UI/State Enabled Canvas")]
    public class StateEnabledCanvas : CanvasComponent, IStateListener<State.State>
    {
        [SerializeField] private StateManager stateManager;
        
#if ODIN_INSPECTOR
        private List<State.State> StateOptions => stateManager != null ? stateManager.States : new List<State.State>();

        [SerializeField, ShowIf(nameof(StateManagerIsSet)), ValueDropdown(nameof(StateOptions)), Space(4)]
#else
        [SerializeField, ShowIf(nameof(StateManagerIsSet)), Space(4)]
#endif
        protected List<State.State> activeStates;

#if ODIN_INSPECTOR
        [SerializeField, ShowIf(nameof(StateManagerIsSet)), Space(4)]
#else
        [SerializeField, HorizontalLine(), ShowIf(nameof(StateManagerIsSet)), Space(4)]
#endif    
        private bool delayDisable = false;
        
#if ODIN_INSPECTOR
        [SerializeField, ShowIf("@this.StateManagerIsSet && this.delayDisable"), ValueDropdown(nameof(StateOptions)), Space(4)]
#else
        [SerializeField, ShowIf(EConditionOperator.And,nameof(StateManagerIsSet), nameof(delayDisable)), Space(4)]
#endif
        protected List<State.State> delayedStates;
        
#if ODIN_INSPECTOR
        [SerializeField, ShowIf("@this.StateManagerIsSet && this.delayDisable"), Space(4)]
#else
        [SerializeField, ShowIf(EConditionOperator.And,nameof(StateManagerIsSet), nameof(delayDisable)), Space(4)]
#endif
        protected float delayDuration = 1f;

        private Coroutine _delayRoutine;

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
            if (_delayRoutine != null)
            {
                StopCoroutine(_delayRoutine);
                _delayRoutine = null;
            }
            
            // Enable the Canvas if we've entered an active state.
            if (activeStates.Contains(newState))
            {
                Canvas.enabled = true;
                return;
            }

            // If we're in an inactive state but the Canvas is already disabled, there's nothing else to do.
            if (!Canvas.enabled) return;

            // If we're in an inactive state but there's no disable delay or we're not in a delayed inactive state,
            // just disable the Canvas immediately.
            if (delayDisable == false || delayDuration <= 0 || delayedStates.Contains(newState) == false)
            {
                Canvas.enabled = false;
                return;
            }

            _delayRoutine = StartCoroutine(WaitBeforeDisable(delayDuration));

            IEnumerator WaitBeforeDisable(float duration)
            {
                yield return Wait.Time(duration);
                Canvas.enabled = false;
                _delayRoutine = null;
            }
        }
        
        private bool StateManagerIsSet => stateManager != null;
    }
}