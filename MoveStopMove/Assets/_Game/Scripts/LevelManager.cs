using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public List<Bot> botsInMap = new List<Bot>();

    public GameUnit botPrefab;

    [SerializeField] Transform centerPoint;
    [SerializeField] float sizeMap;
    [SerializeField] int numberBots;
    [SerializeField] int numberBotsTotal;

    public void PlayGame()
    {
        for (int i = 0; i < numberBotsTotal; i++)
        {
            SpawnBot();
        }
    }

    public void SpawnBot()
    {
        if (numberBots > 0)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, NavMeshUtil.GetRandomPoint(centerPoint.position, sizeMap), Quaternion.identity);
            bot.OnInit();
            botsInMap.Add(bot);
            UIManager.Instance.gamePlayPanel.targetIndicatior.Spawn();
            numberBots--;
        }
        else
        {
            if (botsInMap.Count <= 0)
            {
                Win();
            }
        }
    }

    private void Awake()
    {
        PlayGame();
    }

    public void Win()
    {
        SimplePool.CollectAll();

    }

    public void Lost()
    {
        SimplePool.CollectAll();

    }
}
