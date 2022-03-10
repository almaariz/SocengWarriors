using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpostorFov : MonoBehaviour, IPlayerTriggerable
{
    public void OnPlayerTriggered(PlayerController player)
    {
        GameController.Instance.OnImpostorView(GetComponentInParent<ImpostorController>());
    }
}
