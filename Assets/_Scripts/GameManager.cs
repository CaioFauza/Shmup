using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
   private static GameManager _instance;
   
   public enum GameState { MENU, START, GAME, PAUSE, END };
   public GameState gameState { get; private set; }
   public delegate void ChangeStateDelegate();
   public static ChangeStateDelegate changeStateDelegate;

   public int lifes;
   public int points;
   public int enemyCounter;

   public static GameManager GetInstance()
   {
       if(_instance == null)
       {
           _instance = new GameManager();
       }
       return _instance;
   }

   private GameManager()
   {
       lifes = 10;
       points = 0;
       enemyCounter = 0;
       gameState = GameState.MENU;
   }

   public void ChangeState(GameState nextState)
   {
       if(nextState == GameState.START) Reset();
       gameState = nextState;
       changeStateDelegate();
   }

   private void Reset()
   {
       lifes = 10;
       points = 0;
       enemyCounter = 0;
   }

}