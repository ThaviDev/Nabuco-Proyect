using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cam_Manager : MonoBehaviour
{
    private float speedFollow = 5;
    private float camExplorSize = 8;
    private Transform target;
    // Determina si pueda seguír al jugador en Exploracion
    public bool canFollowPlayer;
    private Camera thisCam;

    void Start(){
        DontDestroyOnLoad(this.gameObject);
        thisCam = this.gameObject.GetComponent<Camera>();
        /*
        try
        {
            target = FindObjectOfType<Player_Exploration>().transform;
        }
        catch {
            //target = FindObjectOfType<PlayerMotor_OW>().transform;
            print("No hay jugador");
            canFollowPlayer = false;
        }
        // Si hay un objetivo desde el try catch entonces se puede seguír al objetivo (jugador)
        if (target != null)
        {
            canFollowPlayer = true;
        }
        */
    }
    void Update()
    {
        if (canFollowPlayer == true)
        {
            Vector3 newPosition = target.position;
            newPosition.y = target.position.y + 0.8f;
            newPosition.z = -10;
            transform.position = Vector3.Slerp(transform.position, newPosition, speedFollow * Time.deltaTime);
            //print(newPosition);
        }
    }

    public void SceneIsExplor()
    {
        // Se puede seguir al jugador
        canFollowPlayer = true;
        // Existe el objetivo, es el jugador y hay que encontrarlo
        target = FindObjectOfType<Player_Exploration>().transform;
        // Otorgar el tamaño de la camara
        thisCam.orthographicSize = camExplorSize;

    }
    public void SceneNotExplor(int camSize)
    {
        // Llevar al punto central a la camara
        transform.position = new Vector3(0f, 0f, 0f);
        // Utilizar el tamanio establecido por GMan
        thisCam = this.gameObject.GetComponent<Camera>();
        thisCam.orthographicSize = camSize;
        // No hay objetivo que seguir
        canFollowPlayer = false;
    }
}
