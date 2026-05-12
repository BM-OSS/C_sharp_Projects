using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    internal class Walls:GameObject
    {
        //写一个构造函数，等到new一个wall类的对象时，
        //就自动生成这个墙的横纵坐标，并且生成的这个类对象会把墙给画出来
        public Walls(int x,int y)
        {
            pos = new Prosition(x,y);
        }

        //写一个画出墙的方法，
        //在new出walls的类对象时就会把这个墙画出来
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor=ConsoleColor.Red;
            Console.Write("■");
        }

        

    }
}
