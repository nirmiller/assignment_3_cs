using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace assignement_3;

public class DisplayUniqueWords
{
    private string[] _uniqueWords;
    private List<(string individualWord, Vector2 position, Color color)> _wordsOnScreen;
    private Random _randIndex;

    // constructor 
    public DisplayUniqueWords(string uniqueWordFilePath)
    {
        _uniqueWords = File.ReadAllLines(uniqueWordFilePath);
        _wordsOnScreen = new List<(string, Vector2, Color)>();
        _randIndex = new Random();
    }

    public void CreateList(int screenWidth, int screenHeight, SpriteFont font, Color color1, 
        Color color2, Color color3)
    {
        // setting starting positons and max position words can be 
        float border = 50f;
        float wordSpacing = 10f;
        float lineHeight = font.LineSpacing;
        
        float xStart = border;
        float yStart = border;
        
        float maxX = screenWidth - border;
        float maxY = screenHeight - border;

        float x = xStart;
        float y = yStart;

        int lineNumber = 0;

        //will clear words saved from previous calls 
        _wordsOnScreen.Clear();

        // loop that is valid while lenght of text written is not longer than boundaries 
        while (true)
        {
            string word = _uniqueWords[_randIndex.Next(_uniqueWords.Length)];
            Vector2 wordSize = font.MeasureString(word);

            if (y + wordSize.Y > maxY) break;

            if (x + wordSize.X + wordSpacing > maxX)
            {
                x = xStart;
                y += lineHeight;
                lineNumber++;
            }

            if (y + wordSize.Y > maxY) break;            
            // change color each line
            Color wordColor;
            if (lineNumber % 3 == 0)
            {
                wordColor = color1;
            }
            else if (lineNumber % 3 == 1)
            {
                wordColor = color2;
            }
            else
            {
                wordColor = color3;
            }
                
            _wordsOnScreen.Add((word, new Vector2(x, y), wordColor));
            x += wordSize.X + wordSpacing;
        }
    }
    //drawing the words 
    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        foreach (var (word, position, color) in _wordsOnScreen)
        {
            spriteBatch.DrawString(font, word, position, color);
        }
    }

    public void CallNewWords(int screenWidth, int screenHeight, SpriteFont font, Color color1, 
        Color color2, Color color3)
    {
        CreateList(screenWidth, screenHeight, font, color1, color2, color3);
    }
}
