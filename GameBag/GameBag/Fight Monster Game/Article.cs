using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 定义一个物品基类，用于具体物品的继承
    /// </summary>
    internal class Article : ICoincide,IDraw
    {

        public Prosition articlePos;//表示生成物品的坐标
        public ConsoleColor color;//表示生成物品的颜色
        public char cion;//表示生成物品的图标，用一个cion来等于生成图标的char值
        public string articleName;//表示生成物品的名字

        public int addDef = 0;//新增的防御力，由装备提供
        public int addHp = 0;//新增血量的，由装备提供
        public int addAtk = 0;//新增的攻击力，由装备提供
        public int addCriticleChance = 0;//新增的暴击率，由装备提供
        public bool isTruePickUp = false;//用于判断是否已经拾取过这个装备

        //借用怪物类的构造函数写一个生成随机坐标的函数
        public Random r = new Random();//用于生成随机数给生成物品使用
        public void CreatePos(Player player,MonsterObject monster)
        {
            //用一个do——while循环来不断地判断是否重叠，是的话就重新生成
            do
            {
                //这里有个问题就是怪物是占据两格，所以我们只能生成偶数，不能生成奇数
                articlePos.x = r.Next(2, (Game.w - 2) / 2) * 2;
                articlePos.y = r.Next(1, Game.h - 15);
            } while (CheckPos(player, monster, articlePos));
        }


        //判断生成的装备和怪物和玩家的坐标是否重叠(相同返回真)
        public bool CheckPos(Player player, MonsterObject monster,Prosition articlePos)
        {
            if (player.playerPos==articlePos||monster.monsterPos== articlePos)
            {
                //判断坐标是重叠的话就调用一个新的重生成函数
                return true;
            }
            return false;
        }


        //生成的物品都需要和墙体和在场的角色相检测所以继承了碰撞接口
        //与怪物类类似，直接复用修改即可
        public bool CheckBorder()
        {
            //如果是与边界相重合时返回true
            return articlePos.x <= 1 || articlePos.x >= Game.w - 2 ||
                   articlePos.y <= 0 || articlePos.y >= Game.h - 15;
        }
        //生成物品时已经生成了怪物，还有玩家在移动，
        //所以在传入的参数里面还要在加上怪物的坐标
        public bool CheckCollision(Player player,MonsterObject monster)
        {

            if (articlePos == player.playerPos || articlePos == monster.monsterPos)
            {
                return true;
            }
            return false;
        }

        public bool CheckCollision(Prosition prosition)
        {
            return false;
        }



        //生成装备的逻辑和生成怪物的逻辑类似，直接借用后修改就行
        public void Draw(Prosition prosition,ConsoleColor color,char cion)
        {
            
            Console.SetCursorPosition(articlePos.x, articlePos.y);
            Console.ForegroundColor = color;
            Console.Write(cion);
        }

        
        //检测是否是真的拾取到装备了
        public bool IsPickUp(Player player, Article article)
        {
            if (player.playerPos == article.articlePos)
            {
                return true;
            }
            return false;
        }

        //拾取装备的函数
        public void PickUp(Player player, Article article)
        {
            if (isTruePickUp)
            {
                return;
            }
            if (IsPickUp(player, article))
            {
                isTruePickUp = true;
                switch (articleName)
                {
                    case "头盔":
                        player.newPlayerDef += addDef;
                        player.newPlayerHp += addHp;
                        player.ownedArmors.Add(E_ArmorType.Helmet); // 新增装备系统
                        break;
                    case "胸甲":
                        player.newPlayerDef += addDef;
                        player.newPlayerHp += addHp;
                        player.ownedArmors.Add(E_ArmorType.Breastplate); // 新增装备系统
                        break;
                    case "裤子":
                        player.newPlayerDef += addDef;
                        player.newPlayerHp += addHp;
                        player.ownedArmors.Add(E_ArmorType.trouser); // 新增装备系统
                        break;
                    case "靴子":
                        player.newPlayerDef += addDef;
                        player.ownedArmors.Add(E_ArmorType.Boot); // 新增装备系统
                        break;
                    case "长剑":
                        player.newPlayerAtk += addAtk;
                        player.newPlayerCriticleChance += addCriticleChance;
                        player.ownedWeapons.Add(E_WeaponType.Rapier); // 新增装备系统
                        break;
                    case "匕首":
                        player.newPlayerAtk += addAtk;
                        player.newPlayerCriticleChance += addCriticleChance;
                        player.ownedWeapons.Add(E_WeaponType.Dagger); // 新增装备系统
                        break;
                    case "飞镖":
                        player.newPlayerAtk += addAtk;
                        player.newPlayerCriticleChance += addCriticleChance;
                        player.ownedWeapons.Add(E_WeaponType.Dart); // 新增装备系统
                        break;
                    case "锁链":
                        player.newPlayerAtk += addAtk;
                        player.newPlayerCriticleChance += addCriticleChance;
                        player.ownedWeapons.Add(E_WeaponType.Chain); // 新增装备系统
                        break;
                }
                // 2.刷新所有玩家属性面板
                player.PlayerPropertyChange(E_PalyerPorpertyType.hpInfo);
                player.PlayerPropertyChange(E_PalyerPorpertyType.atkInfo);
                player.PlayerPropertyChange(E_PalyerPorpertyType.defInfo);
                player.PlayerPropertyChange(E_PalyerPorpertyType.critileChanceInfo);

                // 3.清除屏幕上的装备图标
                Console.SetCursorPosition(articlePos.x, articlePos.y);
                Console.Write(" ");
            }
        }
        
        public void Draw()
        {
            
        }
    }
}
