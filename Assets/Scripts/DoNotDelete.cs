using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDelete : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
