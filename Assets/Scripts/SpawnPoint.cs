using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;
    [SerializeField]
    private GameObject energy;

    private const float PIPE_SPAWN_TIME = 3;
    private const float PIPE_DELTA_TIME = 3;
    private const int PIPE_SHIFT = 2;

    private float pipeTime;
    private float energyTime;

    private List<GameObject> spawnedPipes;
    private List<GameObject> spawnedEnergy;

    void Start()
    {
        pipeTime = 0;
        energyTime = 0;
        spawnedPipes = new List<GameObject>();
        spawnedEnergy = new List<GameObject>();
    }

    void LateUpdate()
    {
        pipeTime -= Time.deltaTime;
        if (pipeTime < 0)
        {
            pipeTime = PIPE_SPAWN_TIME + PIPE_DELTA_TIME * (1 - GameMenu.GameDifficulty);
            SpawnPipe();
            if (Random.value < .33f)
            {
                energyTime = pipeTime / 2;
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
        spawnedPipes.Add(
            Instantiate(pipe, transform.position +
            Vector3.up * Random.Range(-PIPE_SHIFT, PIPE_SHIFT),
            Quaternion.identity)
        );
    }

    void SpawnEnergy()
    {
        spawnedEnergy.Add(
            Instantiate(energy, transform.position +
            Vector3.up * Random.Range(-PIPE_SHIFT, PIPE_SHIFT),
            Quaternion.identity)
        );
    }

    public void Clear()
    {
        foreach (var pipe in spawnedPipes)
        {
            Destroy(pipe);
        }
        foreach (var energy in spawnedEnergy)
        {
            Destroy(energy);
        }

        spawnedPipes.Clear();
        spawnedEnergy.Clear();

        pipeTime = 0;
        energyTime = 0;
    }
}
