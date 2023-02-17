using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    //public bool changeScene;
    string sceneID;
    bool isTransition;
    AsyncOperation operation;

    public Animator transition;
    public Game_Manager Gman;
    public float transitionTime = 1f;
    void Start()
    {
        Gman = FindObjectOfType<Game_Manager>();
        DontDestroyOnLoad(this.gameObject);
        isTransition = false;
    }

    void Update()
    {
        // Si changeScene es verdadero entonces fue cambiado por GManager
        /*
        if (changeScene == true)
        {
            changeScene = false;
            // Empieza la transición
            LoadLevel();
        }
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Scene Manager Triggered Escape Key");
            //changeScene = true;
            //sceneID = "Main Menu";
        }
        */
        if (isTransition == true)
        {
            LoadAsynchronously();
        }
    }

    // ESCENA DE ACCESO POR GMANAGER --- 
    public void ChangeSceneInput(string sceneName)
    {
        if (isTransition == false)
        {
            transition.SetBool("Start", true);
            isTransition = true;
            sceneID = sceneName;
        }
    }
    bool loadJustOnce = true;
    void LoadAsynchronously()
    {
        if(loadJustOnce == true)
        {
            operation = SceneManager.LoadSceneAsync(sceneID);
            loadJustOnce = false;
        }
        if (operation.isDone == true)
        {
            print("Se terminó de cargar");
            // Animar la transición de terminar de cargar
            transition.SetBool("Start", false);
            // Desactivar el loop de la transición
            isTransition = false;
            loadJustOnce = true;
            // Llamar a Game Manager para actualizarse del cambio de escena
            Gman.Input_SceneLoaded();
        }
    }

    /*
    void TransicionToLoad()
    {
        transition.SetBool("Start", true);
        //Time.timeScale = 0f;
        //yield return new WaitForSeconds(transitionTime);
        //Time.timeScale = 1f;
        Gman.Input_SceneLoaded();
        transition.SetBool("Start", false);
        Scene_Loader.LoadScene(sceneID);
        // Load Scene
    }
    */
}
