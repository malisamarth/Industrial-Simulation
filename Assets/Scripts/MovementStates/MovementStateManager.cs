using UnityEngine;

public class MovementStateManager : MonoBehaviour {

    public static MovementStateManager Instance { get; private set; }

    [SerializeField] public float currentMoveSpeed = 3f;

    private GameState currentGameState = GameState.Walkthrough;

    public float walkSpeed = 3f;
    public float walkBackSpeed = 3f;
    public float runSpeed = 7f;
    public float runBackSpeed = 5f;
    public float crouchSpeed = 2f;
    public float crouchBackSpeed = 1f;


    [SerializeField] private float groundOffset = 0.2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] public Animator animator;

    private Vector3 velocity;
    private Vector3 spherePosition;

    [HideInInspector] public Vector3 direction;

    public float horizontalInput;
    public float verticalInput;

    private CharacterController characterController;

    private MovementBaseState currentMovementBaseState;

    public IdleState idleState = new IdleState();
    public WalkState walkState = new WalkState();
    public RunState runState = new RunState();
    public CrouchState crouchState = new CrouchState();

    private void Awake() {

        characterController = GetComponent<CharacterController>();

        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

    }

    private void Start() {
        SwitchState(idleState);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {

        if (currentGameState == GameState.Walkthrough) {

            //Debug.Log("speed : - " + currentMoveSpeed);

            GetDirectionAndMove();
            Gravity();

            animator.SetFloat("hzInput", horizontalInput);
            animator.SetFloat("vInput", verticalInput);

            currentMovementBaseState.UpdateState(this);

        } else {
            Debug.Log("In \"Inspecting\" object, Player can't move!!");
        }


    }


    public void SwitchState(MovementBaseState state) {
         
        currentMovementBaseState = state;   
        currentMovementBaseState.EnterState(this);

    }


    private void GetDirectionAndMove() {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        direction = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

        characterController.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    private bool IsGrounded() {

        spherePosition = transform.position + Vector3.down * ((characterController.height / 2) - groundOffset);

        return Physics.CheckSphere(spherePosition, characterController.radius - 0.05f, groundMask);
    }

    private void Gravity() {

        if (!IsGrounded()) {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0) {
            velocity.y = -2f;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    public void ChangeCurrentState(GameState gameState) {

        currentGameState = gameState;

    }

}