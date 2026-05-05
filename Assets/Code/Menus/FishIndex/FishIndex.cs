using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishIndex : MonoBehaviour
{
    [SerializeField] private Unlocks unlocks;
    [SerializeField] private FishHolderSO fishHolder;
    [SerializeField] private FishImagesSO fishImages;
    [SerializeField] private TMP_Text[] fishTypes;
    [SerializeField] private TMP_Text[] fishSizes;
    [SerializeField] private Image[] images;
    //[SerializeField] private TMP_Text[] largestCaughtText;

    int currentPage = 0;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        OpenPage(currentPage);    
    }

    public void TurnPage(bool nextPage)
    {
        
        if(nextPage) {
            if((currentPage+1)*2<19)OpenPage(currentPage+1);
        }
        else if(currentPage>0) OpenPage(currentPage-1);
    }


    public void OpenPage(int page)
    {
        currentPage = page;
        
        int pageIndex = page*2;

        //Debug.Log(currentPage);
        //Debug.Log(pageIndex);

        // left side
        if(unlocks.LoadFishData(pageIndex)!=0){ 
            // set the fish names to their respective ones
            fishTypes[0].text = fishHolder.GetFish(pageIndex).name;
            images[0].sprite = fishImages.GetFishImage(pageIndex);
            // set size to larges caught
            fishSizes[0].text = (Mathf.Round(unlocks.LoadFishData(pageIndex) * 10)/10).ToString()+" Lbs";
        }
        else
        {
            // set the fish names to nothing
            fishTypes[0].text = "?????";
            images[0].sprite = fishImages.GetFishImage(-1);
            // set size to nothing
            fishSizes[0].text = "0 Lbs";
        }
        // right side 
        if(pageIndex+1<19 && unlocks.LoadFishData(pageIndex+1)!=0){
            // set the fish names to their respective ones
            fishTypes[1].text = fishHolder.GetFish(pageIndex+1).name;
            images[1].sprite = fishImages.GetFishImage(pageIndex+1);
            // set size to larges caught
            fishSizes[1].text =(Mathf.Round(unlocks.LoadFishData(pageIndex+1) * 10)/10).ToString()+" Lbs";
        }
        else if(pageIndex+1<19) // if its still within range just not unlocked
        {
            // set the fish names to their respective ones
            fishTypes[1].text = "?????";
            images[1].sprite = fishImages.GetFishImage(-1);
            // set size to larges caught
            fishSizes[1].text = "0 Lbs";
        }
        else
        {
            // set the fish names to nothing
            fishTypes[1].text = "";
            images[1].sprite = fishImages.GetFishImage(-1);
            // set size to nothing
            fishSizes[1].text = "";
        }


    }

    
}
