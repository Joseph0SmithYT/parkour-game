    using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public Transform cam;
    public float jumpHeight = 2f;
    public float speedMultiplier = 1.25f;
    public bool isSprinting;
    public Vector3 spawn;
    public float defaultSpeed = 12f;
    public float smoothTurnVelo = 0.1f;
    public float turnSpeed = 0.09f;
    public Vector3 wallNormal;
    public float wallDistance;
    bool wallJumpUsed = false;
    [SerializeField] private float cooldownDuration = 10f;
    private float lastUsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        spawn = new Vector3(0, 2, 0);
        Cursor.lockState = CursorLockMode.Locked;
        lastUsedTime = -cooldownDuration; // Set the initial value to allow immediate use
    }

    // Update is called once per frame
    void Update() {

        #region Gravity + Move
        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
        
        controller.Move(velocity * Time.deltaTime);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        
        if (isGrounded) {
            wallJumpUsed = false;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f) 
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelo, 0.09f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        #endregion

        #region Jump + Sprint
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2F * gravity);
        }else if (IsWallJumpAvailable() && Input.GetButtonDown("Jump")) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2F * gravity);
            wallJumpUsed = true;
        }
        

        // Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isGrounded)
            {
                speed = 15;
                isSprinting = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isGrounded)
            {
                speed = defaultSpeed;
                isSprinting = false;
            }
        }
        #endregion
        // Void Death
        if (transform.position.y <= -10f) Death();
        // Dash
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastUsedTime + cooldownDuration)
        {
            StartCoroutine(Dash());
            lastUsedTime = Time.time; // Update the last used time
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) { SceneManager.LoadScene(0); }
    }
    
    public void Death() {
        this.gameObject.transform.position = spawn;
    }
    bool IsWallJumpAvailable()
    {

        // Cast a ray from the player's position in the direction of the wall
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, wallDistance, groundMask) && !wallJumpUsed)
        {
            // Store the normal of the wall for the jump direction
            wallNormal = hit.normal;
            return true;
        }
        return false;
    }

    IEnumerator Dash()
    {
        for(int i = 0; i < 200; i++)
        {
            //turns the speed up temporarily
            speed = 25f;
            
            //normal stuff defined in update
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(x, 0f, z).normalized;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelo, 0.09f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            //for that like smooth animation
            yield return new WaitForSeconds(0.00001f); 
            speed = defaultSpeed;
        }
    }
    

}