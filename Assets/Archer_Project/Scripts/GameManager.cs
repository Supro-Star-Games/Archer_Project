using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  GameManager : MonoBehaviour
{
     public static GameManager Instance;
     public static bool IsPaused;

     private Archer _archer;

     private void Awake()
     {
          if (Instance == null)
          {
               Instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else
          {
               Destroy(gameObject);
          }

          _archer = FindObjectOfType<Archer>();
     }

     private void OnEnable()
     {
          _archer.ArhcerLVLUp += PauseGame;
     }

     private void OnDisable()
     {
          _archer.ArhcerLVLUp -= PauseGame;
     }

     public static void RestartLevel()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

     public static void LoadMenu()
     {
          SceneManager.LoadScene("MainMenu");
     }
     public static void PauseGame()
     {
          if (Time.timeScale > 0)
          {
               IsPaused = true;
               Time.timeScale = 0f;

          }
          else
          {
               IsPaused = false;
               Time.timeScale = 1f;
          }
     }
}
