using UnityEngine;

public class MenuUI : MonoBehaviour {


    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {

            Application.Quit();

        }

    }


}