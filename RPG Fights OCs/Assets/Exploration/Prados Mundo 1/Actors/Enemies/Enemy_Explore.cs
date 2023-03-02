using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Explore : MonoBehaviour
{
    // Numero para identificar el enemigo
    int myEnemID;
    Collider2D myCollider;
    // Objeto que siempre sigue el enemigo
    GameObject target;
    // Personaje jugable del juego
    GameObject player;

    // La visión alrededor del personaje para identificar al jugador en escena
    [SerializeField]
    float visionRadious;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        // Encontrar el objeto "Target"
        GameObject father;
        father = transform.parent.gameObject;
        target = father.transform.Find("Target").gameObject;

        // Encontrar jugador
        player = FindObjectOfType<Player_Exploration>().gameObject;

        // Establecer Target para "AI Destination Settler"
        AIDestinationSetter destination;
        destination = gameObject.GetComponent<AIDestinationSetter>();
        destination.target = target.transform;
    }

    void Update()
    {
        // La distancia entre el jugador y el enemigo
        float distance = Vector3.Distance(player.transform.position, transform.position);
        // - Si la distancia es menor a la vision de radio, ir hacia el jugador
        if (distance <= visionRadious)
        {
            target.transform.position = player.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Encontrar Overworld Motor
        Exploration_Manager owMotor = FindObjectOfType<Exploration_Manager>();
        // Llamarlo para decirle que el jugador chocó contra un enemigo
        owMotor.EnemyEncountered(myEnemID);
        myCollider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        // Dibujar el rango de vision
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRadious);
    }
}
