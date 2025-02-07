/*
    File: PlayerStats.cs
    Developer: Emanuel Juracic
    First Version: February 06, 2025
    Description:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Health { get; set; }
    public int Shield { get; set; }
    public float Speed { get; set; }
    public float MaxSpeed { get; set; }
    public int Lives { get; set; }
    public int Score { get; set; }

}
