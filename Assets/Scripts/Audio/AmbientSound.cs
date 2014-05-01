using UnityEngine;
using System.Collections;

/// <summary>
/// A source of sound that loops repeatedly or just plays every now and then.
/// </summary>
public class AmbientSound : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip ambientSound;

    public float minLoopDelay = 0f;
    public float maxLoopDelay = 0f;
    private float loopDelay = 0f; // Initialized in Start()

    private float timeSinceFinished = 0f;

	// Use this for initialization
	void Start () {

        loopDelay = Random.Range(minLoopDelay, maxLoopDelay); //inclusive on both sides
	
	}
	
	// Update is called once per frame
	void Update () {

        if (audioSource != null && ambientSound != null) {

            // Start playing the audio source if it's not playing and we've waited long enough
            if (!audioSource.isPlaying) {
                if (timeSinceFinished >= loopDelay) {
                    audioSource.PlayOneShot(ambientSound);
                    timeSinceFinished = 0f;
                }
                else {
                    timeSinceFinished += Time.deltaTime;
                }
            }

        }
	
	}
}
