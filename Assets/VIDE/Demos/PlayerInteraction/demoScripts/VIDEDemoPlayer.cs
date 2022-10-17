using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VIDE_Data;
using UnityEngine.InputSystem;

public class VIDEDemoPlayer : MonoBehaviour
{
    //This script handles player movement and interaction with other NPC game objects

    #region VARS
    
    [SerializeField] public string playerName = "VIDE User";

    private PlayerInput playerInput;
    private CharacterController characterController;
    [SerializeField] private Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 3.0f;

    //Reference to our diagUI script for quick access
    public VIDEUIManager1 diagUI;
    public QuestChartDemo questUI;
    // public Animator blue;

    //Stored current VA when inside a trigger
    public VIDE_Assign inTrigger;

    //DEMO variables for item inventory
    //Crazy cap NPC in the demo has items you can collect
    public List<string> demo_Items = new List<string>();
    public List<string> demo_ItemInventory = new List<string>();

    [SerializeField] private float _speed = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
    }

    void OnTriggerExit()
    {
        inTrigger = null;
    }

    #endregion

    #region MAIN

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Awake is called earlier than Start in Unity's event life cycle
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Alex's Player Input System (this was its location in the original code

        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        if (animator == null){
            animator = GetComponent<Animator>();
        }
        

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;

    }


    void handleAnimation()
    {
        bool iswalking = animator.GetBool("isWalking");

        if (isMovementPressed && !iswalking)
        {
            animator.SetBool("isWalking", true);
        }

        else if (!isMovementPressed && iswalking)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    { 
        playerInput.CharacterControls.Disable(); 
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
        } else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
        }
    }

    void Update()
    {

        //Only allow player to move and turn if there are no dialogs loaded
        if (!VD.isActive)
        {
            //transform.Rotate(0, Input.GetAxis("Mouse X") * 5, 0);
            //float move = Input.GetAxisRaw("Vertical");
            // transform.position += transform.forward * 7 * move * Time.deltaTime;
            // transform.position = transform.position + currentMovement.ToIso() * currentMovement.normalized.magnitude * _speed * Time.deltaTime;
            //blue.SetFloat("speed", move);
            // characterController.Move(currentMovement * .025f); 
            handleGravity();
            handleRotation();
            handleAnimation();
            characterController.Move(currentMovement.ToIso() * currentMovement.normalized.magnitude * _speed * Time.deltaTime);
        }


        //Interact with NPCs when pressing E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        //Hide/Show cursor
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //Casts a ray to see if we hit an NPC and, if so, we interact
    void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger)
        {
            diagUI.Interact(inTrigger);
            return;
        }

        /* If we are not in a trigger, try with raycasts */

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
        {
            //Lets grab the NPC's VIDE_Assign script, if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;

            if (assigned.alias == "QuestUI")
            {
                questUI.Interact(); //Begins interaction with Quest Chart
            } else
            {
                diagUI.Interact(assigned); //Begins interaction
            }
        }
    }

    #endregion
}
