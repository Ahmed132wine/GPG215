using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyFactory : MonoBehaviour
{
    [Serializable]
    public class EnemyEntry
    {
        public EnemyType type;
        public GameObject prefab;
    }

    [Header("Enemy Prefab Mapping")]
    [SerializeField] private List<EnemyEntry> enemyPrefabs = new List<EnemyEntry>();

    private Dictionary<EnemyType, GameObject> _map;

    private void Awake()
    {
        _map = new Dictionary<EnemyType, GameObject>();
        foreach (var entry in enemyPrefabs)
        {
            if (entry.prefab == null) continue;

            if (!_map.ContainsKey(entry.type))
                _map.Add(entry.type, entry.prefab);
        }
    }

    // ✅ Factory Method
    public GameObject CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation)
    {
        if (_map == null) Awake();

        if (!_map.TryGetValue(type, out var prefab) || prefab == null)
        {
            Debug.LogError($"EnemyFactory: Missing prefab mapping for enemy type: {type}");
            return null;
        }

        return Instantiate(prefab, position, rotation);
    }
}
