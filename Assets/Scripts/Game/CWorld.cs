using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWorld : MonoBehaviour
{
    private IGame game;
    private SaveData gameData;
    private CRand mapGenerator;

    void Start()
    {
        game = AllServices.Container.Get<IGame>();
        gameData = game.GetData();
        mapGenerator = new CRand(gameData.id);
    }

    void Update()
    {
        
    }
}
