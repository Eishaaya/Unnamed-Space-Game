﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Unnamed_Space_Game
{
    class LoseScreen : Screen
    {
        Button menu;
        Button back;
        Sprite tint;
        Sprite lose;
        Label score;
        List<int> scores;
        List<Label> topScores;
        SpriteFont font;
        public LoseScreen(Sprite dark, Sprite Loser, Button menuButt, Button RestartButt, SpriteFont Font)
            : base()
        {
            tint = dark;
            lose = Loser;
            menu = menuButt;
            back = RestartButt;
            font = Font;
        }

        public override void Transfer (int gamescore)
        {
            score = new Label(font, Color.LightGray, new Vector2(240, 285), $"Your Score: {gamescore}", new TimeSpan(0, 0, 1));

            //Read
            topScores = new List<Label>();
            scores = new List<int>();
            var dataJSON = File.ReadAllText("data.json");
            scores = JsonSerializer.Deserialize<List<int>>(dataJSON);
            scores.Add(gamescore);
            scores.Sort();
            scores.Reverse();
            for (int e = 0; e < 2; e++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i + e * 5 >= scores.Count)
                    {
                        for (int j = i; j < 5; j++)
                        {
                            topScores.Add(new Label(font, Color.LightGray, new Vector2(170 + 130 * e, 320 + j * 30), $"#{j + 1 + e * 5} Score: 0", new TimeSpan(0, 0, 1)));
                        }
                        break;
                    }
                    topScores.Add(new Label(font, Color.LightGray, new Vector2(170 + 130 * e, 320 + i * 30), $"#{i + 1 + e * 5} Score: {scores[i + e * 5]}", new TimeSpan(0, 0, 1)));
                    if (scores[i + e * 5] == gamescore)
                    {
                        topScores[i + e * 5].Color = Color.Yellow;
                    }
                }
            }

            var stuffToWrite = JsonSerializer.Serialize(scores);
            File.WriteAllText("data.json", stuffToWrite);
        }

        public override void Update(GameTime time, Screenmanager manny)
        {
            base.Update(time, manny);
            if (heldMouse)
            {
                return;
            }
            if (menu.check(mousy.Position.ToVector2(), nou))
            {
                manny.next(0, true);
                manny.previousScreens.Pop();
                manny.previousScreens.Pop().Reset();
                manny.clearMemory();
                return;
            }
            if (back.check(mousy.Position.ToVector2(), nou))
            {
                manny.back();
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            tint.Draw(batch);
            lose.Draw(batch);
            back.Draw(batch);
            menu.Draw(batch);
            score.write(batch);
            for (int i = 0; i < topScores.Count; i++)
            {
                topScores[i].write(batch);
            }    
        }
    }
}
