using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePanel : CUI
{
    [SerializeField] private GameObject mainMenu;
    private IGameConsole gameConsole;
    private CActor actor;

    void Start()
    {
        gameConsole = AllServices.Container.Get<IGameConsole>();
        mainMenu.GetComponent<CUI>().InitUI();
        actor = FindObjectOfType<CActor>();
        if (actor == null) Debug.LogError("Actor not found");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsActive()) uiManager.OpenUI(mainMenu.GetComponent<CUI>());
        }
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            gameConsole.Show();
        }
    }

    public void OnUp()
    {
        actor.Walk();
    }
    public void OnDown()
    {
        actor.Idle();
    }
    public void OnLeft()
    {
        actor.Turn(-90);
    }
    public void OnRight()
    {
        actor.Turn(90);
    }

    public void OnAttack()
    {
        actor.SetState(ActorState.melee);
    }

    public void OnDie()
    {
        actor.SetState(ActorState.die);
    }
}
