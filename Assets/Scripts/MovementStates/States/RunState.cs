using UnityEngine;

public class RunState : MovementBaseState {
    
    
    public override void EnterState(MovementStateManager movementManager) {

        movementManager.animator.SetBool("Running", true);

    }

    public override void UpdateState(MovementStateManager movementManager) {
        
        if (Input.GetKeyUp(KeyCode.LeftShift)) {

            ExitState(movementManager, movementManager.walkState);

        } else if (movementManager.direction.magnitude < 0.1f) {
            ExitState(movementManager, movementManager.idleState);
        }

        if (movementManager.verticalInput < 0) {
            movementManager.currentMoveSpeed = movementManager.runBackSpeed;
        }
        else {
            movementManager.currentMoveSpeed = movementManager.runSpeed;
        }

    }


    private void ExitState(MovementStateManager movementManager, MovementBaseState baseState) {

        movementManager.animator.SetBool("Running", false);
        movementManager.SwitchState(baseState);

    }
}