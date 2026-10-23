using UnityEngine;

public abstract class MovementBaseState {

    public abstract void EnterState(MovementStateManager movementManager);

    public abstract void UpdateState(MovementStateManager movementManager);

}