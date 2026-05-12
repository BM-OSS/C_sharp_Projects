using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    internal class GameScene:ISceneUpdate
    {
        
        public Map map;//声明地图类
        public Player player;//玩家类
        public MonsterObject monsters;//怪物类
        private int monsterIndex=19;//用于计算生成了多少个怪物
        public int countTime = 1000;//刷新装备计数变量，每隔1000次刷新一个
        public Article article;//装备类
        public E_WeaponType weaponType;//武器枚举变量
        public E_ArmorType armorType;//护具枚举变量
        public bool isEquipPanelOpen = false;// 类开头新增成员变量
        

        //新增：可用装备候选列表（0-7代表8个装备，生成后移除）
        private List<int> availableEquip = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
        Random R = new Random();
        public GameScene() 
        {
            map = new Map();
            player = new Player(20,10);
        }



        public void Update()
        {
            //新增：如果打开了装备面板，只画面板，暂停所有游戏逻辑
            if (isEquipPanelOpen)
            {
                DrawEquipPanel();
                player.isTrueInfo = true;
                player.Move(player,monsters,article);
                return;
            }


            map.Draw();
            player.Move(player,monsters,article);
            player.Draw();
            IsTrueAtk();//判断是否造成攻击若成就打印信息
            
            if (monsterIndex < 20)//怪物小于20个就生成普通怪物
            {
                if (monsters == null)
                {
                    monsters = new MonsterObject(player);
                    
                }
                else
                {
                    
                    //给怪物移动打上帧率，没到那一帧就不移动
                    if (monsters.autoMove <= 0)
                    {
                        monsters.AutoMove(player);
                        monsters.autoMove = 80;
                    }
                    else
                    {
                        --monsters.autoMove;
                    }
                    monsters.Draw();
                    monsters.MonsterAtkPlayer(player,monsters);
                    //更新剩余怪物的数量
                    Console.SetCursorPosition(49, Game.h - 10);
                    Console.Write("                         ");
                    Console.SetCursorPosition(40, Game.h - 10);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("剩余怪物:"+(monsterIndex>=20?"最终Boss":20-monsterIndex));

                    if (monsters.IsDead())
                    {
                        monsters = null;
                        //打死小怪后再给这个计数器加1
                        ++monsterIndex;
                    }
                    
                }
            }
            else 
            {
                //这里生成20个怪物后就自动生成Boss
                if (monsters == null)
                {
                    // 生成BOSS！（多态：父类变量接收子类对象）
                    monsters = new Boss(player);
                }
                else
                {

                    //设置移动帧率，到达固定帧率才能移动
                    if (monsters.autoMove <= 0)
                    {
                        monsters.AutoMove(player);
                        monsters.autoMove = 60;
                    }
                    else
                    {
                        --monsters.autoMove;
                    }
                    // BOSS逻辑：绘制+攻击
                    monsters.Draw();
                    //更新剩余怪物的数量
                    Console.SetCursorPosition(49, Game.h - 10);
                    Console.Write("                         ");
                    Console.SetCursorPosition(40, Game.h - 10);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("剩余怪物:" + (monsterIndex >= 20 ? "最终Boss" : 20 - monsterIndex));
                    monsters.MonsterAtkPlayer(player, monsters);
                    IsTrueAtk();

                    // BOSS死亡
                    if (monsters.IsDead())
                    {
                        monsters = null;
                        // 游戏胜利
                        Game.isWin = true;
                        Game.ChangeScene(E_GameSceneType.End);
                    }
                }
            }
            // ========== 把装备逻辑从countTime里移出来，每帧执行 ==========
            // 只要场上有装备，就每帧画和检测拾取
            if (article != null)
            {
                article.Draw(article.articlePos, article.color, article.cion); // 每帧重绘，避免被盖
                article.PickUp(player, article);
                if (article.isTruePickUp)
                {
                    article = null;
                }
            }
            // 只有场上没装备、还没生成满8个，才计数生成新装备
            if (countTime <= 0 && article == null && gross < 8)
            {
                CreateArticle(player, monsters);
                countTime = 1000; // 可以把时间调长点，不然10帧刷一个太快了
            }
            else
            {
                --countTime;
            }

            IsTrueAtk(); // 原有攻击打印逻辑不变

        }
        //写一个打印怪物攻击玩家信息的函数(如果造成攻击就打印信息
        //，如果远离玩家就取消打印攻击信息)
        public void IsTrueAtk()
        {
            if (monsters == null)
            {
                return;
            }

            if (monsters.isTrueAtk)
            {
                //造成伤害时才打印出伤害信息
                Console.SetCursorPosition(40, Game.h - 9);
                Console.Write("                      ");
                Console.SetCursorPosition(40, Game.h - 9);
                Console.ForegroundColor = monsterIndex >= 20?ConsoleColor.Red: ConsoleColor.Green;
                Console.Write(monsters.isCriticle == true ? "造成暴击伤害攻击力翻倍" : "造成普通伤害");
            }
            else if(monsters.autoAtkCD<20)
            {
                Console.SetCursorPosition(40, Game.h - 9);
                Console.Write("                      ");
            }
            
        }
        public Random r = new Random();//用于生成随机数给生成物品使用
        public int gross = 0;//记录生成装备的个数
        //随机生成的物品的函数
        public void CreateArticle(Player player, MonsterObject monster)
        {
            // 最多生成8个装备 + 场上只能有1个
            if (availableEquip.Count == 0 || article != null)
            {
                return;
            }
            // 从【未生成的装备列表】里随机，绝对不重复
            int randomIndex = r.Next(0, availableEquip.Count);
            int count = availableEquip[randomIndex];
            // 生成完就从列表里移除，下次不会再随机到
            availableEquip.RemoveAt(randomIndex);

            if (count <= 3)//进入护甲类的switch
            {
                Armor armors = new Armor();
                switch (count)
                {
                    case 0:
                        
                        armors.Helmet(player, monster);
                        ++gross;
                        break;

                    case 1:
                        
                        armors.Breastplate(player, monster);
                        ++gross;
                        break;

                    case 2:
                        
                        armors.trouser(player, monster);
                        ++gross;
                        break;

                    case 3:
                        
                        armors.Boot(player, monster);
                        ++gross;
                        break;
                }
                // 核心：把生成的护具赋值给GameScene的article（捡取才能生效！）
                article = armors;
            }
            else//进入武器类的switch
            {
                Weapon weapons = new Weapon();
                switch (count)
                {
                    case 4:
                        
                        weapons.Rapier(player, monster);
                        ++gross;
                        break;

                    case 5:
                        
                        weapons.Dagger(player, monster);
                        ++gross;
                        break;

                    case 6:

                        weapons.Dart(player, monster);
                        ++gross;
                        break;

                    case 7:

                        weapons.Chain(player, monster);
                        ++gross;
                        break;
                }
                article = weapons;
            }
            
        }
        private void DrawEquipPanel()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            // 标题
            string title = " 装备信息 ";
            Console.SetCursorPosition(Game.w / 2 - title.Length / 2-20, 5);
            Console.Write("====================" + title + "====================");

            // 护具部分
            Console.SetCursorPosition(10, 8);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("【护具】");

            // 头盔
            Console.SetCursorPosition(10, 10);
            bool hasHelmet = player.ownedArmors.Contains(E_ArmorType.Helmet);
            Console.ForegroundColor = hasHelmet ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasHelmet ? "√" : " ")}] 头盔：防御+4，生命+30");

            // 胸甲
            Console.SetCursorPosition(10, 11);
            bool hasBreastplate = player.ownedArmors.Contains(E_ArmorType.Breastplate);
            Console.ForegroundColor = hasBreastplate ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasBreastplate ? "√" : " ")}] 胸甲：防御+6，生命+40");

            // 裤子
            Console.SetCursorPosition(10, 12);
            bool hasTrouser = player.ownedArmors.Contains(E_ArmorType.trouser);
            Console.ForegroundColor = hasTrouser ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasTrouser ? "√" : " ")}] 裤子：防御+5，生命+30");

            // 靴子
            Console.SetCursorPosition(10, 13);
            bool hasBoot = player.ownedArmors.Contains(E_ArmorType.Boot);
            Console.ForegroundColor = hasBoot ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasBoot ? "√" : " ")}] 靴子：防御+4");

            // 武器部分
            Console.SetCursorPosition(10, 15);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("【武器】");

            // 长剑
            Console.SetCursorPosition(10, 17);
            bool hasRapier = player.ownedWeapons.Contains(E_WeaponType.Rapier);
            Console.ForegroundColor = hasRapier ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasRapier ? "√" : " ")}] 长剑：攻击+10，暴击+10%");

            // 匕首
            Console.SetCursorPosition(10, 18);
            bool hasDagger = player.ownedWeapons.Contains(E_WeaponType.Dagger);
            Console.ForegroundColor = hasDagger ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasDagger ? "√" : " ")}] 匕首：攻击+4，暴击+10%");

            // 飞镖
            Console.SetCursorPosition(10, 19);
            bool hasDart = player.ownedWeapons.Contains(E_WeaponType.Dart);
            Console.ForegroundColor = hasDart ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasDart ? "√" : " ")}] 飞镖：攻击+6，暴击+10%");

            // 锁链
            Console.SetCursorPosition(10, 20);
            bool hasChain = player.ownedWeapons.Contains(E_WeaponType.Chain);
            Console.ForegroundColor = hasChain ? ConsoleColor.Green : ConsoleColor.Gray;
            Console.Write($"[{(hasChain ? "√" : " ")}] 锁链：攻击+2，暴击+10%，攻击距离+1");

            // 底部提示
            Console.SetCursorPosition(Game.w / 2 - 24, 25);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("================ 按i键关闭面板 ==================");

            Console.ResetColor();
        }
    }
}
