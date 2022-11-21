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
    private float energyTime;

    public static List<GameObject> SpawnedPipes;

    void Start()
    {
        PipeTime = 0;
        energyTime = 0;
        SpawnedPipes = new List<GameObject>();
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
                energyTime = PipeTime / 2;
            }
        }
        if (energyTime > 0)
        {
            energyTime -= Time.deltaTime;
            if (energyTime < 0)
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
            Quaternion.identity));
    }

    void SpawnEnergy()
    {
        GameObject.Instantiate(energy, transform.position +
            Vector3.up * Random.Range(-Bird.PipeShift, Bird.PipeShift),
            Quaternion.identity);
    }
}