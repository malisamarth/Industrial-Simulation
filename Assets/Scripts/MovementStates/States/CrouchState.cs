using UnityEngine;

public class CrouchState : MovementBaseState {


    public override void EnterState(MovementStateManager movementManager) {

        movementManager.animator.SetBool("Crouching", true);

    }

    public override void UpdateState(MovementStateManager movementManager) {

        if (Input.GetKey(KeyCode.LeftShift)) {

            ExitState(movementManager, movementManager.runState);

        }

        if (Input.GetKeyDown(KeyCode.C)) {

            if (movementManager.direction.magnitude < 0.1f) { 

                Debug.Log("exiting to idle !!");

            } else {

                ExitState(movementManager, movementManager.walkState);
                Debug.Log("exiting to walking !!");

            }

        }

        if (movementManager.verticalInput < 0) {
            movementManager.currentMoveSpeed = movementManager.crouchBackSpeed;
        }
        else {
            movementManager.currentMoveSpeed = movementManager.crouchSpeed;
        }

    }

    private void ExitState(MovementStateManager movementManager, MovementBaseState baseState) {

        movementManager.animator.SetBool("Crouching", false);
        movementManager.SwitchState(baseState);

    }


}