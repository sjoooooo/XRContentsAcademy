using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Text itemCountText;
    public Text playerLivesText;   

    // Update is called once per frame
    void Update()
    {
        itemCountText.text = "ITEM COUNT: " + GameManager.itemCount.ToString();
        playerLivesText.text = "PLAYER LIVES : " + GameManager.playerLives.ToString();
    }
}
