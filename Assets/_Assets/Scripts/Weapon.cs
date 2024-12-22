using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        Debug.Log("Collided with " + other.gameObject.name);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rotator>().shouldRotate = false;
    }
}
