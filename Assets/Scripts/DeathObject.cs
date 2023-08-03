using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    [SerializeField] Material deathMat;

    void Start () {
        BoxCollider objectCollider = this.gameObject.GetComponent<BoxCollider>();
        if (objectCollider.isTrigger == false) {
            objectCollider.isTrigger = true;
        } 
        this.gameObject.GetComponent<Renderer>().material = deathMat;
    }
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.name == "Player") {
            hit.gameObject.GetComponent<PlayerMovement>().Death();
        }
    }
}