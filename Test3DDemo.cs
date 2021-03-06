using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Test3DDemo2 : Game
    {
        GraphicsDeviceManager _graphics;

        //Camera
        Vector3 _camTarget;
        Vector3 _camPosition;
        Matrix _projectionMatrix;
        Matrix _viewMatrix;
        Matrix _worldMatrix;

        //Geometric info
        Model _model;

        //Orbit
        bool _orbit = false;

        public Test3DDemo2()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            //Setup Camera
            _camTarget = new Vector3(0f, 0f, 0f);
            _camPosition = new Vector3(0f, 0f, -5);
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                               MathHelper.ToRadians(45f), _graphics.
                               GraphicsDevice.Viewport.AspectRatio,
                1f, 1000f);
            _viewMatrix = Matrix.CreateLookAt(_camPosition, _camTarget,
                         new Vector3(0f, 1f, 0f));// Y up
            _worldMatrix = Matrix.CreateWorld(_camTarget, Vector3.
                          Forward, Vector3.Up);

            _model = Content.Load<Model>("MonoCube");
        }

        protected override void LoadContent()
        {
            new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _camPosition.X -= 0.1f;
                _camTarget.X -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _camPosition.X += 0.1f;
                _camTarget.X += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _camPosition.Y -= 0.1f;
                _camTarget.Y -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _camPosition.Y += 0.1f;
                _camTarget.Y += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                _camPosition.Z += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                _camPosition.Z -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _orbit = !_orbit;
            }

            if (_orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(
                                        MathHelper.ToRadians(1f));
                _camPosition = Vector3.Transform(_camPosition,
                              rotationMatrix);
            }
            _viewMatrix = Matrix.CreateLookAt(_camPosition, _camTarget,
                         Vector3.Up);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.AmbientLightColor = new Vector3(1f, 0, 0);
                    effect.View = _viewMatrix;
                    effect.World = _worldMatrix;
                    effect.Projection = _projectionMatrix;
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }
    }
}

