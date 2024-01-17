using System;
using System.Collections.Generic;
using System.Linq;
using GI.UnityToolkit.State;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using NaughtyAttributes;
#endif

namespace GI.UnityToolkit.Components.UI
{
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
            Canvas.enabled = enableWhen switch
            {
                StateComparison.AnyAreActive => activeStates.Any(newActiveStates.IsActive),
                StateComparison.AllAreActive => activeStates.All(newActiveStates.IsActive),
                StateComparison.NoneAreActive => !activeStates.Any(newActiveStates.IsActive),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private bool StateManagerIsSet => multiStateManager != null;
    }
}