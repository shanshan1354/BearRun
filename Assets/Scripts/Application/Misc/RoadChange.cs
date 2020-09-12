using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChange : MonoBehaviour
{

    GameObject roadNow;
    GameObject roadNext;
    GameObject parent;

    void Start()
    {
        parent = new GameObject("Road");
        parent.transform.position = Vector3.zero;

        roadNow = Game.Instance.objectPool.Spawn("Pattern_1", parent.transform);
        roadNext = Game.Instance.objectPool.Spawn("Pattern_2", parent.transform);
        roadNext.transform.position = roadNow.transform.position + new Vector3(0, 0, 160);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.road)
        {
            Game.Instance.objectPool.UnSpawn(other.gameObject);
            SpawnNewRoad();
        }
    }
    //生成新的道路
    void SpawnNewRoad()
    {
        string i = Random.Range(1, 5).ToString();
        roadNow = roadNext;
        roadNext = Game.Instance.objectPool.Spawn("Pattern_"+i, parent.transform);
        roadNext.transform.position = roadNow.transform.position + new Vector3(0, 0, 160);
    }
}
