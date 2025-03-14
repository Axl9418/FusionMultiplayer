using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameManager : NetworkBehaviour
{
    //camera reference
    [SerializeField] private Camera cam;

    public override void Spawned()
    {
        //disable main camera
        cam.gameObject.SetActive(false);
    }
}
