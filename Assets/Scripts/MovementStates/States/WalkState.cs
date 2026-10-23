using UnityEngine;

public class WalkState : MovementBaseState {


    public override void EnterState(MovementStateManager movementManager) {

        movementManager.animator.SetBool("Walking", true);

    }

    public override void UpdateState(MovementStateManager movementManager) {

        if (Input.GetKey(KeyCode.LeftShift)) {

            ExitState(movementManager, movementManager.runState);

        } else if (Input.GetKeyDown(KeyCode.C)) {

            ExitState(movementManager, movementManager.crouchState);

        } else if (movementManager.direction.magnitude < 0.1f) {

            ExitState(movementManager, movementManager.idleState);
        }

        if(movementManager.verticalInput < 0) {
            movementManager.currentMoveSpeed = movementManager.walkBackSpeed;
        } else {
            movementManager.currentMoveSpeed = movementManager.walkSpeed;
        }

    }

    private void ExitState(MovementStateManager movementManager, MovementBaseState baseState) {

        movementManager.animator.SetBool("Walking", false);
        movementManager.SwitchState(baseState);

    }

}