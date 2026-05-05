using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCurrentLevel : MonoBehaviour
{
    [SerializeField] int lvl;

    // Start is called before the first frame update
    void Start()
    { // on start set this level as unlocked
        GameManager.Instance.unlocks.SaveLevelData(0,lvl);
    }
}
