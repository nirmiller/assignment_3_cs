using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignement_3;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private KeyboardState _lastKeyboardState;
    private MouseState _lastMouseState;
    
    private SpriteFont _charter;
    private Color _yellow;
    private Color _red;
    private Color _orange;
    private Color _maroon;
    private Color _navy;

    private DisplayUniqueWords _displayUniqueWords;
    
    
//test
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        //_graphics.PreferredBackBufferHeight = 500;
        //_graphics.PreferredBackBufferWidth = 500;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _yellow = new Color(251f / 255f, 222f / 255f, 156f / 255f);
        _red = new Color(199f / 255f, 78f / 255f, 81f / 255f);
        _orange = new Color(249f / 255f, 146f / 255f, 86f / 255f);
        _navy = Color.Navy;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        
        _charter = Content.Load<SpriteFont>("font/Charter");
        _displayUniqueWords = new DisplayUniqueWords("Content/text/uniquewords.txt");
        
        // creating 700 x 600 screen size
        _displayUniqueWords.CreateList(700, 600, _charter, _red, _orange,
            _navy);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        MouseState mouseState = Mouse.GetState();

        if (_lastMouseState.LeftButton == ButtonState.Released &&
            mouseState.LeftButton == ButtonState.Pressed)
        {
            _displayUniqueWords.CallNewWords(700, 600, _charter, _red,
                _orange, _navy);
        }

        _lastMouseState = mouseState;
        
        KeyboardState keyboardState = Keyboard.GetState();
        
        if (_lastKeyboardState.IsKeyDown(Keys.Enter) && keyboardState.IsKeyUp(Keys.Enter))
        {
           // TODO: Change display 

        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(_yellow);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        
        _displayUniqueWords.Draw(_spriteBatch, _charter);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}