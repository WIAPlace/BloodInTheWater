using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInHandsCamMove : MonoBehaviour
{
    [SerializeField][Tooltip("First Person Camera")]
    private Transform cameraRot;
    [SerializeField][Tooltip("Holder of item meshes")]
    private GameObject inHands;

    [SerializeField][Tooltip("Speed To Rotate")]
    private float rotSpeed = 50f;

    void Update()
    {
        Quaternion targetRot = cameraRot.rotation;

        inHands.transform.rotation = Quaternion.RotateTowards(inHands.transform.rotation,targetRot,rotSpeed*Time.deltaTime);
    }
}
