using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;
    private float pipeSpawnTime = 5;
    public static float PipeTime;
    public static List<GameObject> SpawnedPipes;

    void Start()
    {
        PipeTime = 0;
        SpawnedPipes = new List<GameObject>();
    }

    void LateUpdate()
    {
        PipeTime -= Time.deltaTime;
        if (PipeTime < 0)
        {
            PipeTime = pipeSpawnTime;
            SpawnPipe();
        }
    }

    void SpawnPipe()
    {
        SpawnedPipes.Add(
            Instantiate(pipe, transform.position +
            Vector3.up * Random.Range(-Bird.PipeShift, Bird.PipeShift),
            Quaternion.identity));
    }
}