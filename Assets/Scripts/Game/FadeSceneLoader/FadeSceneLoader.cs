using System;
using Plugins.Fade.Scripts;
using SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.FadeSceneLoader
{
    public class FadeSceneLoader : MonoBehaviour
    { 
        [SerializeField]
        private Fade fade = default;

        [SerializeField]
        private NowLoadingView nowLoadingView = default;

        [SerializeField]
        private float fadeTime = 1f;

        [SerializeField]
        private float fadeInterval = 1f;

        public bool IsFadeOutCompleted { get; private set; }
            = false;

        private ZenjectSceneLoader _zenjectSceneLoader;

        [Inject]
        private void Construct(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
        {
            DoLoadingFadeOut();
        }
        
        public void CurrentSceneLoad()
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            var sceneName = ((SceneName)currentScene).ToString();

            DoFadeInSceneLoad(sceneName);
        }

        public void JumpSceneLoad(SceneName sceneName)
        {
            var scene = sceneName.ToString();

            DoFadeInSceneLoad(scene);
        }

        public void NextSceneLoad()
        {
            var currentSceneName = SceneManager.GetActiveScene().buildIndex;

            var enumMax = Enum.GetValues(typeof(SceneName)).Length;
            var nextIndex = (int)(currentSceneName + 1) % enumMax;

            var nextSceneName = (SceneName)nextIndex;
            var scene = nextSceneName.ToString();

            DoFadeInSceneLoad(scene);
        }

        private void DoFadeInSceneLoad(string sceneName)
        {
            fade.FadeIn(fadeTime, () =>
            {
                _zenjectSceneLoader.LoadScene(sceneName);
            });
        }

        public void DoLoadingFadeOut()
        {
            IsFadeOutCompleted = false;

            nowLoadingView.DOAnimation(fadeInterval, DoFadeOut);
        }

        private void DoFadeOut()
        {
            fade.FadeOut(fadeTime, () =>
            {
                IsFadeOutCompleted = true;
            });
        }
    }
}
