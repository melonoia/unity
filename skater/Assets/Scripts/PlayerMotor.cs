using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private const float LANE_DISTANCE = 2.5f;
    private const float TURN_SPEED = 0.05f;

    // animation
    private Animator anim;

    // Movement
    public float jumpForce = 4.0f;
    public float gravity = 12.0f;
    private float verticalVelocity;

    // speed modifier
    private float originalSpeed = 7.0f;
    private float speed;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    private int desiredLane = 1; // 0 = Left, middle = 1, right = 2

    private bool isRunning = false;

    private void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;

            // modifier text
            GameManager.Instance.UpdateModifier(speed);
        }

        // get Input on which Lane
        if (MobileInput.Instance.SwipeLeft)
            // move left
            MoveLane(false);

        if (MobileInput.Instance.SwipeRight)
            // move right
            MoveLane(true);

        // Calculate where to in future
        Vector3 targetPosition = transform.position.z * Vector3.forward;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        // move delta
        Vector3 moveVector = Vector3.zero;

        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        // calculate y
        if (isGrounded)
        {
            verticalVelocity = -0.1f;
            if (MobileInput.Instance.SwipeUp)
            {
                // jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }

            else if (MobileInput.Instance.SwipeDown)
            {
                //slide
                StartSliding();
                Invoke("StopSliding", 1.0f);
            }

        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            // fast falling mechanic
            if (MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        // move Pingu
        controller.Move(moveVector * Time.deltaTime);

        // rotate pingu
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {

            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
    }
    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + .2f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

        return (Physics.Raycast(groundRay, 0.2f + 0.1f));
    }

    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
    }

    public void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }

    public void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }

    }
}
