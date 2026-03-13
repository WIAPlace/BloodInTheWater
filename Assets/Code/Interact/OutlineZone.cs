using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 3/12/26
/// Purpose: Attaches an outline material at nearby interactables
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class OutlineZone : MonoBehaviour
{
    public float radius = 5f;
    public Material outline;
    public LayerMask interactableLayer;

    // This keeps track of objects currently outlined
    private HashSet<Renderer> activeHighlights = new HashSet<Renderer>();

    void Update()
    {
        // Find everything currently in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, interactableLayer);
        HashSet<Renderer> currentlyInRange = new HashSet<Renderer>();

        foreach (var col in hitColliders)
        {
            Renderer rend = col.GetComponent<Renderer>();
            if (rend == null) continue;

            currentlyInRange.Add(rend);

            // Add outline if not already
            if (!activeHighlights.Contains(rend))
            {
                AddMaterial(rend);
                activeHighlights.Add(rend);
            }
        }

        // Check list for objects not in radius
        List<Renderer> toRemove = new List<Renderer>();
        foreach (var rend in activeHighlights)
        {
            if (!currentlyInRange.Contains(rend))
            {
                RemoveMaterial(rend);
                toRemove.Add(rend);
            }
        }

        // Clean up list
        foreach (var rend in toRemove) activeHighlights.Remove(rend);
    }

    void AddMaterial(Renderer rend)
    {
        Material[] mats = rend.materials;
        Material[] newMats = new Material[mats.Length + 1];
        for (int i = 0; i < mats.Length; i++) newMats[i] = mats[i];

        newMats[newMats.Length - 1] = outline;
        rend.materials = newMats;
    }

    void RemoveMaterial(Renderer rend)
    {
        if (rend == null || rend.materials.Length <= 1) return;

        // Undo when outside of radius
        Material[] newMats = new Material[1];
        newMats[0] = rend.materials[0];
        rend.materials = newMats;
    }
}
