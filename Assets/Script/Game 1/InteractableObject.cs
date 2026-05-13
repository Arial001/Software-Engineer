using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Rigidbody rb;
    public string ItemName;

    void Start()
    {
    }

    void Update()
    {

    }

    public string GetItemName()
    {
        return ItemName;
    }

    
}
