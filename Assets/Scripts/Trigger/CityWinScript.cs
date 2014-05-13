using UnityEngine;
using System.Collections;

public class CityWinScript : MonoBehaviour {

    private int playersInside;

    public float winFadeSpeed = 1.0f;

    public Texture winImage;

    private Color fadeColor;

    void Start() {

        fadeColor = Color.clear;

    }

    void Update() {

        //If both players are in the platform activate win animations
        if (playersInside == 1) {
            fadeColor = Color.Lerp(fadeColor, Color.white, winFadeSpeed * Time.deltaTime);
        }

        if (fadeColor.a > 0.95f) {
            // Win the game!
            Debug.Log("Win!");
            Application.Quit();
        }

    }

    void OnGUI() {

        GUI.color = fadeColor;
        GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), winImage);

    }

    void OnTriggerEnter(Collider collider) {

        //Player walked into the platform
        if (collider.transform.name == "FirstPersonController(Clone)") {
            playersInside++;
        }
    }

    void OnTriggerExit(Collider collider) {
        //Player left the platform
        if (collider.transform.name == "FirstPersonController(Clone)") {
            playersInside--;
        }
    }

}
