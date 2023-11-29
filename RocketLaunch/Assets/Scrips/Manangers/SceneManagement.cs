
using UnityEngine.SceneManagement;


namespace GameSceneManagement 
{
    public enum GameScene { MainMenu,TestLevel, Level1 }
    public static class SceneManagement
    {
        private static GameScene currentGameScene = GameScene.Level1;
        public static void LoadScene(GameScene gameScene)
        {
            currentGameScene = gameScene;
            SceneManager.LoadScene((int)currentGameScene);
        }

        public static void ReloadCurrentScene()
        {
            LoadScene(currentGameScene);
        }        
    }
}

