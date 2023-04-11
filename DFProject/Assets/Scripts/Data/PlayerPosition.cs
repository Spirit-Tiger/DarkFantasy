using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PlayerPosition", menuName = "Data/Player Position")]
public class PlayerPosition : ScriptableObject
{
    private Vector3 _defaultPostion = new Vector3(-0.51f, -2.2f, 1);
    public Vector3 Position;
}
