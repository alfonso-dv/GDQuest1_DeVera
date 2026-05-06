using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private bool isJumping = false;
    private float jumpCooldownTimer;

    private CharacterController controller;
    private Animator animator;
    private InputAction moveAction;
    private InputAction jumpAction;

    [SerializeField] private float jumpCooldown;
    [SerializeField] private float gravity;
    [SerializeField] private float characterSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float dampening;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float pushStrength = 1f;
    [SerializeField] private Transform currentCheckpoint;

    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private AudioSource soundEffectAudio;
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private ParticleSystem walkingParticles;

    private Vector3 characterMovement;
    private Vector3 jumpVelocity;
    private Vector3 characterGravity;
    private Vector3 platformVelocity;

    void Start()
    {
        this.controller = this.GetComponent<CharacterController>();
        this.animator = this.GetComponent<Animator>();

        this.moveAction = InputSystem.actions.FindAction("Move");
        this.jumpAction = InputSystem.actions.FindAction("Jump");

        this.jumpCooldownTimer = 0.0f;
    }

    void SetAnimationState(Vector2 inputMovement)
    {
        this.animator.SetBool("IsJumping", this.isJumping);
        this.animator.SetBool("IsRunning", inputMovement != Vector2.zero);
        this.animator.SetFloat("MovementForward", inputMovement.magnitude);
    }

    void HandleFootsteps(Vector2 inputMovement)
        {
            bool isMoving = inputMovement != Vector2.zero;

            if (isMoving && this.controller.isGrounded && !this.isJumping)
            {
                if (!this.footstepAudio.isPlaying)
                {
                    this.footstepAudio.Play();
                }

                if (this.walkingParticles != null && !this.walkingParticles.isPlaying)
                {
                    this.walkingParticles.Play();
                }
            }
            else
            {
                if (this.footstepAudio.isPlaying)
                {
                    this.footstepAudio.Stop();
                }

                if (this.walkingParticles != null && this.walkingParticles.isPlaying)
                {
                    this.walkingParticles.Stop();
                }
            }
        }

    void HandleJumping()
    {
        if (this.controller.isGrounded && this.isJumping && this.jumpCooldownTimer <= 0.0f)
        {
            this.jumpVelocity = Vector3.zero;
            this.isJumping = false;
        }

        if (this.controller.isGrounded && !this.isJumping && this.jumpAction.WasPressedThisFrame())
        {
            this.characterGravity = Vector3.zero;
            this.jumpVelocity = Vector3.zero;
            this.jumpVelocity.y = this.jumpSpeed;
            this.jumpCooldownTimer = this.jumpCooldown;
            this.isJumping = true;

            this.soundEffectAudio.PlayOneShot(this.jumpSound);
        }

        if (this.jumpVelocity.y > 0.0f)
        {
            this.jumpVelocity.y -= Time.fixedDeltaTime;
        }
        else
        {
            this.jumpVelocity = Vector3.zero;
        }

        this.jumpCooldownTimer -= Time.fixedDeltaTime;
    }

    private void GetPlatformVelocity()
    {
        this.platformVelocity = Vector3.zero;

        if (this.isJumping)
        {
            return;
        }

        if (!this.controller.isGrounded)
        {
            return;
        }

        RaycastHit hit;
        float rayLength = this.controller.height / 2f + 0.5f;
        int platformLayerMask = LayerMask.GetMask("Platforms");

        Vector3 rayOrigin = this.transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayLength, platformLayerMask))
        {
            MovingPlatform movingPlatform = hit.collider.GetComponent<MovingPlatform>();

            if (movingPlatform != null)
            {
                this.platformVelocity = movingPlatform.GetVelocity();
            }
        }
    }

    void FixedUpdate()
    {
        this.HandleJumping();
        this.GetPlatformVelocity();

        Vector2 inputMovement = this.moveAction.ReadValue<Vector2>();

        this.SetAnimationState(inputMovement);
        this.HandleFootsteps(inputMovement);

        Vector3 inputRightDirection = this.cameraTransform.right;
        Vector3 inputForwardDirection = this.cameraTransform.forward;

        inputRightDirection.y = 0.0f;
        inputForwardDirection.y = 0.0f;

        inputRightDirection.Normalize();
        inputForwardDirection.Normalize();

        if (this.controller.isGrounded)
        {
            this.characterGravity.y = 0.0f;
        }

        this.characterGravity.y += this.gravity * Time.fixedDeltaTime;

        this.characterMovement += this.characterGravity * Time.fixedDeltaTime;
        this.characterMovement += this.jumpVelocity * Time.fixedDeltaTime;

        this.characterMovement += inputRightDirection * inputMovement.x * this.characterSpeed * Time.fixedDeltaTime;
        this.characterMovement += inputForwardDirection * inputMovement.y * this.characterSpeed * Time.fixedDeltaTime;

        this.characterMovement *= (1 - this.dampening);

        Vector3 inputDirection =
            inputRightDirection * inputMovement.x +
            inputForwardDirection * inputMovement.y;

        inputDirection.y = 0.0f;

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            this.transform.forward = inputDirection.normalized;
        }

        Vector3 combinedMovement = this.characterMovement + this.platformVelocity * Time.fixedDeltaTime;
        this.controller.Move(combinedMovement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
       EnemySquash enemy = hit.collider.GetComponentInParent<EnemySquash>();

        if (enemy != null)
        {
            Debug.Log("Touched enemy");

            bool playerIsAboveEnemy = this.transform.position.y > enemy.transform.position.y + 0.3f;

            if (playerIsAboveEnemy)
            {
                Debug.Log("Squashing enemy");
                enemy.SquashEnemy();
            }

            return;
        }   

        if (!hit.gameObject.CompareTag("Crate"))
        {
            return;
        }

        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f || hit.moveDirection.y > 0.3f)
        {
            return;
        }

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);
        body.linearVelocity = pushDirection * this.pushStrength;
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        this.currentCheckpoint = newCheckpoint;
    }

    public void RespawnAtCheckpoint()
    {
        if (this.currentCheckpoint != null)
        {
            this.controller.enabled = false;
            this.transform.position = this.currentCheckpoint.position;
            this.transform.rotation = this.currentCheckpoint.rotation;
            this.controller.enabled = true;
        }
    }
}