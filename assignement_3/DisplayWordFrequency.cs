using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignement_3
{
    public class DisplayWordFrequency
    {
        private Dictionary<int, int> _frequencyCounts;
        private Texture2D _pixel;
        private SpriteFont _font;
        private int _scrollOffset = 0;

        public DisplayWordFrequency()
        {
            _frequencyCounts = new Dictionary<int, int>();
        }
        
        public void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Down))
                _scrollOffset += 10;

            if (keyboard.IsKeyDown(Keys.Up))
                _scrollOffset = Math.Max(0, _scrollOffset - 10);
        }
        

        public void LoadContent(GraphicsDevice graphicsDevice, SpriteFont font)
        {
            _font = font;

            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            using var stream = TitleContainer.OpenStream("Content/text/wordfrequency.txt");
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':');
                int frequency = int.Parse(parts[0].Trim());
                int count = int.Parse(parts[1].Trim());

                _frequencyCounts[frequency] = count;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int startX = 50;
            int startY = 80;
            int barHeight = 18;
            int spacing = 6;

            int maxCount = 0;
            foreach (var value in _frequencyCounts.Values)
                maxCount = Math.Max(maxCount, value);

            int maxBarWidth = 500;

            int i = 0;
            foreach (var pair in _frequencyCounts)
            {
                float normalized = (float)pair.Value / maxCount;
                int barWidth = (int)(normalized * maxBarWidth);

                Rectangle barRect = new Rectangle(
                    startX,
                    startY + i * (barHeight + spacing) - _scrollOffset,
                    barWidth,
                    barHeight
                );

                spriteBatch.Draw(_pixel, barRect, Color.CornflowerBlue);

                spriteBatch.DrawString(
                    _font,
                    $"{pair.Key} -> {pair.Value}",
                    new Vector2(startX + barWidth + 10, barRect.Y),
                    Color.White
                );

                i++;
            }
        }
    }
}