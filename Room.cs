using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Direction
{
    DL,
    UL,
    UR,
    DR
}


public class Room : MonoBehaviour {

    public GameObject enemy;

    [Header("Are there enemies in these directions?")]
    public bool enemyDL = false;
    public bool enemyUL = false;
    public bool enemyUR = false;
    public bool enemyDR = false;

    [Header("The beat of the measure that the corresponding enemy will attack on")]
    public int DLBeat = 0;
    public int ULBeat = 0;
    public int URBeat = 0;
    public int DRBeat = 0;

    [Header("Spawn points within room")]
    public GameObject spawnDL = null;
    public GameObject spawnUL = null;
    public GameObject spawnUR = null;
    public GameObject spawnDR = null;

    [Header("Colors for enemies")]
    public Color redEnemy;
    public Color yellowEnemy;
    public Color blueEnemy;
    public Color greenEnemy;

    Vector3 center;
    List<GameObject> enemies;
    int previousEnemiesCount = 0;
    bool spawnedOnce = false;

    private void Start()
    {
        center = transform.position;
        enemies = new List<GameObject>();
        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);
    }

    // Update is called once per frame
    void Update ()
    {
        if (GetEnemyCount() <= 0 && previousEnemiesCount > 0)
        {
            StartCoroutine(PlayerCombat.Victory());
        }
        previousEnemiesCount = GetEnemyCount();
	}
    
    public IEnumerator GenerateEnemies()
    {
        if (!spawnedOnce)
        {
            spawnedOnce = true;
            if (enemyDL || enemyUL || enemyUR || enemyDR)
            {
                StartCoroutine(PlayerCombat.EnterCombat());
            }


            if (enemyDL)
            {
                enemies[0] = Instantiate(enemy, spawnDL.transform.position, spawnDL.transform.rotation);
                enemies[0].GetComponent<Enemy>().SetBeat(DLBeat);
                enemies[0].GetComponent<Enemy>().thisRoom = this;
                enemies[0].GetComponentInChildren<SpriteRenderer>().color = redEnemy;
                enemies[0].GetComponentInChildren<SpriteRenderer>().flipX = true;
                enemies[0].SetActive(false);
            }

            if (enemyUL)
            {
                enemies[1] = Instantiate(enemy, spawnUL.transform.position, spawnUL.transform.rotation);
                enemies[1].GetComponent<Enemy>().SetBeat(ULBeat);
                enemies[1].GetComponent<Enemy>().thisRoom = this;
                enemies[1].GetComponentInChildren<SpriteRenderer>().color = yellowEnemy;
                enemies[1].GetComponentInChildren<SpriteRenderer>().flipX = true;
                enemies[1].SetActive(false);
            }

            if (enemyUR)
            {
                enemies[2] = Instantiate(enemy, spawnUR.transform.position, spawnUR.transform.rotation);
                enemies[2].GetComponent<Enemy>().SetBeat(URBeat);
                enemies[2].GetComponent<Enemy>().thisRoom = this;
                enemies[2].GetComponentInChildren<SpriteRenderer>().color = blueEnemy;
                enemies[2].GetComponentInChildren<SpriteRenderer>().flipX = false;
                enemies[2].SetActive(false);
            }

            if (enemyDR)
            {
                enemies[3] = Instantiate(enemy, spawnDR.transform.position, spawnDR.transform.rotation);
                enemies[3].GetComponent<Enemy>().SetBeat(DRBeat);
                enemies[3].GetComponent<Enemy>().thisRoom = this;
                enemies[3].GetComponentInChildren<SpriteRenderer>().color = greenEnemy;
                enemies[3].GetComponentInChildren<SpriteRenderer>().flipX = false;
                enemies[3].SetActive(false);
            }
            yield return new WaitForSeconds(SongManager.instance.nextBeatTime - SongManager.instance.currentDSPTime);
            if (enemyDL)
                enemies[0].SetActive(true);
            if (enemyUL)
                enemies[1].SetActive(true);
            if (enemyUR)
                enemies[2].SetActive(true);
            if (enemyDR)
                enemies[3].SetActive(true);
        }
    }

    public GameObject GetEnemy(Direction direction)
    {
        switch (direction)
        {
            case Direction.DL:
                return enemies[0];
            case Direction.UL:
                return enemies[1];
            case Direction.UR:
                return enemies[2];
            case Direction.DR:
                return enemies[3];
        }
        return null;
    }

    public Vector3 GetCenter()
    {
        return center;
    }

    public int GetEnemyCount()
    {
        int count = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                count++;
        }
        return count;
    }
}
