using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CUtil
{
    public static bool CheckNameForSave(string _name) => (_name.IndexOfAny(Path.GetInvalidFileNameChars()) < 0);
}
