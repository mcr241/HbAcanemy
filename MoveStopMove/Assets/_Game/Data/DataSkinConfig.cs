using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeSkin
{
    Quan, Ao, Mu, Canh, Than
}

[System.Serializable]
public class DataTypeSkin
{
    public TypeSkin type;
    public Material[] materials;
}

[System.Serializable]
public class DataSkin
{
    public string name;
    public DataTypeSkin[] data;
}


[CreateAssetMenu(menuName = "DataConfig", order = 0)]
public class DataSkinConfig : ScriptableObject
{
    public List<DataSkin> data;

    public DataSkin GetDataSkin(int id)
    {
        return data[id];
    } 
}
