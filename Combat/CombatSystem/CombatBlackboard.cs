
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class CombatBlackboard 
{
    public static ITarget Player;
    public static List<ITarget> Companions = new List<ITarget>();
    public static List<ITarget> Enemies =  new List<ITarget>();

    public static List<ITarget> AllTargets()
    {
        List<ITarget> combinedList = Enemies.Concat(Companions).ToList();
        combinedList.Add(Player);
        return combinedList;
    }

 
    public static void AddCompanion(ITarget companion)
    {
        Companions.Add(companion);
    }

    public static void AddEnemy(ITarget enemy)
    {
        Enemies.Add(enemy);
        Enemies = Enemies.OrderByDescending(x => x.strength).ToList();

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].gameObject.transform.SetSiblingIndex(i);
        }
    }

    public static void RemoveCompanion(ITarget companion)
    {
        Companions.Remove(companion);
    }

    public static void RemoveEnemy(ITarget enemy)
    {
        Enemies.Remove(enemy);
    }


    public static void ClearEnemies()
    {
        if(Enemies.Count == 0) return;
        foreach(var enemy in Enemies.ToList())
        {
            Enemies.Remove(enemy);
            enemy.Die();
        }
    }

    public static void ClearCompanions()
    {
        if(Companions.Count == 0) return;
        foreach(var companion in Companions.ToList())
        {
            Companions.Remove(companion);
            companion.Die();
        }
    }

    public static void ClearPlayer()
    {
        Player = null;
    }

    public static void ClearBlackboard()
    {
        ClearEnemies();
        ClearCompanions();
        ClearPlayer();
    }
}