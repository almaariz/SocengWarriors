using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer; 
    [SerializeField] LayerMask interactableLayer; 
    [SerializeField] LayerMask playerLayer; 

    public static GameLayers Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }

    public LayerMask SolidLayer {
        get => solidObjectsLayer;
    }

    public LayerMask InteractableLayer {
        get => interactableLayer;
    }

    public LayerMask PlayerLayer {
        get => playerLayer;
    }
}
