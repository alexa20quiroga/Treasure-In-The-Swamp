using UnityEngine;

public class Personajes : MonoBehaviour
{
    public float speed = 5f;
    public int playerNumber = 1;

    // JUMP SYSTEM
    public float gravity = -35f;
    public float jumpHeight = 2.2f;
    private float verticalVelocity;

    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;

    public bool canMove = true;

    // TREASURE
    public Transform treasure;
    public float pickupDistance = 2f;

    private bool gameEnded = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        treasure = GameObject.FindWithTag("Treasure").transform;
    }

    void Update()
    {
        if (!canMove || gameEnded) return;

        float h = 0f;
        float v = 0f;

        // INPUT
        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.A)) h = -1;
            if (Input.GetKey(KeyCode.D)) h = 1;
            if (Input.GetKey(KeyCode.W)) v = 1;
            if (Input.GetKey(KeyCode.S)) v = -1;

            if (Input.GetKeyDown(KeyCode.Q) && controller.isGrounded)
                Jump();
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) h = -1;
            if (Input.GetKey(KeyCode.RightArrow)) h = 1;
            if (Input.GetKey(KeyCode.UpArrow)) v = 1;
            if (Input.GetKey(KeyCode.DownArrow)) v = -1;

            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
                Jump();
        }

        // CAMERA MOVEMENT
        Vector3 forward = cameraTransform ? cameraTransform.forward : Vector3.forward;
        Vector3 right = cameraTransform ? cameraTransform.right : Vector3.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * v + right * h;

        if (moveDirection != Vector3.zero)
            transform.forward = moveDirection;

        // GRAVITY
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        // MOVE
        Vector3 horizontal = moveDirection * speed;
        Vector3 vertical = Vector3.up * verticalVelocity;

        controller.Move((horizontal + vertical) * Time.deltaTime);

        // ANIMATION
        if (animator != null)
        {
            animator.SetFloat("Speed", controller.velocity.magnitude);
            animator.SetBool("IsGrounded", controller.isGrounded);
        }

        // TREASURE CHECK
        if (Vector3.Distance(transform.position, treasure.position) < pickupDistance)
{
    Debug.Log("TOCANDO TESORO");
    gameEnded = true;

    if (GameManager.instance != null)
    {
        Debug.Log("LLAMANDO A WIN");
        GameManager.instance.PlayerWins(gameObject.name);
    }
    else
    {
        Debug.LogError("GameManager es NULL");
    }
}
    }

    void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}