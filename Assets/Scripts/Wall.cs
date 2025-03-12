using UnityEngine;
using System.Collections.Generic;
using Enums;

public class Wall : MonoBehaviour
{ 
    [SerializeField] private WallType _wallType;
    public WallType GetWallType { get{return _wallType;} }

    [SerializeField] private List<GameObject> _obtacles = new List<GameObject>();

    private void Start()
    {
        if (_obtacles.Count < 1) { return; }
        ActivateObtacles();
    }

    private void ActivateObtacles()
    {
        for (int j = 0; j < 3; j++)
        {
            var rnd = Random.Range(0, _obtacles.Count -1);
            _obtacles[rnd].SetActive(true);
        }
    }
}