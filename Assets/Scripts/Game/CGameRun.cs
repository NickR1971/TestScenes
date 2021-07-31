using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameRun : CGame
{
    private CCubeRotation cube;

    void Start()
    {
        cube = FindObjectOfType<CCubeRotation>();
    }

}
