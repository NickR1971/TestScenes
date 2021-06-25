using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSaveFile
{
	private string saveFileName;

	public void Init(string _filename)
    {
		saveFileName = _filename;
    }

	public void Save(SaveData data)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(saveFileName);
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load(ref SaveData data)
	{
		if (File.Exists(saveFileName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(saveFileName, FileMode.Open);
			data = (SaveData)bf.Deserialize(file);
			file.Close();
		}
		else
			Debug.LogError("There is no save data!");
	}

	public void ResetData()
	{
		if (File.Exists(saveFileName))
		{
			File.Delete(saveFileName);
		}
		else
			Debug.LogError("No save data to delete.");
	}

}
