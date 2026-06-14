using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    internal class EndScene:BeginOrEndScene
    {
        public EndScene()
        {
            strTitle = Game.isWin?"游戏胜利":"游戏失败";
            strPrompt = "(按J或j键确定)";
            strOneLevel = "返回主界面";
            strTwoLevel = "退出游戏";
        }

        public override void EnterJDoSomting()
        {
            //用继承的nowIndexScene的值来判断按下J键后的逻辑
            if (nowIndexScene == 0)
            {
                Game.ChangeScene(E_GameSceneType.Begin);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
