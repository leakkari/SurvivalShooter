using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 movement;
    private Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;

    // * both functions Awake and FixedUpdate are automatically called by unity *


    //setting up references
	private void Awake()
	{
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>(); 
	}

    //every physics update
	private void FixedUpdate()
	{
        float h = Input.GetAxisRaw("Horizontal"); 

        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
	}

	private void Move(float h, float v)
	{
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
	
	}

    private void Turning ()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
            
        }
    }

    void Animating(float h, float v){
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }


}
