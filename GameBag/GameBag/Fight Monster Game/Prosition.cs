using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 位置坐标结构体
    /// </summary>
    internal struct Prosition
    {
        //声明位置坐标变量，可以用于判断是否重合
        public int x;
        public int y;

        //申明构造函数
        public Prosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //写一个==运算符重载函数
        //用于结构体位置判断
        public static bool operator == (Prosition p1, Prosition p2)
        {

            if (p1.x == p2.x && p1.y == p2.y)
            {
                return true;
            }
            return false;

        }

        public static bool operator != (Prosition p1, Prosition p2)
        {

            if (p1.x != p2.x || p1.y != p2.y)
            {
                return true;
            }
            return false;
        }

    }
}
