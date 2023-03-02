using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorVisual : MonoBehaviour
{
    private ActorMotor father;
    void Start()
    {
        father = transform.parent.gameObject.GetComponent<ActorMotor>();
    }
    // Esta es una función para determinar el momento en el que en la animación se crea una acción
    public void InputAnimationAction(int AbilNum)
    {
        father.AnimationAction(AbilNum);
    }
}
