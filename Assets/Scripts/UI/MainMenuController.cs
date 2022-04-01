using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void QuitGame() => Application.Quit();
        
        public void StartGame() => SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

        public void BackToMainMenu() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}