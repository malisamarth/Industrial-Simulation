using UnityEngine;

public class IdleState : MovementBaseState {

    public override void EnterState(MovementStateManager movementManager) {

        //movementManager.animator.SetBool("Walking", true);

    }

    public override void UpdateState(MovementStateManager movementManager) {
        
        if (movementManager.direction.magnitude > 0.1f) {

            if (Input.GetKey(KeyCode.LeftShift)) {

                movementManager.SwitchState(movementManager.runState);

            } else {

                movementManager.SwitchState(movementManager.walkState);

            }

        } else {

            if (Input.GetKeyDown(KeyCode.C)) {

                movementManager.SwitchState(movementManager.crouchState);

            }

        }

    }
}