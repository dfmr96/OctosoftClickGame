using Photon.Pun;
using UnityEngine;

public class MouseTrail : MonoBehaviourPun
{
    [Header("Trail settings")]
    public Color trailColor = new Color(1, 0, 0.38f);
    public float distanceFromCamera = 5;
    public float startWidth = 0.1f;
    public float endWidth = 0f;
    public float trailTime = 0.24f;
    [Space]
    Transform trailTransform;
    Camera thisCamera;
    public GameObject trailObj;
    TrailRenderer trail;
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        trailTransform = trailObj.transform;
        trail = trailObj.GetComponent<TrailRenderer>();
        if (this.name == "Player1Cam")
        {
            trailObj.tag = "Player1";
        }
        else
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