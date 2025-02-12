using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Drawing;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject tilePrefab;
    public Transform tileContainer;

    public GameObject playerPrefab;
    public Transform playerContainer;

    public PanelVisual panel;
    public static GameController Instance { get; private set; }
    public int BOARD_WIDTH = 7;
    public int BOARD_HEIGHT = 5;
    public int PLAYER_SIZE = 4;
    public static readonly char[] playerCharacters = new char[4] { 'X', 'O', 'A', 'B'};

    private int turn = 0;
    private int step = 0;
    private TileVisual[,] tiles;
    private PlayerVisual[] players;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        tiles =new TileVisual[BOARD_HEIGHT, BOARD_WIDTH];
        players = new PlayerVisual[PLAYER_SIZE];
        visit = new bool[BOARD_HEIGHT, BOARD_WIDTH];
        directions = new int[BOARD_HEIGHT, BOARD_WIDTH];

        tileContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(BOARD_WIDTH * 100, BOARD_HEIGHT * 100);
        //Instantiate tiles
        for (int i = 0; i < BOARD_HEIGHT; i++)
            for (int j = 0; j < BOARD_WIDTH; j++)
            {
                tiles[i, j] = Instantiate(tilePrefab, tileContainer).GetComponent<TileVisual>();
                tiles[i, j].Init(i, j);
            }
        //Instantiate players;
        for (int i = 0; i < PLAYER_SIZE; i++)
        {
            players[i] = Instantiate(playerPrefab, playerContainer).GetComponent<PlayerVisual>();
            players[i].Init(i, playerCharacters[i]);
        }
    }
    public void Play(TileVisual tileVisual)
    {
        tileVisual.text.text = players[turn].character.ToString();
        tileVisual.player = turn;
        FindLargestConnectedComponent();
        
        turn++;
        step++;
        turn %= PLAYER_SIZE;

        if (step== BOARD_HEIGHT * BOARD_WIDTH)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        int maxScoreIndex = -1;
        int maxScore = 0;
        bool gameDrawFlag = true;
        for (int i = 1; i < PLAYER_SIZE; i++)
            if (players[i].score != players[i - 1].score)
            {
                gameDrawFlag = false;
                break;
            }
        if (gameDrawFlag)
        {
            panel.Show("Game Draw!");
        }
        else
        {
            for (int i = 0; i < PLAYER_SIZE; i++)
                if (maxScore < players[i].score)
                {
                    maxScore = players[i].score;
                    maxScoreIndex = i;
                }
            panel.Show(players[maxScoreIndex].character.ToString() + " Won!");
        }
    }
    private int currentNumber = 0;
    private int currentMaxNumber = 0;
    private bool[,] visit;
    private static readonly int[] dx = new int[4] { -1, 0, 1, 0 };
    private static readonly int[] dy = new int[4] { 0, -1, 0, 1 };
    private int[,] directions;

    private List<Vector2Int> maxConnectedTiles = new List<Vector2Int>();
    private List<Vector2Int> tempList = new List<Vector2Int>();

    private void DFS(int x, int y)
    {
        if (x < 0 || x >= BOARD_HEIGHT || y < 0 || y >= BOARD_WIDTH)
            return;
        if (tiles[x, y].player != turn || visit[x, y])
            return;

        visit[x, y] = true;
        currentNumber++;
        tempList.Add(new Vector2Int(x, y));

        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (newX >= 0 && newX < BOARD_HEIGHT && newY >= 0 && newY < BOARD_WIDTH &&
                tiles[newX, newY].player == turn && !visit[newX, newY])
            {
                directions[x, y] |= (1 << i);  
                directions[newX, newY] |= (1 << ((i + 2) % 4)); 
                DFS(newX, newY);
            }
        }
    }

    public void FindLargestConnectedComponent()
    {
        Array.Clear(visit, 0, visit.Length);
        Array.Clear(directions, 0, directions.Length);  
        maxConnectedTiles.Clear();
        currentMaxNumber = 0;

        for (int x = 0; x < BOARD_HEIGHT; x++)
        {
            for (int y = 0; y < BOARD_WIDTH; y++)
            {
                if (!visit[x, y] && tiles[x, y].player == turn)
                {
                    currentNumber = 0;
                    tempList.Clear();
                    DFS(x, y);

                    if (currentNumber > currentMaxNumber)
                    {
                        currentMaxNumber = currentNumber;
                        maxConnectedTiles = new List<Vector2Int>(tempList);
                    }
                }
            }
        }
        if (currentMaxNumber >= 3)
        {
            players[turn].UpdateScore(3 + (currentMaxNumber - 3) * 2);
            foreach (var p in maxConnectedTiles)
            {
                List<int> dir = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    if ((directions[p.x, p.y] & (1 << i)) != 0)
                    {
                        dir.Add(i);
                    }
                }
                tiles[p.x, p.y].SetExit(dir.ToArray());
                tiles[p.x, p.y].Use();
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
