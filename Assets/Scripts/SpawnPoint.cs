using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;
    [SerializeField]
    private GameObject energy;

    private float pipeSpawnTime = 3;
    private float pipeDeltaTime = 3;

    public static float PipeTime;
    public static float EnergyTime;

    public static List<GameObject> SpawnedPipes;
    public static List<GameObject> SpawnedEnergy;

    void Start()
    {
        PipeTime = 0;
        EnergyTime = 0;
        SpawnedPipes = new List<GameObject>();
        SpawnedEnergy = new List<GameObject>();
    }

    void LateUpdate()
    {
        PipeTime -= Time.deltaTime;
        if (PipeTime < 0)
        {
            PipeTime = pipeSpawnTime + pipeDeltaTime * (1 - GameMenu.GameDifficulty);
            SpawnPipe();
            if (Random.value < .33f)
            {
                EnergyTime = PipeTime / 2;
            }
        }
        if (EnergyTime > 0)
        {
            EnergyTime -= Time.deltaTime;
            if (EnergyTime < 0)
            {
                SpawnEnergy();
            }
        }
    }

    void SpawnPipe()
    {
        SpawnedPipes.Add(
            Instantiate(pipe, transform.position +
            Vector3.up * Random.Range(-Bird.PipeShift, Bird.PipeShift),
            Quaternion.identity)
        );
    }

    void SpawnEnergy()
    {
        SpawnedEnergy.Add(
            Instantiate(energy, transform.position +
            Vector3.up * Random.Range(-Bird.PipeShift, Bird.PipeShift),
            Quaternion.identity)
        );
    }
}
