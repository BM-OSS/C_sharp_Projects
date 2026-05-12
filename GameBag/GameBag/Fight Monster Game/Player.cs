using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 玩家属性枚举
    /// </summary>
    public enum E_PalyerPorpertyType
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




    /// <summary>
    /// 玩家类
    /// </summary>
    internal class Player : IDraw, ICoincide
    {
        
        //定义玩家的相关属性，这样好传入数据
        public int playerHp=1000;
        public int playerAtk=50;
        public int playerDef=5;
        //这里的暴击率用一个整数存储，后续写一个计算函数来计算
        public int playerCriticleChance=10;
        
        //声明一个用于计算暴击概率的变量
        private int criticleChancecount;
        //用于标志是否产生暴击
        private bool isCriticle = false;
        //申明一个玩家的位置结构体
        public Prosition playerPos;
        //定义一个结构体变量记录移动前的位置
        private Prosition oldPlayerPos = new Prosition();
        public bool isTrueInfo = false;//记录是否打开了装备栏
        
        //定义玩家属性相关信息
        private string hpInfo = "玩家血量:";
        private string atkInfo = "玩家攻击力:";
        private string defInfo = "玩家防御力:";
        private string criticleChanceInfo = "玩家暴击率:";

        // 玩家类新增这两个属性
        public List<E_ArmorType> ownedArmors = new List<E_ArmorType>();
        public List<E_WeaponType> ownedWeapons = new List<E_WeaponType>();

        public int newPlayerHp ;
        public int newPlayerAtk ;
        public int newPlayerDef ;
        public int newPlayerCriticleChance ;


        

        public Player(int x,int y)
        {
            playerPos.x = x;
            playerPos.y = y;
            oldPlayerPos = playerPos;
            newPlayerHp = 0; 
            newPlayerAtk = 0;
            newPlayerCriticleChance = 0;
            newPlayerDef = 0;
        }

        


        public void Draw()
        {
            //玩家图标
            Console.SetCursorPosition(playerPos.x, playerPos.y);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("●");


            //玩家属性基础信息打印
            Console.SetCursorPosition(2, Game.h -14);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{hpInfo}{playerHp}");
            

            Console.SetCursorPosition(2, Game.h - 13);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{atkInfo}{playerAtk}");
            

            Console.SetCursorPosition(2, Game.h - 12);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{defInfo}{playerDef}");

            Console.SetCursorPosition(2, Game.h - 11);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{criticleChanceInfo}{playerCriticleChance}%");

            Console.SetCursorPosition(2, Game.h - 10);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("按j或J键攻击怪物");

            Console.SetCursorPosition(2, Game.h - 9);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("按I或i键查看装备信息");

        }

        
        
        public void PlayerPropertyChange(E_PalyerPorpertyType type)
        {
            //定义一个字符串变量用于获取变化位置变量的字符串长度
            string str;
            //在定义一个int变量来表示原来的长度
            int lastLength;
            //定义一个字体颜色改变的变量
            ConsoleColor color;
            switch (type)
            {
                case E_PalyerPorpertyType.hpInfo:
                    str = playerHp.ToString();
                    lastLength= str.Length;
                    color = ConsoleColor.DarkCyan;
                    playerHp += newPlayerHp;
                    if (playerHp <= 0)
                    {
                        playerHp = 0;
                        // 新增：玩家死亡跳结束场景
                        Game.isWin = false;
                        Game.ChangeScene(E_GameSceneType.End);
                    }
                    UpdateAndCover(11, Game.h - 14, lastLength, playerHp, color);
                    newPlayerHp = 0;//重置
                    break;
                case E_PalyerPorpertyType.atkInfo:
                    str = playerAtk.ToString();
                    lastLength = str.Length;
                    color = ConsoleColor.DarkCyan;
                    playerAtk += newPlayerAtk;
                    UpdateAndCover(13, Game.h - 13, lastLength, playerAtk, color);
                    newPlayerAtk = 0;//重置
                    break;
                case E_PalyerPorpertyType.defInfo:
                    str = playerDef.ToString();
                    lastLength = str.Length;
                    color = ConsoleColor.DarkCyan;
                    playerDef += newPlayerDef;
                    UpdateAndCover(13, Game.h - 12, lastLength, playerDef, color);
                    newPlayerDef = 0;//重置
                    break;
                case E_PalyerPorpertyType.critileChanceInfo:
                    str = playerCriticleChance.ToString();
                    lastLength = str.Length;
                    color = ConsoleColor.DarkCyan;
                    playerCriticleChance += newPlayerCriticleChance;
                    UpdateAndCover(13, Game.h - 11, lastLength, playerCriticleChance, color);
                    newPlayerCriticleChance = 0;//重置
                    break;
            }
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


        //写一个和怪物是否靠近怪物的函数若x轴差值后y轴差值之和等于1，就判断为可以攻击
        public bool CheckDistance(Player player,MonsterObject monster)
        {
            if (monster == null)
            {
                return false;
            }
            if (Math.Abs(playerPos.x - monster.monsterPos.x) == 2 &&
                Math.Abs(playerPos.y - monster.monsterPos.y) == 0 ||
                Math.Abs(playerPos.x - monster.monsterPos.x) == 0 &&
                Math.Abs(playerPos.y - monster.monsterPos.y) == 1)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 玩家移动的函数
        /// </summary>
        public void Move(Player player, MonsterObject monster, Article article)
        {
            

            //执行按键判断逻辑
            if (Console.KeyAvailable)
            {
                //先保存移动前的位置
                oldPlayerPos = playerPos;


                //移动前先擦除原有的角色图标
                Console.SetCursorPosition(playerPos.x, playerPos.y);
                Console.Write(" ");
                switch (Console.ReadKey(true).Key)
                {
                    //既然我们每移动一次都会判断是否撞墙或者撞怪
                    //那不如直接把wasd的四次检测直接统一在switch的外部检测
                  
                    case ConsoleKey.W:
                        if (isTrueInfo)
                        {
                            break;
                        }
                        --playerPos.y;
                        
                        break;
                    case ConsoleKey.S:
                        if (isTrueInfo)
                        {
                            break;
                        }
                        ++playerPos.y;
                        
                        break;
                    case ConsoleKey.A:
                        if (isTrueInfo)
                        {
                            break;
                        }
                        playerPos.x -= 2;
                        
                        break;
                    case ConsoleKey.D:
                        if (isTrueInfo)
                        {
                            break;
                        }
                        playerPos.x += 2; 


                        break;
                    case ConsoleKey.J:
                        if (CheckDistance(player, monster))
                        {
                            PlayerActionAtk(monster);
                            //造成伤害时才打印出伤害信息
                            Console.SetCursorPosition(2, Game.h - 8);
                            Console.Write("                      ");
                            Console.SetCursorPosition(2, Game.h - 8);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(isCriticle == true ? "造成暴击伤害攻击力翻倍" : "造成普通伤害");
                        }
                        //当远离怪物时就消除攻击信息
                        else 
                        {
                            Console.SetCursorPosition(2, Game.h - 9);
                            Console.Write("                      ");
                        }
                        break;
                    // 原有WASD、J的逻辑不变...
                    case ConsoleKey.I:
                        // 切换装备面板状态
                        if (Game.nowScene is GameScene gameScene)
                        {
                            gameScene.isEquipPanelOpen = !gameScene.isEquipPanelOpen;
                            if (gameScene.isEquipPanelOpen)
                            {
                                Console.Clear(); // 打开面板清屏
                            }
                            else
                            {
                                isTrueInfo = false;
                                Console.Clear(); // 关闭面板清屏重绘游戏
                                gameScene.map.Draw();
                                gameScene.player.Draw();
                                gameScene.monsters?.Draw();
                                gameScene.article?.Draw();
                            }
                        }
                        break;

                }
                bool hitBorder = CheckBorder();
                bool hitMonster = false;
                if (monster != null)
                {
                    hitMonster=CheckCollision(monster.monsterPos);
                }
                //检测bool值若为真就重新会重新会退回去
                if (hitBorder || hitMonster)
                {
                    playerPos = oldPlayerPos;
                }
            }

        }

        //继承一个接口来写一个判断玩家移动位置后是否会和怪物和墙壁相重合
        public bool CheckBorder()
        {
            //如果是与边界相重合时返回true
            return playerPos.x<=1||playerPos.x>=Game.w-2||
                   playerPos.y<=0||playerPos.y>=Game.h-15; 
        }
        public bool CheckCollision(Prosition otherPos)
        {

            return otherPos.x==playerPos.x&&otherPos.y==playerPos.y;
        }

        //写一个对怪物造成攻击的函数
        public void PlayerActionAtk(MonsterObject monster)
        {
            if (monster == null)
            {
                return;
            }
            int nowplayerAtk = ChageCriticleChance();
            //先判断是攻击力是否大于防御力
            //如果大于防御力则攻击无效做一个防止反向溢出的问题
            if ((nowplayerAtk - monster.monsterDef) < 0)
            {
                monster.newMonsterHp = monster.monsterHp;
            }
            else
            {
                monster.newMonsterHp = monster.monsterHp - (nowplayerAtk - monster.monsterDef);
            }
            if (monster.newMonsterHp <= 0)
            {
                monster.newMonsterHp = 0;
            }
            monster.MonsterPropertyChange(E_MonsterPorpertyType.hpInfo);
        }
        //再写一个计算暴击后的伤害的函数
        private Random R = new Random();
        public int ChageCriticleChance() 
        {
            int nowPlayerAtk = playerAtk;
            criticleChancecount = R.Next(1,101);
            if (criticleChancecount < playerCriticleChance)
            {
                isCriticle = true;
                nowPlayerAtk = playerAtk * 2;
            }
            else 
            {
                isCriticle = false;
            }
            return nowPlayerAtk;
        } 
    }
}
