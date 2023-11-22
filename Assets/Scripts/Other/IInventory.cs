using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    int Money { get; set; }
    int SnowBallNum { get; set; }
    int BombNum { get; set; }
}
