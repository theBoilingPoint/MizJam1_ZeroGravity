using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject borderTopLeft, borderTop, borderTopRight, borderLeft, borderRight, borderBottomLeft, borderBottom, borderBottomRight, deathWall,
        obstacle, healthIncrement, ball;
    [SerializeField] private int ballNum;// the maximum number of balls that can be generated
    [SerializeField] private int heartNum; // the maximum number of hearts that can be generated, adjusted by the player's health
    [Range(0f, .3f)]
    [SerializeField] private float obstacleChance; // the probability of generating an obstacle
    [SerializeField] private float maxObstacleChance; // the maximum probability of generating an obstacle
    [SerializeField]  private float mapRegenerationStartTime;
    private float mapRegenerationTime;
    [SerializeField] private float obstacleChanceIncrement;
    [SerializeField] private float obstacleChanceIncreaseStartTime;
    private float obstacleChanceIncreaseTime;
    private HealthBar healthBar;
    private GameObject player;

    private void Awake()
    {
        generateDeathWalls();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        player = GameObject.Find("Player");
    }

    void Start()
    {

        mapRegenerationTime = mapRegenerationStartTime;
        obstacleChanceIncreaseTime = obstacleChanceIncreaseStartTime;
        Instantiate(ball, new Vector2(0, 0), Quaternion.identity);
        generatePlatforms();

    }

    void Update()
    {
        increaseObstacleChance(obstacleChanceIncrement);
        if(mapRegenerationTime <= 1.01)
        {
            displayDestroyEffectOfObstacles();
        }

        if(mapRegenerationTime <= 0) {
            clearGameObjectsWithTag(obstacle.tag);
            generatePlatforms();
            mapRegenerationTime = mapRegenerationStartTime;
        }
        else
        {
            mapRegenerationTime -= Time.deltaTime;
        }

        checkHeartNum();
        generateHeart();
        generateBall();
    }

    private void generateDeathWalls()
    {
        for (int i = -9; i < 10; i++)
        {
            Instantiate(deathWall, new Vector2(i, 6), Quaternion.identity);
            Instantiate(deathWall, new Vector2(i, -4), Quaternion.identity);
        }
        for (int j = -4; j < 7; j++)
        {
            Instantiate(deathWall, new Vector2(-10, j), Quaternion.identity);
            Instantiate(deathWall, new Vector2(10, j), Quaternion.identity);
        }
    }

    private void generatePlatforms()
    {
        for (int i = -9; i < 10; i++)
        {
            for(int j = -3; j < 6; j++)
            {
                if(checkOverlap(healthIncrement.tag, i, j))
                {
                    continue;
                }
                else if(UnityEngine.Random.value < obstacleChance)
                {
                    Instantiate(obstacle, new Vector2(i, j), Quaternion.identity);
                }
            }
        }
    }

    private void generateHeart()
    {
        float randX = UnityEngine.Random.Range(-9, 10);
        float randY = UnityEngine.Random.Range(-3, 6);

        if (checkOverlap(obstacle.tag, randX, randY))
        {
            generateHeart();
        }
        else
        {
            if (!hasGotEnoughHeart())
            {
                Instantiate(healthIncrement, new Vector2(randX, randY), Quaternion.identity);
            }
        }
    }

    public void generateBall()
    {
        if (!hasGotEnoughBalls())
        {
            float randX = UnityEngine.Random.Range(-1, 1);
            float randY = UnityEngine.Random.Range(-1, 1);
            Vector2 playerPos = player.transform.position;
            float newX = playerPos.x + randX;
            float newY = playerPos.y + randY;
            if (checkOverlap(obstacle.tag, newX, newY ) || checkOverlap(healthIncrement.tag, newX, newY) || checkOverlap(deathWall.tag, newX, newY))
            {
                generateBall();
            }
            else
            {
                Instantiate(ball, new Vector2(newX, newY), Quaternion.identity);
            }
        }
    }

    private void increaseObstacleChance(float increment)
    {
        if(obstacleChance >= maxObstacleChance)
        {
            obstacleChance = maxObstacleChance;
        }
        else
        {
            if(obstacleChanceIncreaseTime <= 0)
            {
                obstacleChance += increment;
                obstacleChanceIncreaseTime = obstacleChanceIncreaseStartTime;
            }
            else
            {
                obstacleChanceIncreaseTime -= Time.deltaTime;
            }
        }
    }

    private void displayDestroyEffectOfObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(obstacle.tag);
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<DissolveMaterialController>().isDissolving = true;
        }
    }

    private void clearGameObjectsWithTag(string name)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(name);
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<DissolveMaterialController>().isDissolving = true;
            Destroy(obstacle);
        }
    }

    /**
        * Adjust the maximum number of hearts according to the current health of the player.
        * The more health the player has, the less hearts will be generated. The minimum number of hearts is 1.
    */
    private void checkHeartNum()
    {
        heartNum = (int)((1 - Math.Round(healthBar.getCurrentMaxHealthRatio(), 1)) * 10 + 1);
    }

    /**
        * Check if there are enough hearts in the game
        * @return true if there are enough hearts in the game, false otherwise
    */
    private bool hasGotEnoughHeart()
    {
        return GameObject.FindGameObjectsWithTag(healthIncrement.tag).Length >= heartNum;
    }

    /**
        * Check if there are enough balls in the game
        * @return true if there are enough balls in the game, false otherwise
    */
    private bool hasGotEnoughBalls()
    {
        return GameObject.FindGameObjectsWithTag(ball.tag).Length >= ballNum;
    }

    /**
        * Check if there is an object with the given tag at the given position (x,y)
        * @param objectTag the tag of the object
        * @param x the x coordinate to check
        * @param y the y coordinate to check
        * @return true if there is an object with the given tag at the given position (x,y), false otherwise
    */
    private bool checkOverlap(string objectTag, float x, float y)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(objectTag);
        bool isOverlapped = false;
        foreach (GameObject oneObject in objects)
        {
            Vector2 objectPos = oneObject.transform.position;
            if (objectPos.x == x && objectPos.y == y)
            {
                isOverlapped = true;
            }
        }
        return isOverlapped;
    }
}
