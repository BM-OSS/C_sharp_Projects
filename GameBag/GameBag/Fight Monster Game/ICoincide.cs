using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 碰撞与边界检测
    /// </summary>
    internal interface ICoincide
    {
        // 1. 判断是否触碰边界
        abstract bool CheckBorder();
        // 2. 判断是否与 另一个坐标 碰撞
        abstract bool CheckCollision(Prosition prosition);
    }
}
