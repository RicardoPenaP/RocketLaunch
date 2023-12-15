
using UnityEngine.SceneManagement;


namespace GameSceneManagement 
{
    public enum GameScene { MainMenu,TestLevel, Level1_1, Level1_2, Level1_3, Level1_4, Level1_5,
                            Level2_1, Level2_2, Level2_3, Level2_4, Level2_5,
                            Level3_1, Level3_2, Level3_3, Level3_4, Level3_5 }
    public static class SceneManagement
    {
        private static GameScene currentGameScene = GameScene.MainMenu;
        public static void LoadScene(GameScene gameScene)
        {
            currentGameScene = gameScene;
            if (TransitionFade.Instance)
            {
                TransitionFade.Instance.FadeIn(() => SceneManager.LoadScene((int)currentGameScene));
            }
            else
            {
                SceneManager.LoadScene((int)currentGameScene);
            }
        }

        public static void ReloadCurrentScene()
        {
            if (TransitionFade.Instance)
            {
                TransitionFade.Instance.FadeIn(()=>LoadScene(currentGameScene));
            }
            else
            {
                LoadScene(currentGameScene);
            }
        }     
        
        public static GameScene GetCurrentScene()
        {
            return currentGameScene;
        }
    }
}

