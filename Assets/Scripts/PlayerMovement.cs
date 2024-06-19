using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float runSpeed = 10f; 
    public float rotationSpeed = 5f;
    public Transform cameraTransform;
    private Vector3 moveDirection;

    private Animator animator;
    private float movementSpeed;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = (cameraTransform.right * horizontal + cameraTransform.forward * vertical).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
        
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * 5f); 
        }
        else if (moveDirection != Vector3.zero)
        {
            
            movementSpeed = Mathf.Lerp(movementSpeed, moveSpeed, Time.deltaTime * 5f); 
        }
        else
        {
           
            movementSpeed = Mathf.Lerp(movementSpeed, 0f, Time.deltaTime * 5f); 
        }

        transform.position += moveDirection * movementSpeed * Time.deltaTime;

        
        animator.SetFloat("Speed", movementSpeed, 0.1f, Time.deltaTime); 

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}