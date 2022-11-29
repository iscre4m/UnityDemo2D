using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;
    [SerializeField]
    private GameObject energy;
    [SerializeField]
    private GameObject bigHeart;

    private const float PIPE_SPAWN_TIME = 3;
    private const float PIPE_DELTA_TIME = 3;
    private const int PIPE_SHIFT = 2;

    private float pipeTime;
    private float energyTime;
    private float bigHeartTime;

    private List<GameObject> spawnedPipes;
    private List<GameObject> spawnedEnergy;
    private List<GameObject> spawnedHearts;

    void Start()
    {
        pipeTime = 0;
        energyTime = 0;
        bigHeartTime = 0;

        spawnedPipes = new List<GameObject>();
        spawnedEnergy = new List<GameObject>();
        spawnedHearts = new List<GameObject>();
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
            if (Random.value < .1f)
            {
                bigHeartTime = pipeTime / 2.5f;
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

        if (bigHeartTime > 0)
        {
            bigHeartTime -= Time.deltaTime;
            if (bigHeartTime < 0)
            {
                SpawnBigHeart();
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

    void SpawnBigHeart()
    {
        spawnedHearts.Add(
            Instantiate(bigHeart, transform.position +
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
        foreach (var heart in spawnedHearts)
        {
            Destroy(heart);
        }

        spawnedPipes.Clear();
        spawnedEnergy.Clear();
        spawnedHearts.Clear();

        pipeTime = 0;
        energyTime = 0;
        bigHeartTime = 0;
    }
}
