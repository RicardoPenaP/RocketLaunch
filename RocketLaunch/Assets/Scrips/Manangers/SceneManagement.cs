using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SceneManagement 
{
    public enum GameScene { MainMenu,TestLevel}
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
    }
}

