using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishAggro_Abstract : MonoBehaviour
{
    [SerializeField] [Tooltip("That which the fish will be agro appon")]
    protected GameObject target;
    [SerializeField] [Tooltip("Speed of Movement in Aggro")]
    protected float aggroSpeed;
    [SerializeField] [Tooltip("Time to alert before it will do its agro thing")]
    private float warnTime;
    [SerializeField] [Tooltip("How Quickly they will rotate to target")]
    protected float rotationSpeed;


    public abstract void InitiateAggroState(); // Go into the State that makes them aggressive
    
    protected IEnumerator WindUp() // this is the wait time to initiate something.
    {
        yield return new WaitForSeconds(warnTime);
        FullAggro();
    }

    protected abstract void FullAggro(); // the actual attacking the thing they hate.
}
