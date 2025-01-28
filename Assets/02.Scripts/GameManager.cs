using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject FuelPrefab;
    [SerializeField] private Transform[] spawnPoints;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 2f;
    [SerializeField] private TMP_Text gasText;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject EndPanel;
    
    public float gas = 101f;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_instance == null)
                {
                    Debug.Log("no instance found");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        Time.timeScale = 0f;
        StartPanel.SetActive(true);
        EndPanel.SetActive(false);
        StartCoroutine(SpawnFuelCoroutine());
    }
    
    private void Update()
    {
        gas -= Time.deltaTime * 10f;
        gasText.text = "Gas: " + Mathf.Max(0, Mathf.FloorToInt(gas)).ToString();
        if (gas <= 0f)
        {
            EndGame();
        }
    }

    IEnumerator SpawnFuelCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(FuelPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        EndPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
