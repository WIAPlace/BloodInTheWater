using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishOnBarrel : MonoBehaviour
{
    [SerializeField] private GameObject[] carcasses;
    private int currentKey=0;
    private int keyHolder; // used for holding what will become the new current key;


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject fish in carcasses)
        {
            fish.SetActive(false);
        }
        currentKey = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeKey(int newKey)
    {
        if(newKey >= carcasses.Length) newKey = -1; // set it so it will become 0

        keyHolder = newKey+1; // 0 is empty; 
    }

    public void SetFishDown()
    {
        if (carcasses[currentKey].activeSelf)
        {   // turn off previous one
            carcasses[currentKey].SetActive(false);
        }
        
        currentKey = keyHolder;

        if (!carcasses[currentKey].activeSelf)
        {   // turn on new one
            carcasses[currentKey].SetActive(true);
        }
    }
}
