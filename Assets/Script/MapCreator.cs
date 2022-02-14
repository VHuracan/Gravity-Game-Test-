using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapCreator : MonoBehaviour
{
    [Header("Platform size")]
    [SerializeField] private float platformSizeMin;
    [SerializeField] private float platformSizeMax;
    [Header("Hole size")]
    [SerializeField] private float holeSizeMin;
    [SerializeField] private float holeSizeMax;
    [Header("Platform prefab")]
    [SerializeField] private GameObject platform;
    [Header("Platform up and down position")]
    [SerializeField] private Transform platformPosA;
    [SerializeField] private Transform platformPosB;
    [Header("Created platforms arrays")]
    public List<GameObject> arrayPlatformA = new List<GameObject>();
    public List<GameObject> arrayPlatformB = new List<GameObject>();

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log(player.name);
        CheckMapArray();
       
    }
    
    
    void Update()
    {
        CheckMapArray();
    }

    private void CheckMapArray()
    {
        if (arrayPlatformA.Count < 10 )
        {
           
                BlockCreator(platformPosA, arrayPlatformA, 15);
                BlockCreator(platformPosB, arrayPlatformB, 15);
            
        }

        if (arrayPlatformA.Any())
        {
            if (arrayPlatformA[arrayPlatformA.Count - 1].transform.position.x - player.transform.position.x < 100)
            {
                BlockCreator(platformPosA, arrayPlatformA, 15);

            }

            if (arrayPlatformA[0].transform.position.x - player.transform.position.x > 100)
            {
                BlockCleaner(arrayPlatformA, 5);

            }
        }

        if (arrayPlatformB.Any())
        {
            if (arrayPlatformB[arrayPlatformB.Count - 1].transform.position.x - player.transform.position.x < 100)
            {
                BlockCreator(platformPosB, arrayPlatformB, 15);
            }

            if (arrayPlatformB[0].transform.position.x - player.transform.position.x > 100)
            {
                BlockCleaner(arrayPlatformB, 5);
            }
        }

    }

    private void BlockCleaner(List<GameObject> createdBlocksList, int amount)
    {
        for (int i = 0; i < 5; i++)
        {
            Destroy(createdBlocksList[0].gameObject);
            createdBlocksList.Remove(createdBlocksList[0]);
        }
    }

    private void BlockCreator(Transform blockStandartPos, List<GameObject> createdBlocksList, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            {
                float prevblockSizeX = 0;
                float holeSize = 0;
                Vector3 prevBlockPosition = blockStandartPos.transform.position;
                float newBlockSize = Random.Range(platformSizeMin, platformSizeMax);

                if (createdBlocksList.Any())
                {
                    prevblockSizeX = createdBlocksList[createdBlocksList.Count - 1].GetComponent<MeshRenderer>().bounds
                        .size.x / 2;
                    prevBlockPosition = createdBlocksList[createdBlocksList.Count - 1].transform.position;
                    holeSize = Random.Range(holeSizeMin, holeSizeMax);
                }

                GameObject nbA = Instantiate(platform, transform);
                nbA.transform.localScale =
                    new Vector3(newBlockSize, nbA.transform.localScale.y, nbA.transform.localScale.z);
                float myblockSizeX = nbA.transform.GetComponent<MeshRenderer>().bounds.size.x / 2;
                nbA.transform.position =
                    prevBlockPosition + new Vector3(prevblockSizeX + holeSize + myblockSizeX, 0, 0);
                createdBlocksList.Add(nbA);

            }
        }
    }
    
    
}
