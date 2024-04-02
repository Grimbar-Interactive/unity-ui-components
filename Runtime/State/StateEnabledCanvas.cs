using UnityEngine;

#if UNITY_2019
using System;
using System.Collections;
using GI.UnityToolkit.Utilities;
using System.Collections.Generic;
using GI.UnityToolkit.State;
using System.Linq;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using GI.UnityToolkit.Attributes;
#endif
#endif

namespace GI.UnityToolkit.Components.UI
{
    /// <summary>
    /// Enables and disables an attached Canvas based on the state.
    /// </summary>
    [AddComponentMenu("Grimbar Interactive/UI/State Enabled Canvas")]
#if !UNITY_2019
    public class StateEnabledCanvas : StateEnabledCanvasGeneric<State.State> {}
#else
    public class StateEnabledCanvas : CanvasComponent, IStateListener<State.State>
    {
        [SerializeField] private StateManager stateManager;
        
        private enum StateComparison
        {
            AnyAreActive = 0,
            NoneAreActive = 1
        }

        [SerializeField] private StateComparison enableWhen = StateComparison.AnyAreActive;
        
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

        private new void Awake()
        {
            base.Awake();
            stateManager.RegisterListener(this);
        }
        
        private void OnEnable()
        {
            OnStateChanged(stateManager.PreviousState, stateManager.CurrentState);
        }

        private void OnDestroy()
        {
            stateManager.UnregisterListener(this);
        }

        public void OnStateChanged(State.State previousState, State.State newState)
        {
            var enabled = enableWhen switch
            {
                StateComparison.AnyAreActive => activeStates.Any(s => s == newState),
                StateComparison.NoneAreActive => activeStates.All(s => s != newState),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            // Enable the Canvas if we've entered an active state.
            if (enabled)
            {
                if (_delayRoutine != null)
                {
                    StopCoroutine(_delayRoutine);
                    _delayRoutine = null;
                }
                Canvas.enabled = true;
                return;
            }

            // If we're in an inactive state but the Canvas is already disabled, there's nothing else to do.
            if (!Canvas.enabled) return;

            // If we're in an inactive state but there's no disable delay, or we're not in a delayed inactive state,
            // just disable the Canvas immediately.
            if (delayDisable == false || delayDuration <= 0 || delayedStates.Contains(newState) == false)
            {
                if (_delayRoutine != null)
                {
                    StopCoroutine(_delayRoutine);
                    _delayRoutine = null;
                }
                Canvas.enabled = false;
                return;
            }

            _delayRoutine ??= StartCoroutine(WaitBeforeDisable(delayDuration));
            return;

            IEnumerator WaitBeforeDisable(float duration)
            {
                yield return Wait.Time(duration);
                Canvas.enabled = false;
                _delayRoutine = null;
            }
        }
        
        private bool StateManagerIsSet => stateManager != null;
    }
#endif
}