using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishInstanceMenu : MonoBehaviour
{
    [SerializeField][Tooltip("Fish Holder SO")]
    private FishHolderSO fishHolder;
    [SerializeField][Tooltip("TextMesh Pro Text")]
    private Text text;
    private int fishHoldLen;
    // Start is called before the first frame update
    void Start()
    {
        fishHoldLen = fishHolder.GetLength();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
