using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTileController : MonoBehaviour
{
    [SerializeField]
    private GameObject trackTilePrefab;
    [SerializeField]
    private Transform trackTileParent;

    [SerializeField] GameTimer timer;

    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private List<Transform> objectSpawnPoints;
    [SerializeField] [Range(1,100)] private int chanceForGhostToSpawn = 20;

    public List<GameObject> list_of_ghosts {get; private set;} 

    public int uncollected_ghosts = 0;

    private Vector3 respawnTrackTilePoint;
    private Vector3 despawnTrackTilePoint;

    private List<GameObject> trackTiles;

    [SerializeField] private AnimationCurve scrollSpeedCurve;
    [SerializeField] private float maximumScrollSpeed = 500.0f;

    private System.Random rnd; 



    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random(Guid.NewGuid().GetHashCode());
        list_of_ghosts = new List<GameObject>();
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
        float trackSpeed = scrollSpeedCurve.Evaluate((timer.roundTime - timer.currentTime) / timer.roundTime) * maximumScrollSpeed;
        foreach(GameObject trackTile in trackTiles)
        {
            trackTile.transform.position -= (Vector3.forward * trackSpeed * Time.deltaTime);
            if(trackTile.transform.position.z < despawnTrackTilePoint.z)
            {
                trackTile.transform.position = respawnTrackTilePoint - (despawnTrackTilePoint - trackTile.transform.position);
                TrackSpawned(trackTile.transform);
            }
        }

    }

    private void TrackSpawned(Transform _track_transform) {
        if (rnd.Next(101) <= chanceForGhostToSpawn && uncollected_ghosts <= 10) {
            Transform spawn_point = objectSpawnPoints[rnd.Next(objectSpawnPoints.Count)];
            GameObject ghost = Instantiate(ghostPrefab,spawn_point.transform);
            ghost.transform.parent = _track_transform;
            list_of_ghosts.Add(ghost);
            uncollected_ghosts += 1;
        }
    }

    public void ghostPickedUp() {
        uncollected_ghosts -= 1;
    }
}
