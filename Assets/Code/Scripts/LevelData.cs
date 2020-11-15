using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : MonoBehaviour
{

    string[] coordinates;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void setCoordinates(string[] coordinates)
    {
        this.coordinates = coordinates;
    }

    public string[] GetCoordinates()
    {
        return coordinates;
    }

}
