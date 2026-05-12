using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 护具类型枚举
    /// </summary>
    public enum E_ArmorType
    {
        /// <summary>
        /// 头盔
        /// </summary>
        Helmet,
        /// <summary>
        /// 胸甲
        /// </summary>
        Breastplate,
        /// <summary>
        /// 裤子
        /// </summary>
        trouser,
        /// <summary>
        /// 靴子
        /// </summary>
        Boot,
    }
    internal class Armor:Article
    {
        

        //添加护具属性函数
        public void Helmet(Player player, MonsterObject monster)//头盔
        {
            addDef = 4;//增加4点防御力
            addHp = 30;//增加30生命值

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '¤';//设置护具的样子
            articleName = "头盔";//设置护具的名字
            CreatePos(player, monster);
            Draw(articlePos, color, cion);
            
        }

        //添加护具属性函数
        public void Breastplate(Player player, MonsterObject monster)//胸甲
        {
            addDef = 6;//增加6点防御力
            addHp = 40;//增加40生命值

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '¤';//设置护具的样子
            articleName = "胸甲";//设置护具的名字
            CreatePos(player, monster);
            Draw(articlePos, color, cion);

        }
        //添加护具属性函数
        public void trouser(Player player, MonsterObject monster)//裤子
        {
            addDef = 5;//增加5点防御力
            addHp = 30;//增加30生命值

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '¤';//设置护具的样子
            articleName = "裤子";//设置护具的名字
            CreatePos(player, monster);
            Draw(articlePos, color, cion);

        }
        //添加护具属性函数
        public void Boot(Player player, MonsterObject monster)//靴子
        {
            addDef = 4;//增加2点防御力
            

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '¤';//设置护具的样子
            articleName = "靴子";//设置护具的名字
            CreatePos(player, monster);
            Draw(articlePos, color, cion);
        }
    }
}
