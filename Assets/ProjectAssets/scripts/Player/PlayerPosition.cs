using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPosition", menuName = "Scriptable Objects/Player/PlayerPosition")]
public class PlayerPosition : ScriptableObject
{
    [field: SerializeField] public float SampleDelay { get; private set; }

    public Vector3 PlayerDelayedPosition { get; set; }
}
