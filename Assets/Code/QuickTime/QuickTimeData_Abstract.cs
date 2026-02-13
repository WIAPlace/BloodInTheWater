using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/12/26
/// Purpose: This is an abstract for all quick time minigames things.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public abstract class QuickTimeData_Abstract : MonoBehaviour
{
    [SerializeField] [Tooltip("Length of the Bar")]
    protected float barLength;
    [SerializeField] [Tooltip("Area within that the player will need to hit")]
    protected float[] hitZone;
    [SerializeField] [Tooltip("How fast the player hit indicator should move")]
    protected float QTSpeed;
    [SerializeField] [Tooltip("How long should the allowable time be for this")]
    protected float QTLength;

    public QuickTimeData_Abstract(QuickTimeData_Abstract other)
    {
        this.barLength=other.barLength;
        this.hitZone = other.hitZone;
        this.QTSpeed = other.QTSpeed;
        this.QTLength = other.QTLength;
    }

}
