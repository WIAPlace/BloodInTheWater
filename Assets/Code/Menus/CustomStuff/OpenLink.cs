using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Script to open a link. attach to a button to click n stuff.
public class OpenLink : MonoBehaviour
{
    public string websiteURL;

    public void OpenWebsite()
    {   // open website url.
        Application.OpenURL(websiteURL);
    }
}
