using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace FourPics
{
    public class Loader : MonoBehaviour
    {
        [SerializeField]
        private string mainSceneName;

        [SerializeField]
        private float mainSceneLoadThreshold = 0.9f;

        private IEnumerator Start()
        {
            Assert.IsFalse(string.IsNullOrEmpty(mainSceneName));

            // Asynchronously load the main scene
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(mainSceneName);

            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                // Wait till the main scene is loaded to the desired value
                if (asyncOperation.progress >= mainSceneLoadThreshold)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
