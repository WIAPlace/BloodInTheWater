using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCreditsEarly : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject options;
    // Start is called before the first frame update
    void Start()
    {
        input.SetUI();
        input.AnyButtonEventUI += HandelInteract;
    }

    void OnDestroy()
    {
        input.AnyButtonEventUI -= HandelInteract;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void HandelInteract()
    {
        if(credits!=null) Destroy(credits);
        if(!options.activeSelf) options.SetActive(true);
    }
}
