using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CGameObject : MonoBehaviour
{
    protected CPositionControl positionControl;

    protected void InitGameObject()
    {
        positionControl = new CPositionControl(transform);
    }
}
