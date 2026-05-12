using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 场景类型枚举
    /// </summary>
    enum E_GameSceneType
    {
        /// <summary>
        /// 开始的游戏场景
        /// </summary>
        Begin,
        /// <summary>
        /// 游戏中的场景
        /// </summary>
        Game,
        /// <summary>
        /// 游戏结束的场景
        /// </summary>
        End,
    }


    internal class Game
    {
        //定义窗口的宽高
        public static int w = 80;
        public static int h = 45;

        //定义一个接口的变量来表示当前的窗口显示
        public static ISceneUpdate nowScene;

        public static bool isWin = false;//用于结束时的判断，到时候可以用来判断是游戏胜利还是游戏失败

        public Game() 
        {

            //影藏光标
            Console.CursorVisible = false;

            

            //设置程序窗口和缓冲区的大小
            Console.SetWindowSize(w, h);
            Console.SetWindowSize(w, h);

            ChangeScene(E_GameSceneType.Begin);
           

        }

        //设置游戏的开始函数，其实就是设计一个死循环
        public void StartGame()
        {
            while (true)
            {
                //判断当前的场景值是什么，来确定是不是要刷新场景
                if (nowScene != null)
                {
                    nowScene.Update();
                }
                
            }
        }

        //设置一个变换场景的函数来改变得出场景的变换
        //设置一个switch语句来选择说选的场景是什么
        public static void ChangeScene(E_GameSceneType type)
        {
            //切换场景前先把前面生成的内容先删除掉
            Console.Clear();
            switch (type)
            {
                case E_GameSceneType.Begin:
                    nowScene = new BeginScene();
                    break;
                case E_GameSceneType.Game:
                    nowScene = new GameScene();
                    break;
                case E_GameSceneType.End:
                    nowScene = new EndScene();
                    break;
                
            }
        }



    }
}
