using Ink.Runtime;
using UnityEngine;

// Will make sure a Rigidbody2D is actually attached to the script
// Will not execute otherwise
[RequireComponent(typeof(Rigidbody2D))]

public class Shuriken : MonoBehaviour {
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float damage = 10.0f;

    // These can be changed by other scripts
    [HideInInspector]       // does not automatically show the public field
    public float Speed {
        get => speed;
        set => speed = value;
    }

    public float Damage {
        get => damage;
        set => damage = value;
    }


    // Start is called before the first frame update
    void Awake() {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

        // The Shuriken will self-destroy in specified amount of seconds
        Invoke("DestroySelf", 10.0f);
    }


    public void SetDirection(Vector2 direction) {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }


    // Whenever the Shuriken collides with an object, it will destroy itself
    private void OnTriggerEnter2D(Collider2D collider) {
        
        // If the Shuriken collides with the player, do not destroy it!
        if (collider.CompareTag("Player"))
            return;

        // Deal damage
        Damageable d = collider.GetComponent<Damageable>();
        if (d != null)
            d.Damage(damage);

        DestroySelf();
    }


    void DestroySelf() => Destroy(gameObject);
}
