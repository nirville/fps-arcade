using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10f;

    public Vector3 rotationAxis = Vector3.up;

    public bool shouldRotate = false;


    void Update()
    {
        if(shouldRotate)
        {
            transform.Rotate(rotationAxis, speed * Time.deltaTime);
        }
    }
}
