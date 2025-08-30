using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//集合玩家相关脚本
public class Player : MonoBehaviour
{
    public PlayerMovement PlayerMovement => playerMovement;



    PlayerMovement playerMovement;



    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
}
