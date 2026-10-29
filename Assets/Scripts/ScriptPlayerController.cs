using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ScriptPlayerController : MonoBehaviour
{
    public float laneChangeSpeed = 12f;
    public float jumpHeight = 1.5f;
    public float gravity = -25f;

    public float hitCooldown = 0.6f;
    private float hitTimer;

    private CharacterController controller;
    
    [Header("Animation Settings")]
    public Animator animator; 
    public string runAnimationName = "Take 001"; 

    private int currentLane;
    private float targetX;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

       
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

       
        if (animator != null)
        {
            animator.Play(runAnimationName);
        }

        currentLane = GameConstants.CenterLane;
        targetX = GameConstants.LaneToX(currentLane);
    }

    void Update()
    {
        HandleLaneInput();
        Move();
        UpdateAnimation();

        if (hitTimer > 0f) hitTimer -= Time.deltaTime;
    }

    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            currentLane = Mathf.Max(0, currentLane - 1);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            currentLane = Mathf.Min(GameConstants.LaneCount - 1, currentLane + 1);

        targetX = GameConstants.LaneToX(currentLane);
    }

    void Move()
    {
        Vector3 move = Vector3.zero;

        float currentForwardSpeed = ScriptGameManager.Instance != null 
            ? ScriptGameManager.Instance.CurrentSpeed 
            : 8f;

        move.z = currentForwardSpeed;

        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        move.x = (newX - transform.position.x) / Time.deltaTime;

        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        float currentSpeed = ScriptGameManager.Instance != null 
            ? ScriptGameManager.Instance.CurrentSpeed 
            : 8f;

        
        animator.speed = currentSpeed / 8f; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            ScriptGameManager.Instance.AddCoin();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (hitTimer > 0f) return;
            hitTimer = hitCooldown;
            ScriptGameManager.Instance.TakeDamage(1);
        }
    }
}