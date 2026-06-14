using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    internal abstract class GameObject : IDraw
    {

        public Prosition pos;

        //写一个抽象类函数，让其子类去实现具体的内容
        public abstract void Draw();
       
    }
}
