using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetMenuButtonOnActive : MonoBehaviour
{
    [SerializeField] GameObject firstSelectedOption;
    // Start is called before the first frame update
    void Start()
    {
        SetFirstButton(firstSelectedOption);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFirstButton(GameObject selectedOpt)
    {
        EventSystem.current.SetSelectedGameObject(selectedOpt);
    }
}
