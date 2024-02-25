#if !UNITY_2019
using System;
using System.Collections.Generic;
using System.Linq;
using GI.UnityToolkit.State;
using UnityEngine;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using GI.UnityToolkit.Attributes;
#endif

namespace GI.UnityToolkit.Components.UI
{
#if !UNITY_2019
    public class MultiStateEnabledCanvasGeneric<TState> : CanvasComponent, IMultiStateListener<TState> where TState : StateBase
    {
        [SerializeField] private MultiStateManagerBase<TState> multiStateManager;

#if ODIN_INSPECTOR
        private List<TState> StateOptions => multiStateManager != null ? multiStateManager.States : new List<TState>();

        [SerializeField, ShowIf(nameof(StateManagerIsSet)), ValueDropdown(nameof(StateOptions)), Space(4)]
#else
        [SerializeField, ShowIf(nameof(StateManagerIsSet)), Space(4)]
#endif
        protected List<TState> activeStates;
        
        private enum StateComparison
        {
            AnyAreActive = 0,
            AllAreActive = 1,
            NoneAreActive = 2
        }

        [SerializeField] private StateComparison enableWhen = StateComparison.AnyAreActive;
        
        private void OnEnable()
        {
            multiStateManager.RegisterListener(this);
            OnStateChanged(multiStateManager.PreviousActiveStates, multiStateManager.CurrentActiveStates);
        }

        private void OnDisable()
        {
            multiStateManager.UnregisterListener(this);
        }

        public void OnStateChanged(MultiStateValue<TState> previousActiveStates, MultiStateValue<TState> newActiveStates)
        {
            switch (enableWhen)
            {
                case StateComparison.AnyAreActive:
                    Canvas.enabled = activeStates.Any(newActiveStates.IsActive);
                    break;
                case StateComparison.AllAreActive:
                    Canvas.enabled = activeStates.All(newActiveStates.IsActive);
                    break;
                case StateComparison.NoneAreActive:
                    Canvas.enabled = !activeStates.Any(newActiveStates.IsActive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private bool StateManagerIsSet => multiStateManager != null;
    }
#endif
}