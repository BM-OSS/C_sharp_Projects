using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 怪物类型枚举
    /// </summary>
    enum E_PlayerType
    {
        /// <summary>
        /// 普通小怪
        /// </summary>
        CommonMonster,
        /// <summary>
        /// 最终怪物
        /// </summary>
        Boss,
    }
    /// <summary>
    /// 怪物属性枚举
    /// </summary>
    public enum E_MonsterPorpertyType
    {
        /// <summary>
        /// 血量
        /// </summary>
        hpInfo,
        /// <summary>
        /// 攻击力
        /// </summary>
        atkInfo,
        /// <summary>
        /// 防御力
        /// </summary>
        defInfo,
        /// <summary>
        /// 暴击率
        /// </summary>
        critileChanceInfo,
    }
    internal class MonsterObject : IDraw, ICoincide
    {
        //定义怪物的相关属性，这样好传入数据
        public int monsterHp = 100;
        public int monsterAtk = 12;
        public int monsterDef = 10;
        //这里的暴击率用一个整数存储，后续写一个计算函数来计算
        public int monsterCriticleChance = 0;
        //声明一个用于计算暴击概率的变量
        private int criticleChancecount;
        //用于标志是否产生暴击
        public bool isCriticle = false;
        //写一个是否产生攻击的标志变量
        public bool isTrueAtk = false;

        //定义怪物基类属性相关信息
        protected string hpInfo = "普通怪物血量:";
        protected string atkInfo = "普通怪物攻击力:";
        protected string defInfo = "普通怪物防御力:";
        protected string criticleChanceInfo = "普通怪物暴击率:";

        //写一个变量用于存储坐标信息
        public Prosition monsterPos = new Prosition();
        //信息更改的方法后面在写现在先放在这里
        public int newMonsterHp = 100;
        public int newMonsterAtk = 3;
        public int newMonsterDef = 1;
        public int newMonsterCriticleChance = 0;
        //写一个用于设计怪物攻击的频率的变量
        public int autoAtkCD = 0;
        //写一个用于设计怪物移动的频率变量
        public int autoMove = 0;
        //构造一个随机值使得生成的怪物位置随机
        Random r = new Random();

        //构造一个怪物基类的构造函数
        public MonsterObject(Player player)
        {
            //用一个do——while循环来不断地判断是否重叠，是的话就重新生成
            do
            {
                //这里有个问题就是怪物是占据两格，所以我们只能生成偶数，不能生成奇数
                monsterPos.x = r.Next(2, (Game.w - 2) / 2) * 2;
                monsterPos.y = r.Next(1, Game.h - 15);
            } while (CheckPos(player, monsterPos));


        }






        //写一个判断生成的怪物和玩家的坐标是否重叠(相同返回真)
        public bool CheckPos(Player player, Prosition monster)
        {
            if (player.playerPos.x == monster.x && player.playerPos.y == monster.y)
            {
                //判断坐标是重叠的话就调用一个新的重生成函数
                return true;
            }
            return false;
        }


        //写一个判断怪物死亡的方法,死亡后应该把原来的怪物擦掉
        public virtual bool IsDead()
        {

            if (monsterHp == 0)
            {
                Console.SetCursorPosition(monsterPos.x, monsterPos.y);
                Console.Write(" ");
                return true;
            }

            return false;
        }

        public virtual void Draw()
        {
            Draw(monsterPos);
        }

        //画出怪物
        public virtual void Draw(Prosition monster)
        {
            //怪物图标
            Console.SetCursorPosition(monster.x, monster.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("●");


            //玩家属性基础信息打印
            Console.SetCursorPosition(40, Game.h - 14);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{hpInfo}{monsterHp}");


            Console.SetCursorPosition(40, Game.h - 13);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{atkInfo}{monsterAtk}");


            Console.SetCursorPosition(40, Game.h - 12);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{defInfo}{monsterDef}");

            Console.SetCursorPosition(40, Game.h - 11);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{criticleChanceInfo}{monsterCriticleChance}%");



        }
        //定义一个字符串变量用于获取变化位置变量的字符串长度
        private string str;
        //在定义一个int变量来表示原来的长度
        private int lastLength;
        //定义一个字体颜色改变的变量
        private ConsoleColor color;
        public void MonsterPropertyChange(E_MonsterPorpertyType type)
        {
            switch (type)
            {

                case E_MonsterPorpertyType.hpInfo:
                    str = monsterHp.ToString();
                    lastLength = str.Length;
                    color = ConsoleColor.Green;
                    UpdateAndCover(53, Game.h - 14, lastLength, newMonsterHp, color);
                    monsterHp = newMonsterHp;

                    break;
                case E_MonsterPorpertyType.atkInfo:
                    str = monsterAtk.ToString();
                    lastLength = str.Length;
                    color = ConsoleColor.Green;
                    UpdateAndCover(55, Game.h - 13, lastLength, newMonsterAtk, color);
                    monsterAtk = newMonsterAtk;
                    break;
                case E_MonsterPorpertyType.defInfo:
                    str = monsterDef.ToString();
                    lastLength = str.Length;
                    color = ConsoleColor.Green;
                    UpdateAndCover(55, Game.h - 12, lastLength, newMonsterDef, color);
                    monsterDef = newMonsterDef;
                    break;
                case E_MonsterPorpertyType.critileChanceInfo:
                    str = monsterCriticleChance.ToString();
                    lastLength = str.Length;
                    color = ConsoleColor.Green;
                    UpdateAndCover(55, Game.h - 11, lastLength, newMonsterCriticleChance, color);
                    monsterCriticleChance = newMonsterCriticleChance;
                    break;


            }
        }
        //怪物自动移动的逻辑函数(如果撞墙或则和玩家重合就保留原位置)
        public virtual void AutoMove(Player player)
        {
            Prosition oldpos = monsterPos;
            int dir = r.Next(0, 4);
            switch (dir)
            {
                case 0:
                    --monsterPos.y;
                    break;
                case 1:
                    ++monsterPos.y;
                    break;
                case 2:
                    monsterPos.x += 2;
                    break;
                case 3:
                    monsterPos.x -= 2;
                    break;

            }


            if (CheckBorder() || CheckCollision(player.playerPos))
            {
                monsterPos = oldpos;
            }
            else
            {
                Console.SetCursorPosition(oldpos.x, oldpos.y);
                Console.Write(" ");
            }

        }

        
        
        //写一个怪物攻击的方法，如果位置靠近就可以直接自动攻击了
        public void MonsterAtkPlayer(Player player,MonsterObject monster)
        {
            isTrueAtk = false;
            //先判断是否冷却攻击再判段是否可以达到攻击距离
            
            if (autoAtkCD > 0)
            {
                --autoAtkCD;
                return;
            }

            if (player.CheckDistance(player, monster))
            {
                
                int nowMonsterAtk = ChageCriticleChance();
                int damage = nowMonsterAtk - player.playerDef;
                //先判断是攻击力是否大于防御力
                //如果大于防御力则攻击无效做一个防止反向溢出的问题
                if (damage > 0)
                {
                    // 扣血要传负值，因为玩家属性更新是 += 逻辑
                    player.newPlayerHp = -damage;
                }
                else
                {
                    // 伤害<=0不扣血
                    player.newPlayerHp = 0;
                }
                player.PlayerPropertyChange(E_PalyerPorpertyType.hpInfo);
                isTrueAtk = true;
                autoAtkCD = 120;
                
                
            }
        }
        //沿用玩家攻击怪物的暴击方式让怪物也能对玩家产生暴击攻击
        public int ChageCriticleChance()
        {
            int nowMonsterAtk = monsterAtk;
            criticleChancecount = r.Next(1, 101);
            if (criticleChancecount < monsterCriticleChance)
            {
                isCriticle = true;
                nowMonsterAtk = monsterAtk * 2;
            }
            else
            {
                isCriticle = false;
            }
            return nowMonsterAtk;
        }
        // 通用方法：覆盖旧内容并打印新内容
        private void UpdateAndCover(int x, int y, int lastLength, int newContent, ConsoleColor color)
        {
            // 覆盖旧内容
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', Math.Max(lastLength, (newContent.ToString().Length))));

            // 打印新内容
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(newContent);


        }

        //继承一个接口来写一个判断怪物移动位置后是否会和怪物和墙壁相重合
        public bool CheckBorder()
        {
            //如果是与边界相重合时返回true
            return monsterPos.x <= 1 || monsterPos.x >= Game.w - 2 ||
                   monsterPos.y <= 0 || monsterPos.y >= Game.h - 15;
        }
        public bool CheckCollision(Prosition otherPos)
        {

            return otherPos.x == monsterPos.x && otherPos.y == monsterPos.y;
        }
    }
}