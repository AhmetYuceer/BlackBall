using UnityEngine;
using Enums;

public class Wall : MonoBehaviour
{ 
    [SerializeField] private WallType _wallType;
    public WallType GetWallType { get{return _wallType;} }
}