using System;
using System.Collections;
using GI.UnityToolkit.Utilities;
using GI.UnityToolkit.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GI.UnityToolkit.Components
{
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
#if ODIN_INSPECTOR
        [LabelText("Load Progress (Optional)"), Optional]
#endif
        [SerializeField] private FloatVariable loadProgressVariable;
        
        public float Progress =>
            _currentLoadOperation != null ? Mathf.Clamp01(_currentLoadOperation.progress + 0.1f) : 1f;

        private AsyncOperation _currentLoadOperation;

        /// <summary>
        /// Loads the scene at the given path and invokes the onComplete callback when finished.
        /// If the scene load failed the Scene passed to the onComplete callback will be null.
        /// </summary>
        /// <param name="scenePath"></param>
        /// <param name="onComplete"></param>
        /// <param name="mode"></param>
        /// <param name="setActiveAfterLoad"></param>
        public void LoadScene(string scenePath, Action<Scene?> onComplete, LoadSceneMode mode = LoadSceneMode.Additive,
            bool setActiveAfterLoad = false)
        {
            StartCoroutine(LoadSceneRoutine());

            IEnumerator LoadSceneRoutine()
            {
                AsyncOperation operation = null;
                try
                {
                    operation = SceneManager.LoadSceneAsync(scenePath, mode);
                }
                catch (Exception error)
                {
                    this.LogError($"An error occurred while loading scene \"{scenePath}\": {error}");
                }

                if (operation == null)
                {
                    onComplete?.Invoke(null);
                    yield break;
                }

                if (mode == LoadSceneMode.Single)
                {
                    _currentLoadOperation = operation;
                    SetLoadProgress(0f);
                }

                while (!operation.isDone)
                {
                    yield return null;
                    if (_currentLoadOperation != null)
                    {
                        SetLoadProgress(_currentLoadOperation.progress);
                    }
                }

                var scene = SceneManager.GetSceneByPath(scenePath);
                if (setActiveAfterLoad)
                {
                    yield return null;
                    SceneManager.SetActiveScene(scene);
                }

                if (mode == LoadSceneMode.Single)
                {
                    _currentLoadOperation = null;
                    SetLoadProgress(1f);
                }
                onComplete?.Invoke(scene);
            }

            void SetLoadProgress(float progress)
            {
                if (!loadProgressVariable) return;
                loadProgressVariable.SetValue(progress);
            }
        }

        /// <summary>
        /// Loads the scene using <see cref="LoadScene"/>LoadScene()
        /// </summary>
        /// <param name="scenePath"></param>
        /// <param name="onComplete"></param>
        /// <param name="mode"></param>
        public void LoadSceneIfNotLoaded(string scenePath, Action<Scene?> onComplete,
            LoadSceneMode mode = LoadSceneMode.Additive)
        {
            var loadedScene = SceneManager.GetSceneByPath(scenePath);
            if (loadedScene.isLoaded)
            {
                onComplete?.Invoke(loadedScene);
                return;
            }

            LoadScene(scenePath, onComplete, mode);
        }
    }
}