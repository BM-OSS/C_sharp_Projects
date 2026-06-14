using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    /// <summary>
    /// 创建一个开始结束场景基类
    /// </summary>
    abstract class BeginOrEndScene:ISceneUpdate
    {
        //创建的基类，把通用的信息放在这个基类，
        //初始界面和结束界面都继承这个基类
        
        //定义变量表示界面信息
        protected string strTitle;
        protected string strOneLevel;
        protected string strPrompt;
        protected string strTwoLevel;

        //定义一个预选中项变量，来确定当前所处的场景
        public int nowIndexScene=0;

        abstract public void EnterJDoSomting();
        

        //进入程序后就不断地开始调用Update()函数
        public void Update()
        {
            //打印出对应的页面信息
            //调整输入字体的背景颜色
            Console.ForegroundColor = ConsoleColor.White;

            //打印标题（提示词）
            Console.SetCursorPosition(Game.w / 2 - strTitle.Length, 12);
            Console.Write(strTitle);
            Console.SetCursorPosition(Game.w / 2 - 7, 13);
            Console.Write(strPrompt);


            //选着打印的字体背景颜色（三目运算符）选择颜色
            Console.ForegroundColor = nowIndexScene==0? ConsoleColor.Red: ConsoleColor.White;
            //打印第一个字符串
            Console.SetCursorPosition(Game.w / 2 - strOneLevel.Length, 15);
            Console.Write(strOneLevel);

            //选着打印的字体背景颜色（三目运算符）选择颜色
            Console.ForegroundColor = nowIndexScene == 1 ? ConsoleColor.Red : ConsoleColor.White;
            //打印第一个字符串
            Console.SetCursorPosition(Game.w / 2 - strTwoLevel.Length, 17);
            Console.Write(strTwoLevel);

            //选项按键检测
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    --nowIndexScene;
                    if (nowIndexScene < 0)
                    {
                        nowIndexScene = 0;
                    }
                    break;
                case ConsoleKey.S:
                    ++nowIndexScene;
                    if (nowIndexScene > 1)
                    {
                        nowIndexScene = 1;
                    }
                    break;
                case ConsoleKey.J:
                    EnterJDoSomting();
                    break;
            }

        }
    }
}
