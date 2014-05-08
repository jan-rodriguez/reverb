using UnityEngine;
using System.Collections;

public class CityWinScript : MonoBehaviour {

    private float winTimer = 0f;
    public int playersInside;

    void Start() {
        DontDestroyOnLoad(this);
    }

    void OnTriggerEnter(Collider collider) {

        //Player walked into the platform
        if (collider.transform.name == "FirstPersonController(Clone)") {
            playersInside++;
            //If both players are in the platform activate win animations
            if (playersInside == (((GameManagerC)GameObject.FindGameObjectWithTag("GameManager").GetComponent("GameManagerC")).twoPlayers ? 2 : 1)) {
                StartCoroutine("PlayWinAnimation");
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        //Player left the platform
        if (collider.transform.name == "FirstPersonController(Clone)") {
            playersInside--;
        }
    }

    IEnumerator PlayWinAnimation() {
        while (winTimer < 1) {

            winTimer += Time.deltaTime;
            Debug.Log(winTimer);

            yield return null;
        }

        Application.Quit();
        Debug.Log("Quit attempted. Quitting only works in built projects, not from plays within the Unity Editor.");
    }

}
