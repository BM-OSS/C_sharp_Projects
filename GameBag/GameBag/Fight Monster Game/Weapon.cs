using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 武器类型枚举
    /// </summary>
    public enum E_WeaponType
    {
        /// <summary>
        /// 长剑
        /// </summary>
        Rapier,
        /// <summary>
        /// 匕首
        /// </summary>
        Dagger,
        /// <summary>
        /// 飞镖
        /// </summary>
        Dart,
        /// <summary>
        /// 锁链
        /// </summary>
        Chain,
    }
    /// <summary>
    /// 武器子类
    /// </summary>
    internal class Weapon : Article
    {



        
        //添加武器属性函数
        public void Rapier(Player player,MonsterObject monster)//长剑
        {
            addAtk = 10;//增加10点攻击力
            addCriticleChance = 10;//增加10点暴击

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '◆';//设置武器的样子
            articleName = "长剑";//设置武器的名字
            CreatePos(player, monster);
            Draw(articlePos,color,cion);
            
        }

        //添加武器属性函数
        public void Dagger(Player player, MonsterObject monster)//匕首
        {
            addAtk = 4;//增加4点攻击力
            addCriticleChance = 10;//增加10点暴击

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '◆';//设置武器的样子
            articleName = "匕首";//设置武器的名字
            CreatePos(player, monster);
            Draw(articlePos, color, cion);

        }

        //添加武器属性函数
        public void Dart(Player player, MonsterObject monster)//飞镖
        {
            addAtk = 6;//增加6点攻击力
            addCriticleChance = 10;//增加10点暴击

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '◆';//设置武器的样子
            articleName = "飞镖";//设置武器的名字
            CreatePos(player, monster);
            Draw(articlePos, color, cion);

        }

        //添加武器属性函数
        //还没写完，现在还没完成攻击距离的增加
        public void Chain(Player player, MonsterObject monster)//锁链
        {
            addAtk = 2;//增加2点攻击力
            addCriticleChance = 10;//增加10点暴击

            color = ConsoleColor.DarkCyan;//设置颜色
            cion = '◆';//设置武器的样子
            articleName = "锁链";//设置武器的名字
            CreatePos(player, monster);
            Draw(articlePos, color, cion);


        }
    }
}
