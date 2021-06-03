using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static int itemCount = 0;
    public static int playerLives = 3;
    public GameData gameData;
    public Image gameImage;
    void Start()
    {
        gameImage.sprite = gameData.startTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameImage.enabled = false;
        }
    }
}
