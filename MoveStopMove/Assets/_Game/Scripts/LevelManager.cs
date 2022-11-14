using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Bot> botsInMap = new List<Bot>();

    public GameUnit botPrefab;

    [SerializeField] Transform centerPoint;
    [SerializeField] float sizeMap;
    [SerializeField] int numberBots;



    public void SpawnBot()
    {
        Bot bot = SimplePool.Spawn(botPrefab, NavMeshUtil.GetRandomPoint(centerPoint.position, sizeMap), Quaternion.identity) as Bot;
        bot.OnInit();
        botsInMap.Add(bot);
    }

    private void Awake()
    {
        for (int i = 0; i < numberBots; i++)
        {
            SpawnBot();
        }
    }

}
