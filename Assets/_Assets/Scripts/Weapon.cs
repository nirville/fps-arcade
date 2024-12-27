using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        var body = gameObject.GetComponent<Rigidbody>();
        body.isKinematic = true;
        gameObject.GetComponent<Rotator>().shouldRotate = false;

        if(other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<EnemyController>().Die();
            body.isKinematic = false;
        }
    }
}
