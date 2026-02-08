using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignement_3;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardState _lastKeyboardState;
    private DisplayWordFrequency _wordFrequencyDisplay;

    private SpriteFont _font;

    private bool displayingFrequency;
    
    DisplayUniqueWords _uniqueWords;
    
    
//test
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        displayingFrequency = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }
    

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("font/Charter");
        _uniqueWords = new DisplayUniqueWords("Content/text/uniquewords.txt");
        _uniqueWords.CallNewWords(
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height,
            _font,
            Color.White, Color.Yellow, Color.LightGreen
        );
        
        _wordFrequencyDisplay = new DisplayWordFrequency();
        _wordFrequencyDisplay.LoadContent(GraphicsDevice, _font);
        
        _lastKeyboardState = Keyboard.GetState();

        // TODO: use this.Content to load your game content here
    }
    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (_lastKeyboardState.IsKeyUp(Keys.Enter) && keyboardState.IsKeyDown(Keys.Enter))
        {
            displayingFrequency = !displayingFrequency;
        }

        if (displayingFrequency)
        {
            _wordFrequencyDisplay.HandleInput(keyboardState);
        }

        _lastKeyboardState = keyboardState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        if (!displayingFrequency)
        {
            _uniqueWords.Draw(_spriteBatch, _font); // show words
        }
        else
        {
            //word frequency add functionality 
            _wordFrequencyDisplay.Draw(_spriteBatch);
        }
        


        _spriteBatch.End();

        base.Draw(gameTime);
    }

}