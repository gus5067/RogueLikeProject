using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IForceable
{
    public void TakeForce(Vector2 dir, int power);
}
