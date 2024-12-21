using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerBody;
    
    [SerializeField] float aimSensitivity = 10f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;

    CharacterController characterController;

    float rotX = 0;
    float rotY = 0;

    Vector3 velocity;
    bool isGrounded;

     void Start() 
     {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;    
    }

    void Update() 
    {

         isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        rotY += Input.GetAxis("Mouse X") * aimSensitivity;
        rotX += Input.GetAxis("Mouse Y") * -1f * aimSensitivity;

        //Debug.Log(Mathf.Abs(rotX));
        if(Mathf.Abs(rotX) < 45)
            playerBody.localEulerAngles = new Vector3(rotX, rotY, 0);
        else
            rotX = Mathf.Sign(rotX) * 44.9f; // Reset rotX to just below 45


         // WASD movement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        var dir = playerBody.forward;
        dir.y = 0;
        Vector3 move = playerBody.right * moveX + dir * moveZ;
        
        //transform.Translate(move);

        characterController.Move(move);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    
}
