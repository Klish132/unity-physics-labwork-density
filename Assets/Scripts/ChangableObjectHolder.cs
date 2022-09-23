using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangableObjectHolder : ObjectHolder
{
    public int state = 0;

    public void MoveHeldObject(int dir, double maxHoldingHeight)
    {
        if (dir > 0 && state + dir <= GSC.sampleStateCount - 1)
        {
            state += dir;
            float move = (float)(-maxHoldingHeight / GSC.sampleStateCount);
            currHoldingPosition += new Vector3(0, move, 0);
            heldObject.transform.localPosition = currHoldingPosition;
            heldObjectScript.ChangeState(state);

        }
        else if (dir < 0 && state + dir >= 0)
        {
            state += dir;
            float move = (float)(maxHoldingHeight / GSC.sampleStateCount);
            currHoldingPosition += new Vector3(0, move, 0);
            heldObject.transform.localPosition = currHoldingPosition;
            heldObjectScript.ChangeState(state);
        }
    }

    public override void ClearObject()
    {
        base.ClearObject();
        state = 0;
        heldObjectScript.ChangeState(0);
        heldObject = null;
    }
}
