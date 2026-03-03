using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSpawn : MonoBehaviour
{
    [SerializeField]GameObject thingToSpawn; 
    [SerializeField]float afterSeconds;
    public void Start()
    {
        StartCoroutine(delayedSpawnEnemies());
        thingToSpawn.SetActive(false);
    } 
    private IEnumerator delayedSpawnEnemies()
    {
        yield return new WaitForSeconds(afterSeconds);
        thingToSpawn.SetActive(true);
    }
}
