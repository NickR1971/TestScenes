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
        InitUI();
        gameConsole = AllServices.Container.Get<IGameConsole>();
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
        actor.AddCommand(ActorCommand.run);
    }
    public void OnDown()
    {
        actor.AddCommand(ActorCommand.wait);
    }
    public void OnLeft()
    {
        actor.AddCommand(ActorCommand.turnleft);
    }
    public void OnRight()
    {
        actor.AddCommand(ActorCommand.turnright);
    }

    public void OnAttack()
    {
        actor.AddCommand(ActorCommand.melee);
    }

    public void OnDie()
    {
        actor.AddCommand(ActorCommand.die);
    }
}
