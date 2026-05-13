using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentNamingScript : MonoBehaviour
{
    [Header("Referensi InteractableObject")]
    public InteractableObject interactableObject; // Referensi ke script InteractableObject di Parent

    private void Start()
    {
        if (interactableObject == null)
        {
            Debug.LogError("Referensi InteractableObject belum diatur!");
            return;
        }

        // Panggil fungsi untuk memeriksa anak yang aktif dan update ItemName
        UpdateItemNameFromActiveChild();
    }
    private void Update()
    {
       
    }

    // Fungsi untuk memeriksa siapa saja Child yang aktif dan update ItemName di InteractableObject
    private void UpdateItemNameFromActiveChild()
    {
        // Temukan semua Child yang merupakan Anak dari Parent ini
        // Temukan semua Child yang merupakan Anak dari Parent ini
        foreach (Transform child in transform)
        {
            // Dapatkan status hidup/mati dan nama anak
            bool isActive = child.gameObject.activeInHierarchy;
            string childName = child.name;

            // Tampilkan nama dan status anak di konsol
            //Debug.Log($"Nama Anak: {childName}, Status: {(isActive ? "Hidup" : "Mati")}");

            // Cek apakah Child tersebut aktif
            if (isActive)
            {
                // Dapatkan nama dari Child tersebut melalui script InteractableObject miliknya
                InteractableObject childInteractable = child.GetComponent<InteractableObject>();
                if (childInteractable != null)
                {
                    string activeChildName = childInteractable.GetItemName();
                    //Debug.Log($"Anak aktif ditemukan: {activeChildName}"); // Tampilkan nama anak yang aktif di konsol

                    // Gantikan ItemName dari Parent dengan ItemName dari Child
                    interactableObject.ItemName = activeChildName;
                    //Debug.Log($"ItemName di Parent telah diganti menjadi: {interactableObject.ItemName}");

                    break; // Hentikan loop setelah menemukan satu anak aktif
                }
            }
        }

    }
}
