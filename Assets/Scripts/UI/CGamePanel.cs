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
        actor.AddCommand(ActorCommand.walk);
        //actor.Walk();
    }
    public void OnDown()
    {
        actor.AddCommand(ActorCommand.wait);
        //actor.Idle();
    }
    public void OnLeft()
    {
        actor.AddCommand(ActorCommand.turnleft);
        //actor.Turn(-90);
    }
    public void OnRight()
    {
        actor.AddCommand(ActorCommand.turnright);
        //actor.Turn(90);
    }

    public void OnAttack()
    {
        actor.AddCommand(ActorCommand.melee);
        //actor.SetState(ActorState.melee);
    }

    public void OnDie()
    {
        actor.AddCommand(ActorCommand.die);
        //actor.SetState(ActorState.die);
    }
}
