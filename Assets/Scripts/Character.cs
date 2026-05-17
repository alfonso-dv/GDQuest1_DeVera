using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private bool isJumping = false;
    private float jumpCooldownTimer;

    private bool canMove = true;

    private CharacterController controller;
    private Animator animator;
    private InputAction moveAction;
    private InputAction jumpAction;

    [Header("Movement")]
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float gravity;
    [SerializeField] private float characterSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float dampening;
    [SerializeField] private Transform cameraTransform;

    [Header("Pushing")]
    [SerializeField] private float pushStrength = 1f;

    [Header("Checkpoint")]
    [SerializeField] private Transform currentCheckpoint;

    [Header("Audio")]
    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private AudioSource soundEffectAudio;
    [SerializeField] private AudioClip jumpSound;

    [Header("Particles")]
    [SerializeField] private ParticleSystem walkingParticles;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100.0f;
    private float currentHealth;
    [SerializeField] private int maxLives = 3;
    private int currentLives;

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
        this.currentHealth = this.maxHealth;
        this.currentLives = this.maxLives;
    }

    void FixedUpdate()
    {
        if (!this.canMove)
        {
            this.HandleFootsteps(Vector2.zero);
            this.SetAnimationState(Vector2.zero);
            return;
        }

        if (this.IsDead())
        {
            this.HandleFootsteps(Vector2.zero);
            this.SetAnimationState(Vector2.zero);
            return;
        }

        this.HandleJumping();
        this.GetPlatformVelocity();

        Vector2 inputMovement = this.moveAction.ReadValue<Vector2>();

        this.SetAnimationState(inputMovement);
        this.HandleFootsteps(inputMovement);
        this.HandleMovement(inputMovement);
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

    void HandleMovement(Vector2 inputMovement)
    {
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        EnemySquash enemy = hit.collider.GetComponentInParent<EnemySquash>();

        if (enemy != null)
        {
            bool playerIsAboveEnemy = this.transform.position.y > enemy.transform.position.y + 0.3f;

            if (playerIsAboveEnemy)
            {
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

    public void InflictDamage(float amount)
    {
        this.currentHealth -= amount;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0.0f, this.maxHealth);
    }

    public void RestoreHealth()
    {
        this.currentHealth = this.maxHealth;
    }

    public float GetCurrentHealth()
    {
        return this.currentHealth;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }

    public bool IsDead()
    {
        return this.currentHealth <= 0.0f;
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

            this.RestoreHealth();
        }
    }
    public int GetCurrentLives()
    {
        return this.currentLives;
    }

    public int GetMaxLives()
    {
        return this.maxLives;
    }

    public void LoseLife()
    {
        this.currentLives--;
    }

    public bool HasLivesLeft()
    {
        return this.currentLives > 0;
    }
    public void DisableMovement()
    {
        this.canMove = false;
    }

    public void EnableMovement()
    {
        this.canMove = true;
    }
}