using System.Collections.Generic;
using UnityEngine;

public enum MaskType { Green, Red, Blue }

public class MaskSettings : MonoBehaviour
{
    [SerializeField] private MaskType[] _sprite;
    [SerializeField] private MaskData[] Masks;

    private Dictionary<MaskType, MaskData> _maskDictionary;
    private void Awake()
    {
        //init
    }

    public Sprite GetSpriteByType(MaskType type)
    {
            return _maskDictionary[type].Sprite;
    }
}

public struct MaskData
{
    public Sprite Sprite;

    public MaskData(Sprite sprite)
    {
        Sprite = sprite;
    }
}