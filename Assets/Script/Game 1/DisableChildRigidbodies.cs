using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildRigidbodies : MonoBehaviour
{
    [Header("Referensi Objek")]
    public GameObject[] referenceObjects;
    [SerializeField] private Transform[] targetParents; // Array untuk multiple target parents
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning($"Tidak ada Rigidbody pada objek {gameObject.name}");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        UpdateKinematicState();
    }

    private void UpdateKinematicState()
    {
        rb.WakeUp();
        if (rb != null)
        {
            bool isChildOfAnyParent = CheckIfChildOfAnyParent();
            rb.isKinematic = isChildOfAnyParent;
        }
    }

    private bool CheckIfChildOfAnyParent()
    {
        if (targetParents == null || targetParents.Length == 0)
        {
            return false;
        }

        foreach (Transform parent in targetParents)
        {
            if (parent != null && transform.IsChildOf(parent))
            {
                return true;
            }
        }

        return false;
    }
}
