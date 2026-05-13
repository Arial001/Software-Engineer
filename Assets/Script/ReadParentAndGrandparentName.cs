using UnityEngine;

public class ReadParentAndGrandparentName : MonoBehaviour
{
    private void Start()
    {
        read();
    }

    private void Update()
    {
        read();
    }
    private void read()
    {
        // Periksa apakah objek ini memiliki parent
        if (transform.parent != null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Matikan isKinematic jika Rigidbody ada
                rb.isKinematic = true;
                //Debug.Log($"Rigidbody pada {gameObject.name}, {rb.isKinematic} kinematic.");
            }
            // Ambil nama parent
            string parentName = transform.parent.name;
            //Debug.Log($"Nama parent dari {gameObject.name} adalah: {parentName}");

            // Periksa apakah parent memiliki parent (grandparent)
            if (transform.parent.parent != null)
            {
                string grandparentName = transform.parent.parent.name;
                //Debug.Log($"Nama grandparent dari {gameObject.name} adalah: {grandparentName}");
            }
            else
            {
                //Debug.Log($"Parent dari {gameObject.name} tidak memiliki grandparent.");
            }
        }
        else
        {
            //Debug.Log($"{gameObject.name} tidak memiliki parent.");

            // Periksa apakah objek memiliki komponen Rigidbody
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Matikan isKinematic jika Rigidbody ada
                rb.isKinematic = false;
                //Debug.Log($"Rigidbody pada {gameObject.name} tidak lagi kinematic.");
            }
            else
            {
                //Debug.LogWarning($"Rigidbody tidak ditemukan pada {gameObject.name}.");
            }
        }
    }
}