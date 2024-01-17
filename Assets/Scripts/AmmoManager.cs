using UnityEngine;

public class AmmoManager : MonoBehaviour {
    public static AmmoManager instance;

    public uint ammo;

    public uint damage;


    private void Awake() {

        // Ensure only one instance of AmmoManager exists
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}