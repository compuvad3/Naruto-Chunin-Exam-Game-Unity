using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnKill : MonoBehaviour {

    public int totalNumOfEnemies;

    [Header("UI")]
    [SerializeField] private GameObject winScreenCanvas;

    [Space]

    [Header("SFX")]
    public AudioSource WinningSFX;


    public void Start() {

        // Do not show the win canvas at the start!
        winScreenCanvas.SetActive(false);
    }


    public void CountKilledEnemies() {
        totalNumOfEnemies--;

        if (totalNumOfEnemies == 0) {
            NarutoMovementChapter1Scene1.playerControl = false;

            WinningSFX.Play();

            winScreenCanvas.SetActive(true);
        }
    }
}
