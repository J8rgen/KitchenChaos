using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader { //not attached to any object, no instances

    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene
    }

    private static Scene targetScene; // The scene to be loaded


    // Loader.Load(Loader.Scene.XXX); calls loading scene, where LoaderCallback is called
    public static void Load(Scene targetScene) {

        Loader.targetScene = targetScene; // Set the target scene to the one passed as a parameter (right one)

        SceneManager.LoadScene(Scene.LoadingScene.ToString()); // Load the loading scene

    }

    // the first update in Loading scene object LoaderCallback calls the target scene through LoaderCallback:
    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString()); // Load the target scene after the loading scene
    }

}
