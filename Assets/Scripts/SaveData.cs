using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
	public uint id;
    //public Color color;
    public float r;
    public float g;
    public float b;
    public float a;

    public void SetColor(Color _color)
    {
        r = _color.r;
        g = _color.g;
        b = _color.b;
        a = _color.a;
    }

    public Color GetColor()
    {
        Color color = new Color(r, g, b, a);

        return color;
    }
}
