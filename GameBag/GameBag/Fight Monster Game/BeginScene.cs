using System;
using System.Collections.Generic;
using System.Text;

namespace GameBag.Fight_Monster_Game
{
    internal class BeginScene:BeginOrEndScene
    {
        public BeginScene()
        {
            strTitle = "唐老狮打怪兽";
            strPrompt = "(按J或j键确定)";
            strOneLevel = "开始游戏";
            strTwoLevel = "退出游戏";
        }

        public override void EnterJDoSomting()
        {
            //用继承的nowIndexScene的值来判断按下J键后的逻辑
            if (nowIndexScene == 0)
            {
                Game.ChangeScene(E_GameSceneType.Game);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
