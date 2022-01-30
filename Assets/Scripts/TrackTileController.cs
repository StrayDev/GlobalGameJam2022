using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackTileController : MonoBehaviour
{
    [SerializeField]
    private GameObject trackTilePrefab;
    [SerializeField]
    private Transform trackTileParent;

    [SerializeField] GameTimer timer;

    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private List<Transform> objectSpawnPoints;
    [SerializeField] [Range(1,100)] private int chanceForGhostToSpawn = 20;
    [SerializeField] [Range(1,100)] private int chanceForObstacleToSpawn = 20;
    
    [SerializeField] private float timeTillObstaclesSpawn = 30;

    public List<GameObject> list_of_ghosts {get; private set;} 
    public List<GameObject> list_of_obstacles {get; private set;}

    public int uncollected_ghosts = 0;

    private Vector3 respawnTrackTilePoint;
    private Vector3 despawnTrackTilePoint;

    private List<GameObject> trackTiles;

    [SerializeField] private AnimationCurve scrollSpeedCurve;
    [SerializeField] private float maximumScrollSpeed = 500.0f;

    private System.Random rnd; 

    private bool canObstacleSpawn = false;

    [SerializeField] UnityEvent ghostSpawned = default;
    [SerializeField] UnityEvent ObstacleSpawned = default;

    private bool game_over = false;
    private float track_speed = 0;



    // Start is called before the first frame update
    void Start()
    {
        GameData.resetAllScore();
        rnd = new System.Random(Guid.NewGuid().GetHashCode());
        list_of_ghosts = new List<GameObject>();
        list_of_obstacles = new List<GameObject>();
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

        StartCoroutine(spawnObstaclesLater(timeTillObstaclesSpawn));
    }

    // Update is called once per frame
    void Update()
    {
        if (!game_over) {
            track_speed = scrollSpeedCurve.Evaluate((timer.roundTime - timer.currentTime) / timer.roundTime) * maximumScrollSpeed;
        }

        foreach(GameObject trackTile in trackTiles)
        {
            trackTile.transform.position -= (Vector3.forward * track_speed * Time.deltaTime);
            if(trackTile.transform.position.z < despawnTrackTilePoint.z)
            {
                trackTile.transform.position = respawnTrackTilePoint - (despawnTrackTilePoint - trackTile.transform.position);
                destroyAttachtedObsticles(trackTile.transform);
                TrackSpawned(trackTile.transform);
            }
        }

    }

    private void destroyAttachtedObsticles(Transform _track_transform) {
        List<Obstacle> train_obs = new List<Obstacle>(_track_transform.GetComponentsInChildren<Obstacle>());
        train_obs.ForEach(_t => _t.killSelf());
    }

    private IEnumerator spawnObstaclesLater(float _time_till_spawn) {
        yield return new WaitForSeconds(_time_till_spawn);
        canObstacleSpawn = true;
    }    

    private void TrackSpawned(Transform _track_transform) {
        if (rnd.Next(101) <= chanceForGhostToSpawn && uncollected_ghosts <= 10) {
            Transform spawn_point = objectSpawnPoints[rnd.Next(objectSpawnPoints.Count)];
            GameObject ghost = Instantiate(ghostPrefab,spawn_point.transform);
            ghost.transform.parent = _track_transform;
            list_of_ghosts.Add(ghost);
            uncollected_ghosts += 1;
            ghostSpawned?.Invoke();

        } else if (rnd.Next(101) <= chanceForObstacleToSpawn && canObstacleSpawn && list_of_obstacles.Count <= 20) {
            Transform spawn_point = objectSpawnPoints[rnd.Next(objectSpawnPoints.Count)];
            GameObject obs = Instantiate(obstaclePrefab,spawn_point.transform);
            obs.transform.parent = _track_transform;
            list_of_obstacles.Add(obs);
            ObstacleSpawned?.Invoke();
        }
    }

    public void startEndGameSequence() {
        game_over = true;
        StartCoroutine(slowDownTrains());
    }

    private IEnumerator slowDownTrains() {
        float original_speed = track_speed;
        float duration = 2;
        float e_time = 0;
        while (e_time < duration)
        {
            track_speed = Mathf.Lerp(original_speed,0,e_time/duration);
            e_time += Time.deltaTime;
            yield return null;
        }
        track_speed = 0;
    }

    public void ghostPickedUp() {
        uncollected_ghosts -= 1;
    }
}
