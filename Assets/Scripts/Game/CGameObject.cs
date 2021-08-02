using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameObject : MonoBehaviour
{
    private CPositionControl positionControl;

    private void Start()
    {
        positionControl = new CPositionControl(transform);
        positionControl.MoveForward();
    }
    private void Update()
    {
        positionControl.Update();
    }
}
