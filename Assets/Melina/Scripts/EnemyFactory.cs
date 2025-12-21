using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject simpleEnemyPrefab;
    public GameObject fastEnemyPrefab;

    public GameObject MakeEnemy(string name)
    {
        if (name == "Simple")
        {
            return Instantiate(simpleEnemyPrefab);
        }

        else if (name == "Fast")
        {
            return Instantiate(fastEnemyPrefab);
        }

        return null;
    }
}
