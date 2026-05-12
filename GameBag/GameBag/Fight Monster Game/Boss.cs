using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace GameBag.Fight_Monster_Game
{
    //关键1：继承 MonsterObject
    internal class Boss : MonsterObject
    {
        //关键2：构造函数调用父类构造（必须写）
        public Boss(Player player) : base(player)
        {
            //关键3：强化BOSS属性（远超普通怪物）
            monsterHp = 500;          // BOSS血量500
            monsterAtk = 30;          // BOSS攻击30
            monsterDef = 30;          // BOSS防御30
            monsterCriticleChance = 35; // BOSS暴击率35%

            // 重写属性文本
            hpInfo = "BOSS血量:";
            atkInfo = "BOSS攻击力:";
            defInfo = "BOSS防御力:";
            criticleChanceInfo = "BOSS暴击率:";

            // BOSS攻击/移动更快
            autoAtkCD = 60;
            autoMove = 0;
        }
        public override void Draw()
        {
            // 强制调用 Boss 自己的 Draw(Prosition)
            Draw(monsterPos);
        }
        //重写：BOSS专属绘制（图标、颜色不一样）
        public override void Draw(Prosition monster)
        {
            // BOSS图标用 B ，颜色红色
            Console.SetCursorPosition(monster.x, monster.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("⊙");

            // BOSS属性面板
            Console.SetCursorPosition(40, Game.h - 14);
            Console.Write("                         ");
            Console.SetCursorPosition(40, Game.h - 14);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{hpInfo}{monsterHp}");

            Console.SetCursorPosition(40, Game.h - 13);
            Console.Write("                         ");
            Console.SetCursorPosition(40, Game.h - 13);
            Console.Write($"{atkInfo}{monsterAtk}");

            Console.SetCursorPosition(40, Game.h - 12);
            Console.Write("                         ");
            Console.SetCursorPosition(40, Game.h - 12);
            Console.Write($"{defInfo}{monsterDef}");

            Console.SetCursorPosition(40, Game.h - 11);
            Console.Write("                         ");
            Console.SetCursorPosition(40, Game.h - 11);
            Console.Write($"{criticleChanceInfo}{monsterCriticleChance}%");
        }

        // 重写：BOSS死亡特效
        public override bool IsDead()
        {
            if (monsterHp == 0)
            {
                // 清空BOSS图标
                Console.SetCursorPosition(monsterPos.x, monsterPos.y);
                Console.Write(" ");
                // BOSS死亡清空属性面板
                Console.SetCursorPosition(40, Game.h - 14);
                Console.Write("                         ");
                Console.SetCursorPosition(40, Game.h - 13);
                Console.Write("                         ");
                Console.SetCursorPosition(40, Game.h - 12);
                Console.Write("                         ");
                Console.SetCursorPosition(40, Game.h - 11);
                Console.Write("                         ");
                return true;
            }
            return false;
        }

        // 重写：BOSS移动更快
        Random r = new Random();
        public override void AutoMove(Player player)
        {
            // BOSS移动频率更高
            //base.AutoMove(player);
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
    }
}
