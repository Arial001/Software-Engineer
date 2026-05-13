using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractStartG3 : MonoBehaviour
{
    [Header("Script References")]
    public SelectionManagerG3 selectionManager;
    public RaycastVisualizer raycastVisualizer;
    public Datagame3 dataManager;
    public GeneratorGame3 GeneratorGame3;
    // Referensi ini akan diambil dari objek yang terkena raycast
    // public RotationObjectG3 RotationObjectG3; 

    [Header("Renderer Settings")]
    [Tooltip("Index Mesh Renderer yang akan digunakan saat objek diinteraksi.")]
    public int interactRendererIndex = 1;
    [Header("Target forbiddend Reparent")]
    public List<GameObject> forbiddendReparents;

    //[Header("Key Settings")]
    //public KeyCode interactKey = KeyCode.I;

    private void Update()
    {
        if (selectionManager == null || raycastVisualizer == null || dataManager == null)
        {
            //.LogWarning("Satu atau lebih referensi belum diatur!");
            return;
        }

        Debug.DrawRay(raycastVisualizer.transform.position, raycastVisualizer.transform.forward * 10f, Color.red);
        GameObject objectInFront = GetObjectInFront();
        if (objectInFront != null)
        {
            if (forbiddendReparents != null && !forbiddendReparents.Contains(objectInFront))
            {
                InteractableObject interactable = objectInFront.GetComponent<InteractableObject>();
                if (interactable != null && selectionManager.IsInteractable(interactable))
                {
                    ChangeBlockAppearance(objectInFront);
                    StartCoroutine(Reload());

                    // --- Kode baru: Panggil rotasi hanya pada objek ini ---
                    RotationObjectG3 rotationScript = objectInFront.GetComponent<RotationObjectG3>();
                    if (rotationScript != null)
                    {
                        rotationScript.ToggleRotation();
                    }
                    else
                    {
                        //Debug.LogWarning("Objek yang disorot tidak memiliki skrip RotationObjectG3.");
                    }
                    // --- Akhir kode baru ---

                    Debug.Log("Material objek berhasil diubah!");
                }
                else
                {
                    Debug.Log("Objek bukan InteractableObject yang valid dalam daftar SelectionManager");
                }
            }
            
        }
        //// Logika untuk interaksi objek tunggal (seperti sebelumnya)
        //if (Input.GetKeyDown(interactKey))
        //{
            
        //}
    }

    private GameObject GetObjectInFront()
    {

        Ray ray = new Ray(raycastVisualizer.transform.position, raycastVisualizer.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastVisualizer.raycastLayerMask))
        {

            if (hit.transform != null)
            {
                Debug.Log($"Objek di depan: {hit.transform.name}");
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Mengubah MeshRenderer dari sebuah balok.
    /// </summary>
    /// <param name="blockObject">Balok yang akan diubah.</param>
    private void ChangeBlockAppearance(GameObject blockObject)
    {
        MeshRenderer mr = blockObject.GetComponent<MeshRenderer>();

        if (mr == null)
        {
            //Debug.LogWarning("Objek tidak memiliki komponen MeshRenderer.");
            return;
        }

        if (dataManager.blockRenderers.Count > interactRendererIndex)
        {
            mr.sharedMaterial = dataManager.blockRenderers[interactRendererIndex].sharedMaterial;
        }
        else
        {
            //Debug.LogWarning("Indeks renderer tidak valid!");
        }
    }
    private IEnumerator Reload()
    {
        GeneratorGame3.UpdateWordPlacementData();
        yield return new WaitForSeconds(0.1f);
        //GeneratorGame3.PopulateAvailableRendererNames();
        yield return new WaitForSeconds(0.1f);
        GeneratorGame3.VerifMeshRendererPerPlayer(interactRendererIndex);
        yield return new WaitForSeconds(0.1f);
        dataManager.DisableFoundWordBlocks();
        
    }
}