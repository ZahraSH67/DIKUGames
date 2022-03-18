using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;

namespace Galaga {
    public class Score {
        private int score;
        private Text display;
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
            display.SetColor(System.Drawing.Color.White);
        }
        public void AddPoints() {
            score++;
            display.SetText(score.ToString());
        }
        public void RenderScore() {
            display.RenderText();
        }
    }
}
