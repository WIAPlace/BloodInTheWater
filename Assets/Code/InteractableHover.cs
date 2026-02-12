using UnityEngine;
using UnityEngine.UI;
/// 
/// Author: Marsahll Turner
/// Created: 2/12/26
/// Purpose: Have a Crosshair Appear When Hovering Over Interactables
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class InteractableHover : MonoBehaviour
{
    public Image crosshairImage;
    public float detectionRange = 100f;
    public LayerMask interactableLayer;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange, interactableLayer))
        {
            if (!crosshairImage.enabled)
            {
                crosshairImage.enabled = true;
            }
        }
        else
        {
            if (crosshairImage.enabled)
            {
                crosshairImage.enabled = false;
            }
        }
    }
}