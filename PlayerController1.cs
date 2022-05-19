using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public static PlayerController1 instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public CharacterController charController;
    public GameObject playerModel;
    public float rotateSpeed;
    public Animator anim;

    private Vector3 moveDirection;
    private Camera theCamera;
    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockBackCounter;
    public Vector2 knockedBackPower;

    public GameObject[] playerPieces;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = Camera.main;  
    }

    // Update is called once per frame
    void Update()
    {
        if(!isKnocking)
        {
            float yStore = moveDirection.y;
            // moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = -1f;

                if (Input.GetButtonDown("Jump")) { moveDirection.y = jumpForce; }
            }



            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            //transform.position = transform.position + (moveDirection) * Time.deltaTime * moveSpeed;

            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCamera.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                //playerModel.transform.rotation = newRotation;
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }

        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockedBackPower.x; 
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = -1f;

                if (Input.GetButtonDown("Jump")) { moveDirection.y = jumpForce; }
            }

            charController.Move(moveDirection * Time.deltaTime);
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", charController.isGrounded);
    }   

    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("OWIEEEE");
        moveDirection.y = knockedBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);

    }
}
