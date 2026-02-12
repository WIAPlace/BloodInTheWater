using UnityEngine;
using UnityEngine.UI;

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