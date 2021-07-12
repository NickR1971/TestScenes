using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRecordContainer : MonoBehaviour
{
    [SerializeField] private GameObject record0;
    private GameObject[] records;

    public void CreateList(string[] _saveNames,bool _isSave)
    {
        records = new GameObject[_saveNames.Length];
        record0.SetActive(true);
        records[0] = record0;
        record0.GetComponent<CRecord>().Init(CLocalisation.GetString(EnumStringID.ui_new),_isSave);

        for (int i = 1; i < _saveNames.Length; i++)
        {
            records[i] = Instantiate(record0, Vector3.zero, Quaternion.identity, transform);
            records[i].GetComponent<CRecord>().Init(_saveNames[i],_isSave);
        }
        record0.SetActive(_isSave);
    }

    public void DestroyRecords()
    {
        for(int i=1;i<records.Length;i++)
        {
            Destroy(records[i]);
        }
        records = null;
    }
}
