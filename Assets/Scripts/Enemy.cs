using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [Header("Attack")]
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float range = 2.0f;            // how far the enemy can attack the player
    [SerializeField] private float attackCooldown = 0.75f;

    [Space]

    [Header("Movement")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform finalPoint;

    [Space]

    [Header("SFX")]
    public AudioSource damageSFX;
    public AudioSource defeatSFX;

    private GameObject player;
    private bool canAttack = true;

    private Transform[] points = new Transform[2];

    // If target is 0, the enemy will be moving towards the starting point
    // If target is 1, the enemy will be moving towards the final point
    int target = 0;

    // References to Enemy component
    private Animator mAnimator;

    // Boolean used to coordinate with the Animator's state machine
    private bool mRunning;

    private bool canMove = true;

    public void Start() {

        // Get reference to the Animator component
        mAnimator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

        // If there is no player in the game
        if (player == null)
            enabled = false;

        points = new Transform[2] {
            startingPoint,
            finalPoint
        };
    }


    public void Update() {

        // If the enemy cannot move, exit!
        if (!canMove)
            return;

        // If the distance between the player and the enemy is less than the attack range,
        // and the enemy can attack then damage can be dealt
        if (Vector3.Distance(player.transform.position, transform.position) < range && canAttack) {
            Damageable d = player.GetComponent<Damageable>();

            // If d is not null, the damage is dealt
            if (d != null)
                d.Damage(damage);

            StartCoroutine(AttackCooldown());
        }

        // Decide the taget direction of the enemy
        Transform point = points[target];

        // Move towards the target point
        float move = 0.0f;

        // If the distance between the target point and the enemy is less than a particular value,
        // then reverse our target point (if target equals to 0, make it 1 or else make it 0)
        if (Mathf.Abs(point.position.x - transform.position.x) < 0.2f) {
            target = target == 0 ? 1 : 0;
        }
        else {

            // If the target point is to the left of the enemy position, move to the left (-1.0f)
            // Otherwise, the target point is to the right, so move to the right (1.0f)
            if (point.position.x < transform.position.x)
                move = -1.0f;
            else if (point.position.x > transform.position.x)
                move = 1.0f;
        }

        // Play the running animation when the enemy moves
        mRunning = true;

        if (mRunning)
            mAnimator.SetBool("isRunning", true);
        else
            mAnimator.SetBool("isRunning", false);

        // Make the enemy flip depending on the moving direction
        if (((move < 0) && (transform.localScale.x < 0)) || ((move > 0) && (transform.localScale.x > 0))) {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        // Update the enemy position
        transform.Translate(new Vector3(move * speed * Time.deltaTime, 0));
    }


    // Wait for attackCooldown seconds before the enemy can attack again
    // This prevents continuous attacks
    private IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    // How much time we need for showing the damage taken animation
    private IEnumerator DamageTimer() {
        canMove = false;
        mAnimator.SetBool("isDamaged", true);

        damageSFX.Play();

        yield return new WaitForSeconds(0.25f);
        canMove = true;
        mAnimator.SetBool("isDamaged", false);
    }


    // Called whenever the enemy is damaged
    public void OnDamage() {
        StartCoroutine(DamageTimer());
    }


    // How much time we need for showing the death animation
    private IEnumerator DeadTimer() {
        canMove = false;
        mAnimator.SetBool("isDead", true);

        defeatSFX.Play();

        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }


    // Called when the enemy dies
    public void OnDeath() {
        StartCoroutine(DeadTimer());
        enabled = false;
    }
}