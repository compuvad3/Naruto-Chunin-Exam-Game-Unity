using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NarutoMovementChapter1Scene1 : MonoBehaviour {

    // Variables set in the inspector
    [Header("Movement")]
    [SerializeField] private float mWalkSpeed;
    [SerializeField] private float mRunSpeed;
    [SerializeField] private float mJumpForce;
    [SerializeField] private LayerMask mWhatIsGround;

    [Space]

    [Header("Attacking")]
    [SerializeField] private float attackDelay = 0.65f;
    [SerializeField] private Transform shootPoint;          // point from where the shuriken is shoot
    [SerializeField] private GameObject bullet;             // the shuriken prefab
    [SerializeField] private uint maxAmmo;
    public AudioSource shurikenSFX;
    public AudioSource damageSFX;
    public AudioSource defeatSFX;

    [Space]

    [Header("UI")]
    [SerializeField] private Image healthFillImage;
    [SerializeField] private GameObject deathScreenCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI ammoText;

    private float kGroundCheckRadius = 0.3f;

    // Booleans used to coordinate with the animator's state machine
    private bool mRunning;
    private bool mMoving;
    private bool mGrounded;
    private bool mFalling;

    Vector2 mFacingDirection;

    private bool canMove = true;
    private bool canShoot = true;

    // Store the info if the player has or does not have control over the main character
    static public bool playerControl = false;

    // Variables that will be updated after completing Chapter 1 of the game
    private uint ammo;
    private uint damage;

    // References to Player's components
    private Animator mAnimator;
    private Rigidbody2D mRigidBody2D;
    private Transform mGroundCheck;


    private void Start() {

        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");

        healthFillImage.fillAmount = 1.0f;      // as we start, the health is set to 100%

        // Get the direction the Player is facing as the game starts
        mFacingDirection = new Vector2(GetComponentInChildren<SpriteRenderer>().flipX ? -1 : 1, 0);

        // Make sure the death screen is off as the game starts
        deathScreenCanvas.SetActive(false);

        // Initialize the ammo amount and its damage by retrieving it from the AmmoManager
        ammo = AmmoManager.instance.ammo;
        damage = AmmoManager.instance.damage;

        // Initialize the text of the ammo
        ammoText.SetText(ammo + " / " + maxAmmo);
    }


    private void Update() {

        // If a dialogue is playing, freeze the player!
        if (DialogueManager.GetInstance() != null && DialogueManager.GetInstance().dialogueIsPlaying) {
            mAnimator.SetBool("isGrounded", true);
            mAnimator.SetBool("isMoving", false);
            mAnimator.SetBool("isRunning", false);
            mAnimator.SetBool("isFalling", false);

            // If the end of the dialogue is reached, update the ammo, its damage and transition to the next level!
            if (DialogueManager.GetInstance().CheckStageTransition() == 1) {
                SetAmmoFromInk();
                SetDamageBasedOnWinningRate();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            return;
        }

        // If the player cannot move, exit!
        if (!canMove)
            return;

        UpdateGrounded();
        UpdateFalling();

        // Make sure that the character can move and attack (fire) only when the player has control
        if (playerControl) {
            MoveCharacter();

            if (Input.GetButton("Fire") && canShoot)
                StartCoroutine(Fire());
        }

        // Update Animator's variables
        if (mMoving && mGrounded)
            mAnimator.SetBool("isMoving", true);
        else
            mAnimator.SetBool("isMoving", false);

        if (mRunning && mGrounded)
            mAnimator.SetBool("isRunning", true);
        else
            mAnimator.SetBool("isRunning", false);

        if (mGrounded)
            mAnimator.SetBool("isGrounded", true);
        else
            mAnimator.SetBool("isGrounded", false);

        if (mFalling)
            mAnimator.SetBool("isFalling", true);
        else
            mAnimator.SetBool("isFalling", false);
    }


    private void UpdateGrounded() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mGroundCheck.position, kGroundCheckRadius, mWhatIsGround);
        foreach (Collider2D col in colliders) {
            if (col.gameObject != gameObject) {
                mGrounded = true;
                return;
            }
        }
        mGrounded = false;
    }


    private void MoveCharacter() {

        // Run is [Left Shift]
        mRunning = Input.GetButton("Run");

        float horizontal = Input.GetAxis("Horizontal");
        mMoving = !Mathf.Approximately(horizontal, 0f);
        if (mMoving) {
            transform.Translate(horizontal * (mRunning ? mRunSpeed : mWalkSpeed) * Time.deltaTime, 0, 0);
            FaceDirection(horizontal < 0f ? Vector2.left : Vector2.right);
        }

        if (mGrounded && Input.GetButtonDown("Jump"))
            mRigidBody2D.AddForce(new Vector2(0, mJumpForce), ForceMode2D.Impulse);
    }


    private void UpdateFalling() {
        mFalling = mRigidBody2D.velocity.y < 0.0f;
    }


    private void FaceDirection(Vector2 direction) {
        mFacingDirection = direction;

        if (direction == Vector2.right) {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else {
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }


    public Vector2 GetFacingDirection() {
        return mFacingDirection;
    }


    // Set the amount of ammo from the Ink file
    public void SetAmmoFromInk() {
        DialogueManager dialogueManager = DialogueManager.GetInstance();

        // Get the number of shurikens from the Ink file
        int inkAmmoValue = dialogueManager.GetShurikenCountFromInk();

        if (inkAmmoValue < 0)
            ammo = 0;
        else if (inkAmmoValue >= maxAmmo)
            ammo = maxAmmo;
        else
            ammo = (uint)inkAmmoValue;

        // Update the AmmoManager's ammo value
        AmmoManager.instance.ammo = ammo;

        // Update the UI text to reflect the new ammo value
        ammoText.SetText(ammo + " / " + maxAmmo);
    }


    // Set the damage of ammo from the Ink file
    public void SetDamageBasedOnWinningRate() {
        DialogueManager dialogueManager = DialogueManager.GetInstance();

        // Get the winning rate from the Ink file
        int inkWinningRateValue = dialogueManager.GetWinningRateFromInk();

        if (inkWinningRateValue >= 0 && inkWinningRateValue < 25)
            damage = 1;
        else if (inkWinningRateValue >= 25 && inkWinningRateValue < 50)
            damage = 5;
        else if (inkWinningRateValue >= 50 && inkWinningRateValue < 75)
            damage = 10;
        else
            damage = 15;

        // Update the AmmoManager's damage value
        AmmoManager.instance.damage = damage;
    }


    private IEnumerator Fire() {

        // If the player has no more ammo, cannot fire!
        if (ammo <= 0)
            yield break;

        // If the bullet or shooting point is not set, the Player cannot fire!
        if (bullet == null || shootPoint == null)
            yield break;

        canMove = false;
        canShoot = false;

        mAnimator.SetBool("isThrowing", true);

        Shuriken shuriken = Instantiate(bullet, shootPoint.position, transform.rotation).GetComponent<Shuriken>();

        shurikenSFX.Play();

        // Make sure the shuriken appears in the direction the player is facing
        shuriken.SetDirection(GetFacingDirection());

        // Change the damage of the shuriken based on the value in the player script
        shuriken.Damage = damage;

        // For each fired attack decrease the ammo amount by 1
        ammo--;

        // Update the UI text to reflect the new ammo value
        ammoText.SetText(ammo + " / " + maxAmmo);

        yield return new WaitForSeconds(attackDelay);
        canShoot = true;
        canMove = true;

        mAnimator.SetBool("isThrowing", false);
    }


    // How much time is needed for the damage taken animation
    private IEnumerator DamageTimer() {
        canMove = false;
        mAnimator.SetBool("isDamaged", true);

        damageSFX.Play();

        yield return new WaitForSeconds(0.5f);
        canMove = true;
        mAnimator.SetBool("isDamaged", false);
    }


    // Called whenever the Player is damaged
    public void OnDamage() {
        Damageable damageable = GetComponent<Damageable>();

        // Get the percentage of filled health image based on the current health and default total health
        healthFillImage.fillAmount = damageable.Health / damageable.DefaultHealth;

        StartCoroutine(DamageTimer());
    }


    // How much time is needed for the death animation
    private IEnumerator DeadTimer() {
        canMove = false;
        mAnimator.SetBool("isDead", true);

        defeatSFX.Play();

        yield return new WaitForSeconds(5.0f);
    }


    // Called when the Player dies
    public void OnDeath() {

        // Ensure that the AmmoManager instance is destroyed when the Player dies
        AmmoManager.instance = null;

        StartCoroutine(DeadTimer());

        // Show the death screen
        deathScreenCanvas.SetActive(true);

        // Unable the player to move
        enabled = false;
    }
}