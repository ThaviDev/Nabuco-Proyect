using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    // VARIABLES
    Scene myScene;
    bool changeScene;
    int currentScene;

    // VARIABLES DE EXPLORACION
    // La escena de exploracion anterior, sirve para saber que escena de exploracion cargar despues de una batalla
    [SerializeField]
    string lastExplorationScene;
    [SerializeField]
    Color backgrndExplorColor;

    // CODIGOS
    Scene_Manager sceneMan;
    Camera myCamera;
    Cam_Manager camManager;
    PauseManager pauseManager;
    MyDialogueManager myDialogueMan;

    MenuMotor menuMotor;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        // Encontrar todos los objetos de la base del juego
        sceneMan = FindObjectOfType<Scene_Manager>();
        myCamera = Camera.main;
        camManager = Camera.main.GetComponent<Cam_Manager>();
        pauseManager = FindObjectOfType<PauseManager>();
        myDialogueMan = FindObjectOfType<MyDialogueManager>();

        // Destruye Game Base: la escena con todos los objetos y códigos indestructibles para no molestar con una escena demás que no tiene nada
        SceneManager.UnloadSceneAsync("Game Base");

        // Obtener la escena activa en ese momento
        myScene = SceneManager.GetActiveScene();
        print("El nombre de esta escena actual es: " + myScene.name);

        // Si la escena activa es game base
        if (myScene.name == "Game Base")
        {
            // Cargar Main Menu, Destruir Game Base y volver a checar la escena actual
            SceneManager.LoadSceneAsync("Main Menu");
            SceneManager.UnloadSceneAsync("Game Base");
            // Iniciar cuna corrutina para checar la escena unos segundos despues de que se termine de cargar el pinche unity
            StartCoroutine("WaitForLoadScene_InStartGame");
        }

        CheckScene();
    }

    // Este enumerador existe solamente para evitar un glich que tiene que ver con el cargar el menu principal del juego
    // cuando no hay ninguna escena en ese momento
    IEnumerator WaitForLoadScene_InStartGame()
    {
        yield return new WaitForSeconds(0.250f);
        CheckScene();
    }

    // Esta función sirve para estar checando que escena está cargada, y una vez que sabe eso, que códigos necesita encontrar, además de que otras funciones puede acceder
    void CheckScene()
    {
        // Obtener la escena activa en ese momento
        myScene = SceneManager.GetActiveScene();
        print("El nombre de esta escena actual es: " + myScene.name);
        switch (myScene.name)
        {
            case "Main Menu":
                menuMotor = FindObjectOfType<MenuMotor>();
                menuMotor.cam = myCamera;
                camManager.SceneNotExplor(8);
                myCamera.backgroundColor = Color.black;
                break;
            case "L1 R1":
                camManager.SceneIsExplor();
                lastExplorationScene = "L1 R1";
                myCamera.backgroundColor = backgrndExplorColor;
                break;
            case "Battle Scene":
                camManager.SceneNotExplor(5);
                break;
        }
    }
    // Envía codigo a Scene Manager para cambiar la escena
    public void Output_SceneManager_ChangeScene(string sceneName)
    {
        // Pasar el nombre de la escena que cambiará y que se pueda cambiar
        /*
        sceneMan.sceneID = sceneName;
        sceneMan.changeScene = true;
        */
        sceneMan.ChangeSceneInput(sceneName);
    }

    public void Input_SceneLoaded()
    {
        print("Gman Sabe que estoy en nueva escena");
        // Se cargo una nueva escena, hay que indicar nuevas instrucciones
        CheckScene();
        //myCamera.GetComponent<CameraManager_OW>().canFollowPlayer = false;
        //myCamera.gameObject.transform.position = new Vector3(0f, 0f, -10f);
        // Checar en que escena estamos
    }

    public void Input_SceneBattle(bool isVictory)
    {
        // Aquí va a estar la lógica de que hacer cuando se gane o se pierda

        // Cargar la escena de exploracion
        Output_SceneManager_ChangeScene(lastExplorationScene);
    }


}
