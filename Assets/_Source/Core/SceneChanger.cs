using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}