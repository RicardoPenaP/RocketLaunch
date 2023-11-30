
using UnityEngine.SceneManagement;


namespace GameSceneManagement 
{
    public enum GameScene { MainMenu,TestLevel, Level1, Level2, Level3, Level4, level5}
    public static class SceneManagement
    {
        private static GameScene currentGameScene = GameScene.MainMenu;
        public static void LoadScene(GameScene gameScene)
        {
            currentGameScene = gameScene;
            SceneManager.LoadScene((int)currentGameScene);
        }

        public static void ReloadCurrentScene()
        {
            LoadScene(currentGameScene);
        }     
        
        public static GameScene GetCurrentScene()
        {
            return currentGameScene;
        }
    }
}

