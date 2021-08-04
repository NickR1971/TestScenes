﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightPosition { dangeon=0, morning=1, noon=2, evening=3, zenit=4 }

public interface ILight : IService
{
    void MoveTo(Vector3 _target);
    void MoveTo(LightPosition _position);
    void SetInstant(Vector3 _target);
    void SetInstant(LightPosition _position);
    void SetColor(Color _color);
    void SetColor(float _r, float _g, float _b);
}

public class CLight : MonoBehaviour, ILight
{
    private Light mLight;
    private CMove move;
    private const float changeTime = 0.7f; // час в секундах за який змінюється положення джеоела світла
    private Vector3[] standartPosition = new Vector3[5];
    private IGameConsole gameConsole;

    private void Awake()
    {
        AllServices.Container.Register<ILight>(this);
        standartPosition[0] = new Vector3(0, -10, -1);
        standartPosition[1] = new Vector3(-10, 5, 0);
        standartPosition[2] = new Vector3(0, 7, -2);
        standartPosition[3] = new Vector3(10, 5, -1);
        standartPosition[4] = new Vector3(0, 10, 0);
    }

    private void Start()
    {
        gameConsole = AllServices.Container.Get<IGameConsole>();
        gameConsole.AddCommand(new CGameConsoleCommand("light", OnConsole,EnumStringID.msg_light));
        mLight = GetComponent<Light>();
        move = new CMove();
        move.SetActionTime(changeTime);
        SetInstant(LightPosition.noon);
    }

    private void Update()
    {
        if (move.IsActive())
        {
            move.UpdatePosition();
            transform.position = move.GetCurrentPosition();
            transform.LookAt(Vector3.zero);
        }
    }

    private void OnConsole(string _str)
    {
        _str = CUtil.GetWord(_str);
        if (_str == "morning") MoveTo(LightPosition.morning);
        if (_str == "noon") MoveTo(LightPosition.noon);
        if (_str == "evening") MoveTo(LightPosition.evening);
    }

    //----------------------------------------------------------
    // ILight interface
    //----------------------------------------------------------
    public void MoveTo(Vector3 _target)
    {
        if (move.IsActive()) move.CorrectTargetPosition(_target);
        else
        {
            move.SetPositions(transform.position, _target);
            move.StartAction();
        }
    }

    public void SetInstant(Vector3 _target)
    {
        transform.position = _target;
        transform.LookAt(Vector3.zero);
    }

    public void SetColor(Color _color)
    {
        mLight.color = _color;
    }

    public void SetColor(float _r, float _g, float _b)
    {
        SetColor(new Color(_r, _g, _b));
    }

    public void MoveTo(LightPosition _position)
    {
        MoveTo(standartPosition[(int)_position]);
    }

    public void SetInstant(LightPosition _position)
    {
        SetInstant(standartPosition[(int)_position]);
    }
    //----------------------------------------------------------
}
