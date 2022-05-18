using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    int alive = 0;
    public Text populationText;
    public Text evolutionText;
    public int evolution = 0;
    public float timeframe;
    public int populationSize;//creates population size
    public GameObject prefab;//holds bot prefab

    public int[] layers = new int[4] { 4, 8, 8, 1 };//initializing network to the right size

    [Range(0.0001f, 1f)] public float MutationChance = 0.01f;

    [Range(0f, 1f)] public float MutationStrength = 0.5f;

    [Range(0.1f, 10f)] public float Gamespeed = 1f;

    //public List<Bot> Bots;
    public List<NeuralNetwork> networks;
    public List<PlayerController> cars= new List<PlayerController>();

    void Start()// Start is called before the first frame update
    {
        if (populationSize % 2 != 0)
            populationSize = 50;//if population size is not even, sets it to fifty

        InitNetworks();
        CreateBots();
      //  InvokeRepeating("CreateBots", 0.1f, timeframe);//repeating function
    }

    public void Update()
    {
        
       


        alive = 0;
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i].alive)
            {
                alive++;
                
            }
        }
        
        if (alive == 0)
        {
            SortNetworks(); //this sorts networks and mutates them
            evolution++;
            foreach (var ball in cars)
            {
                Destroy(ball.gameObject);
            }
            cars.Clear();
            CreateBots();
        }
        populationText.text = alive.ToString();
        evolutionText.text = evolution.ToString();
    }



    public void InitNetworks()
    {
        networks = new List<NeuralNetwork>();
        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Load("Assets/Pre-trained.txt");//on start load the network save
            networks.Add(net);
        }
    }

    public void CreateBots()
    {
        Time.timeScale = Gamespeed;//sets gamespeed, which will increase to speed up training
        if (cars != null)
        {
            for (int i = 0; i < cars.Count; i++)
            {
           //     GameObject.Destroy(cars[i].gameObject);//if there are Prefabs in the scene this will get rid of them
            }

         //   evolution++;
         //   SortNetworks();//this sorts networks and mutates them
        }

       
        for (int i = 0; i < populationSize; i++)
        {
            PlayerController car = (Instantiate(prefab, new Vector3(0, 0f, -0), new Quaternion(0, 0, 0, 0))).GetComponent<PlayerController>();//create botes
            car.brain = networks[i];//deploys network to each learner
            cars.Add(car);
        }
        
    }

    public void SortNetworks()
    {
        
        for (int i = 0; i < populationSize; i++)
        {
            cars[i].UpdateFitness();//gets bots to set their corrosponding networks fitness
        }
        networks.Sort();
        
        for (int i = 0; i < 5; i++)
        {
            //   networks[i] = networks[i + populationSize / 2].copy(new NeuralNetwork(layers));
           // networks[i].Mutate((int)(1/MutationChance), MutationStrength/5);
           NeuralNetwork copy = new NeuralNetwork(cars[i].brain);
           Debug.Log(networks[i].fitness);
        }
      
        for (int i = 10; i < populationSize/2; i++)
        {
         //   networks[i] = networks[i + populationSize / 2].copy(new NeuralNetwork(layers));
            networks[i].Mutate((int)(1/MutationChance), MutationStrength/5);
        }
        for (int i = populationSize/2; i < populationSize; i++)
        {
         //   networks[i] = networks[i + populationSize / 2].copy(new NeuralNetwork(layers));
            networks[i].Mutate((int)(1/MutationChance), MutationStrength);
        }
        networks[populationSize - 1].Save("Assets/Save.txt");//saves networks weights and biases to file, to preserve network performance
        
    }
}
