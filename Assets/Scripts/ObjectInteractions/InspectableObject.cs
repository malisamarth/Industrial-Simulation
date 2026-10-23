using UnityEngine;

public class InspectableObject : MonoBehaviour {

    [SerializeField] private GameObject inspectPoint;
    [SerializeField] private InspectableObjectType inspectableObjectType;
    [SerializeField] private GameObject InfoButton;

    [SerializeField] private string objectInfoDisplayString;

    [SerializeField] private ObjectInfoDisplay objectInfoDisplay;

    [SerializeField] RotationAxis rotationAxis;
    
    private GameState currentGameState = GameState.Walkthrough;

    private Vector3 fixedRotation;

    public float rotationSpeed = 200f;
    private float targetRotationAxis;
    public float smoothSpeed = 5f;

    private float scrollAmount;

    private void Awake() {
        fixedRotation = transform.eulerAngles;
    }

    private void Update() {

        if (currentGameState == GameState.InspectingObject) {

            scrollAmount = Input.GetAxis("Mouse ScrollWheel");

            if (scrollAmount != 0) {
                targetRotationAxis += scrollAmount * rotationSpeed;
            }

            Quaternion targetRotation = GetQuaternionRotationOnAxis(targetRotationAxis);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        }

    }


    private Quaternion GetQuaternionRotationOnAxis(float targetRotationAxis) {

        Quaternion targetRotation;

        if (inspectableObjectType == InspectableObjectType.TankLever) {

            targetRotation = Quaternion.Euler(targetRotationAxis, -90f, 0);
            return targetRotation;

        }

        if (rotationAxis == RotationAxis.XAxis) {

            targetRotation = Quaternion.Euler(targetRotationAxis, 0, 0);

        } else if (rotationAxis == RotationAxis.YAxis) {

            targetRotation = Quaternion.Euler(0, targetRotationAxis, 0);

        } else {

            targetRotation = Quaternion.Euler(0, 0, targetRotationAxis);

        }

        return targetRotation;

    }



    public InspectableObjectType GetInspectableObjectType() {
        return inspectableObjectType;
    }

    public Transform GetInspectPoint() {
        return inspectPoint.transform;
    }

    public void HideInfoButton() {
        InfoButton.SetActive(false);
    }

    public void ShowInfoButton() {
        InfoButton.SetActive(true);
    }

    public void SwitchToGameState(GameState state) {
        currentGameState = state;
    }

    public void ResetRotation() {
        transform.eulerAngles = fixedRotation;
    }

    public void SetObjectInfo() {
        objectInfoDisplay.SetInfoDisplay(objectInfoDisplayString);
    }

    public void ResetOjectInfo() {
        objectInfoDisplay.DisplayNothing();
    }


    public float GetScrollAmount() {
        return scrollAmount;
    }

}


