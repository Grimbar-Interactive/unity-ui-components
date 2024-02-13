using System.Collections;
using GI.UnityToolkit.Utilities;
using GI.UnityToolkit.Variables;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using NaughtyAttributes;
#endif

namespace GI.UnityToolkit.Components.UI.Variables
{
    /// <summary>
    /// Enables and disables a canvas based on the value of a BoolVariable.
    /// </summary>
    public class BoolEnabledCanvas : CanvasComponent
    {
        [SerializeField] private BoolVariable boolVariable;
        
#if ODIN_INSPECTOR
        [SerializeField, ShowIf(nameof(BoolVariableIsSet)), Space(4)]
#else
        [SerializeField, HorizontalLine(), ShowIf(nameof(BoolVariableIsSet)), Space(4)]
#endif    
        private bool delayDisable = false;

#if ODIN_INSPECTOR
        [SerializeField, ShowIf("@this.BoolVariableIsSett && this.delayDisable"), Space(4)]
#else
        [SerializeField, ShowIf(EConditionOperator.And,nameof(BoolVariableIsSet), nameof(delayDisable)), Space(4)]
#endif
        protected float delayDuration = 1f;

        private Coroutine _delayRoutine;

        private void OnEnable()
        {
            boolVariable.AddListener(OnValueChanged);
            OnValueChanged();
        }

        private void OnDisable()
        {
            boolVariable.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged()
        {
            if (_delayRoutine != null)
            {
                StopCoroutine(_delayRoutine);
                _delayRoutine = null;
            }

            if (boolVariable.Value)
            {
                Canvas.enabled = true;
                return;
            }

            if (!Canvas.enabled) return;

            if (delayDisable == false || delayDuration <= 0)
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
        
        private bool BoolVariableIsSet => boolVariable != null;
    }
}
