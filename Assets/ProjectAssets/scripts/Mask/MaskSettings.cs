using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum MaskType { Green, Red, Blue }

[Serializable]
public struct MaskData
{
    public Sprite Sprite;
    public ProjectileHandler ProjectilePrefab;
}

public class MaskSettings : MonoBehaviour
{
    [SerializeField] private MaskType[] _MaskTypes;
    [SerializeField] private MaskData[] _MaskData;

    private static Dictionary<MaskType, MaskData> _maskDictionary = new Dictionary<MaskType, MaskData>();

    private void Awake()
    {
        if (_MaskData.Length != 0 && _MaskTypes.Length != _MaskData.Length) { Debug.LogError("MaskTypes and MaskData must be the same length, and not empty"); }
        else
        {
            for (int i = 0; i < _MaskTypes.Length; i++)
            {
                _maskDictionary.Add(_MaskTypes[i], _MaskData[i]);
            }
        }
    }
    public static MaskData GetDataByType(MaskType type)
    {
        return _maskDictionary[type];
    }
    public static Sprite GetSpriteByType(MaskType type)
    {
        return _maskDictionary[type].Sprite;
    }

}