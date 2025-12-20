using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int> OnEnemyKilled;

    public static Action<int> OnPlayerDamaged;

    public static Action OnGameOver;
}
