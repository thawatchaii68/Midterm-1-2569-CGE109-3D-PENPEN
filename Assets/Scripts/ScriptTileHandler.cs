using UnityEngine;

public class ScriptTileHandler : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstaclePrefabs;
    public GameObject foodPrefab; // Prefab ของอาหาร

    [Range(0, 1)] public float obstacleChance = 0.4f; // โอกาสเกิดสิ่งกีดขวาง
    [Range(0, 1)] public float foodChance = 0.4f;     // โอกาสเกิดอาหาร

    public void Populate(bool allowObstacles)
    {
        foreach (Transform pt in spawnPoints)
        {
            float roll = Random.value;

            if (allowObstacles && roll < obstacleChance && obstaclePrefabs.Length > 0)
            {
                Spawn(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], pt);
            }
            else if (roll < obstacleChance + foodChance && foodPrefab != null)
            {
                Spawn(foodPrefab, pt);
            }
        }
    }

    void Spawn(GameObject prefab, Transform pt)
    {
        Instantiate(prefab, pt.position, pt.rotation, transform);
    }
}