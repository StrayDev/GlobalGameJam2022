using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SyncLanes : MonoBehaviour
{
    [SerializeField] List<Transform> spawn_points;

    void Start() {
        for (int i = 0; i < spawn_points.Count; i++)
        {
            spawn_points[i].position = new Vector3(GameData.getLaneXPos(i),spawn_points[i].position.y,spawn_points[i].position.z);
        }
    }
}
