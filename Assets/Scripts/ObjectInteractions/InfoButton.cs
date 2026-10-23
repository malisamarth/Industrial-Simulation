using UnityEngine;

public class InfoButton : MonoBehaviour {

    [SerializeField] private GameObject press_E_Visual;

    [SerializeField] private GameObject ObjectDetails;

    private bool isPress_E_Visual = false;

    private float elaspedTime = 0;
    private float timeLimit = 3f;

    private void Start() {
        HidePress_E_Visual();
    }

    private void Update() {
        
        if (isPress_E_Visual) {

            if (elaspedTime >= timeLimit) {
                HidePress_E_Visual();
                elaspedTime = 0;
            } else {
                elaspedTime += Time.deltaTime;
            }
        }

    }

    public void ShowPress_E_Visual() {
        press_E_Visual.SetActive(true);
        isPress_E_Visual = true;
    }

    public void HidePress_E_Visual() {
        press_E_Visual.SetActive(false);
        isPress_E_Visual = false;
    }

    public void ShowObjectDetails() {
        ObjectDetails.SetActive(true);
    }


    public void HideObjectDetails() {
        ObjectDetails.SetActive(false);
    }

}