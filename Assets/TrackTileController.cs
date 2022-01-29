using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTileController : MonoBehaviour
{
    [SerializeField]
    private GameObject trackTilePrefab;
    [SerializeField]
    private Transform trackTileParent;

    private Vector3 respawnTrackTilePoint;
    private Vector3 despawnTrackTilePoint;

    private List<GameObject> trackTiles;

    public float trackScrollSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        trackTiles = new List<GameObject>();
        int i;
        for (i = 0; i < trackTileParent.childCount; ++i)
        {
            GameObject trackPiece = trackTileParent.GetChild(i).gameObject;
            trackTiles.Add(trackPiece);
        }

        //This assumes the furthest forward track piece is the lowest in the hierarchy, which may not be safe
        respawnTrackTilePoint = trackTiles[0].transform.position;
        despawnTrackTilePoint = trackTiles[i-1].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject trackTile in trackTiles)
        {
            trackTile.transform.position -= (Vector3.forward * trackScrollSpeed * Time.deltaTime);
            if(trackTile.transform.position.z < despawnTrackTilePoint.z)
            {
                trackTile.transform.position = respawnTrackTilePoint - (despawnTrackTilePoint - trackTile.transform.position);
            }
        }

    }
}
