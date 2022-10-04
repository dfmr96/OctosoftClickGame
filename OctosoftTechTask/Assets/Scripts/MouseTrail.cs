using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MouseTrail : MonoBehaviourPun
{
    public Color trailColor = new Color(1, 0, 0.38f);
    public float distanceFromCamera = 5;
    public float startWidth = 0.1f;
    public float endWidth = 0f;
    public float trailTime = 0.24f;

    Transform trailTransform;
    Camera thisCamera;

    public GameObject trailObj;
    TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();

        trailTransform = trailObj.transform;
        trail = trailObj.GetComponent<TrailRenderer>();
        
        if (this.name == "Player1Cam")
        {
            trailObj.tag = "Player1";
        } else
        {
            trailObj.tag = "Player2";
        }
        trail.time = -1f;
        MoveTrailToCursor(Input.mousePosition);
        trail.time = trailTime;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;
        trail.numCapVertices = 2;
        trail.sharedMaterial = new Material(Shader.Find("Unlit/Color"));
        trail.sharedMaterial.color = trailColor;

        if (trailObj.tag == "Player2" && PhotonNetwork.IsMasterClient)
        {
            trailObj.GetPhotonView().TransferOwnership(PhotonNetwork.PlayerList[1]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTrailToCursor(Input.mousePosition);
    }
    void MoveTrailToCursor(Vector3 screenPosition)
    {
        if (trailObj.GetPhotonView().IsMine)
        {
        trailTransform.position = thisCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
        }
    }
}