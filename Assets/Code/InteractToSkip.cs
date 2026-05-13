using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractToSkip : MonoBehaviour
{
    [SerializeField] TransistionScene scene;
    [SerializeField] InputReader input;
    
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
        scene.StartGame();
        input.AnyButtonEventUI -= HandelInteract;
    }
}
