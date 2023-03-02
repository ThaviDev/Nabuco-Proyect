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
    // Esta es una funci�n para determinar el momento en el que en la animaci�n se crea una acci�n
    public void InputAnimationAction(int AbilNum)
    {
        father.AnimationAction(AbilNum);
    }
}
