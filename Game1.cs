/*
    CSC 395-02: Game Design & Programming
    Assignment 1: Getting Started in MonoGame
    Author : Havin Lim
    Date : 02/05/2024
*/


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameStart
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _image;

        // The velocity of the image(hamster)
        private Vector2 _velocity;
        // The angular velocity of the image(hamster)
        private float _angularVelocity;

        // The initial position of the image(hamster)
        private Vector2 _position;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Random rnd = new Random();
            
            // Set the initial position and velocity of the image(hamster)
            // The velocity of the hamster is set randomly between 3 and 5
            _position = new Vector2(400, 240);
            _velocity = new Vector2(rnd.Next(3,5), rnd.Next(3,5));

            // The angular velocity of the hamster is set to 0.005f
            _angularVelocity += 0.005f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _image = Content.Load<Texture2D>("hamster");
        }

        protected override void Update(GameTime gameTime)
        {
            // The position of the image(hamster) is updated by adding the velocity to it
            _position += _velocity;

            // The angular velocity of the image(hamster) is updated by adding 0.005f to it
            _angularVelocity += 0.005f;

            // The rotation of the image(hamster) is set to the angular velocity
            var rotation = MathHelper.WrapAngle(_angularVelocity);

            // Calculate the height and width of the image(hamster) at an angle
            double H = Math.Abs(_image.Width * Math.Sin(_angularVelocity)) + Math.Abs(_image.Height * Math.Cos(_angularVelocity));
            double W = Math.Abs(_image.Width * Math.Cos(_angularVelocity)) + Math.Abs(_image.Height * Math.Sin(_angularVelocity));


            // If the image(hamster) is at the edge of the screen, the velocity is reversed
            // meaning that the image(hamster) will bounce off the edge
            if (_position.Y < H/2 || _position.Y > _graphics.PreferredBackBufferHeight - H/2)
            {
                _velocity.Y *= -1;
            }

            if (_position.X < W/2 || _position.X > _graphics.PreferredBackBufferWidth - W/2)
            {
                _velocity.X *= -1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_image, _position, null, Color.White, _angularVelocity, new Vector2(_image.Width / 2, _image.Height / 2), 1.0f, SpriteEffects.None, 0.0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
