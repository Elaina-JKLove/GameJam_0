using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//集合玩家相关脚本
public class Player : MonoBehaviour
{
    public Player_Movement Player_Movement => player_Movement;



    Player_Movement player_Movement;



    void Awake()
    {
        player_Movement = GetComponent<Player_Movement>();
    }
}
