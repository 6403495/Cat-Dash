
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Running")]
    public float forwardSpeed = 8f;
    public float laneDistance = 2f;
    public float laneChangeSpeed = 10f;

    [Header("Jumping")]
    public float jumpForce = 8f;

    private CharacterController controller;
    private Animator catAnimator;
    private int currentLane = 1;
    private float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // ค้นหา Animator ที่อยู่บน Player หรือลูกของ Player
        catAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance != null &&
    !GameManager.Instance.IsPlaying)
        {
            return;
        }

        HandleLaneInput();
        HandleJumpInput();
        MovePlayer();
    }

    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.A))
        {
            currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
        }

        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            controller.isGrounded)
        {
            verticalVelocity = jumpForce;

            // สั่งให้แมวเล่นท่ากระโดด
            if (catAnimator != null)
            {
                catAnimator.SetTrigger("Jump");
            }
        }
    }

    void MovePlayer()
    {
        float targetX = (currentLane - 1) * laneDistance;

        float nextX = Mathf.MoveTowards(
            transform.position.x,
            targetX,
            laneChangeSpeed * Time.deltaTime
        );

        float horizontalMove =
            nextX - transform.position.x;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += Physics.gravity.y
            * Time.deltaTime;

        Vector3 move = new Vector3(
            horizontalMove,
            verticalVelocity * Time.deltaTime,
            forwardSpeed * Time.deltaTime
        );

        controller.Move(move);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EndGame();
            }
        }
    }
}

