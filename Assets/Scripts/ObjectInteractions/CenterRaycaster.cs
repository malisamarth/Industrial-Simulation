using System.Net.NetworkInformation;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CenterRaycaster : MonoBehaviour {

    [SerializeField] private CinemachineCamera inspectionCamera;
    [SerializeField] private CinemachineCamera playerCamera;

    [SerializeField] private Image centerPointImage;
    [SerializeField] private float distance = 1f;

    [SerializeField] private GameObject playerModel;

    private InspectableObject inspectableObject;

    private MovementStateManager movementStateManager;

    private const string INSPECTABLE = "Inspectable";

    private GameState currentGameState = GameState.Walkthrough;

    private Ray ray;

    private InfoButton infoButton;

    private void Start() {

        movementStateManager = MovementStateManager.Instance;

    }

    void Update() {

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance)) {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(INSPECTABLE)) {

                SelectObjectWithRaycast(hit);

                if (Input.GetKeyDown(KeyCode.E)) {

                    InteractWithObject(hit);

                    if (inspectableObject != null) {

                        EnterInspectionMode();

                    }
                }

            } else {

                OnNotDetectInteractable();

            }

        }
        else {

            OnNotDetectInteractable();

        }

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        CheckExitMode();

    }


    void EnterInspection(InspectableObject obj) {

        Transform inspectPoint = obj.GetInspectPoint();

        inspectionCamera.transform.position = new Vector3(inspectPoint.position.x, inspectPoint.position.y, inspectPoint.position.z + (-0.5f)); //inspectPoint.position;
        inspectionCamera.transform.rotation = inspectPoint.rotation;

        inspectionCamera.Priority = 20;
        playerCamera.Priority = 10;
    }

    void ExitInspection() {

        inspectionCamera.Priority = 0;
        playerCamera.Priority = 10;
    }

    private void SelectObjectWithRaycast(RaycastHit hit) {

        OnDetectInteractable();

        infoButton = hit.collider.gameObject.GetComponent<InfoButton>();
        infoButton.ShowPress_E_Visual();
    }

    private void InteractWithObject(RaycastHit hit) {

        infoButton.HidePress_E_Visual();

        infoButton.ShowObjectDetails();

        playerModel.SetActive(false);

        currentGameState = GameState.InspectingObject;

        inspectableObject = hit.collider.gameObject.GetComponentInParent<InspectableObject>();

        inspectableObject.SetObjectInfo();

        Debug.Log(inspectableObject.GetInspectableObjectType());

        PressurePumpUI PressurePumpUI = hit.collider.gameObject.GetComponent<PressurePumpUI>();
    }

    private void EnterInspectionMode() {

        inspectableObject.HideInfoButton();

        EnterInspection(inspectableObject);

        movementStateManager.ChangeCurrentState(GameState.InspectingObject);

        inspectableObject.SwitchToGameState(GameState.InspectingObject);

        Debug.Log(inspectableObject.transform.position);
    }

    private void OnNotDetectInteractable() {

        centerPointImage.color = Color.white;

    }

    private void OnDetectInteractable() {

        centerPointImage.color = Color.blue;

    }

    private void CheckExitMode() {

        if (currentGameState == GameState.InspectingObject && Input.GetKeyDown(KeyCode.Q)) {

            inspectableObject.ResetOjectInfo();

            inspectableObject.SwitchToGameState(GameState.Walkthrough);

            playerModel.SetActive(true);

            ExitInspection();

            movementStateManager.ChangeCurrentState(GameState.Walkthrough);

            infoButton.HideObjectDetails();

            inspectableObject.ShowInfoButton();

            inspectableObject.ResetRotation();

        }

    }


}