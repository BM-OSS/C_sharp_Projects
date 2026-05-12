using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    internal class Map : GameObject
    {
        //画地图的话就需要生成一个walls的数组
        public Walls[] walls;

        //写一个构造函数，在生成地图的时候就会自动把地图打印出来
        public Map()
        {

            //计算画出墙的个数
            walls = new Walls[Game.w+(Game.h-3)*2 + (Game.w ) / 2];
            //写一个index变量，用来访问数组序列
            int index = 0;
            //画出横着的墙
            for (int i = 0; i < Game.w; i+=2)
            {
                walls[index] = new Walls(i,0);
                walls[index+1] = new Walls(i, Game.h - 2);
                walls[index + 2] = new Walls(i, Game.h - 15);
                index += 3;
            }
            //画出竖着的墙
            for (int i = 1; i < Game.h-2; i++)
            {
                walls[index] = new Walls(0,i);
                walls[index+1] = new Walls(Game.w-2,i);
                index += 2;
            }

        }
            

        public override void Draw()
        {
            //用for循环调用Walls里面的Draw函数画出墙
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].Draw();
            }
        }
    }
}
