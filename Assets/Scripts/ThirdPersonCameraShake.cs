using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    
    public float sensitivity = 2f; 
    public float smoothTime = 0.1f; 

  
    public float lookAtDistance = 5f; 
    public float lookAtSpeed = 5f; 

    
    public Transform playerTransform;

    
    private Vector3 initialPosition; 
    private Vector3 targetPosition; 
    private Quaternion targetRotation; 
    private float smoothRotationVelocity; 

    private void Start()
    {
       
        transform.parent = playerTransform;
        initialPosition = transform.localPosition;
    }

    private void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        transform.RotateAround(playerTransform.position, Vector3.up, mouseX);
        transform.RotateAround(playerTransform.position, playerTransform.right, -mouseY);

       
        if (Input.GetMouseButton(1)) 
        {
            
            targetPosition = playerTransform.position + playerTransform.forward * lookAtDistance;
            targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position, Vector3.up);


            transform.position = Vector3.Lerp(transform.position, targetPosition, lookAtSpeed * Time.deltaTime);

            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime );
        }
        else if (playerTransform.GetComponent<Rigidbody>().velocity.magnitude > 0f) 
        {
            
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, lookAtSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation, lookAtSpeed * Time.deltaTime);
        }
    }
}