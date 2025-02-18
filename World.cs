﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace EasyMonoGame
{
    public class World
    {
        private Texture2D backgroundTile;
        private SortedSet<Type> updateOrder; // first is  updated first
        private SortedSet<Type> drawOrder; // first is drawn on top, that is drawn last
        internal Dictionary<Type, List<Actor>> actors; //TODO make private
        private List<Actor> actorsToRemove = new List<Actor>();
        private List<Actor> actorsToAdd = new List<Actor>();
        private Dictionary<Vector2, Text> texts;
        private bool isBounded = true;
        private int width;
        private int height;
        private string backgroundTileName;
        private Color backgroundColor = Color.CornflowerBlue;
        /// <summary>
        /// Inherit this class to create a world for your game.
        /// </summary>
        /// <param name="width">Width of world i pixels.</param>
        /// <param name="height">Height of world i pixels.</param>
        public World(int width, int height)
        {
            //graphics = new GraphicsDeviceManager(this);
            this.width =  width;
            this.height = height;
            
            //Content.RootDirectory = "Content";
            //IsMouseVisible = true;
            actors = new Dictionary<Type, List<Actor>>();
            texts = new Dictionary<Vector2, Text>();
        }
        /// <summary>
        /// Inherit this class to create a world for your game.
        /// </summary>
        /// <param name="width">Width of world i pixels.</param>
        /// <param name="height">Height of world i pixels.</param>
        /// <param name="isBounded">Set to false, to have an unbounded world.</param>
        public World(int width, int height, bool isBounded)
        {
            this.width = width;
            this.height = height;
            this.isBounded = isBounded;
            actors = new Dictionary<Type, List<Actor>>();
            texts = new Dictionary<Vector2, Text>();
        }
        /// <summary>
        /// Set or get the background color for this world.
        /// </summary>
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        /// <summary>
        /// Set or get the background image for this world. 
        /// If the image is smaller than the world the background wíll be tiled.
        /// </summary>
        public string BackgroundTileName
        {
            get { return backgroundTileName; }
            set { backgroundTileName = value; }
        }
            
        /// <summary>
        /// Returns the height of the world in pixels.
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
        }
        /// <summary>
        /// Returns the width of the world in pixels.
        /// </summary>
        public int Width
        {
            get 
            { 
                return width; 
            }
        }


        /// <summary>
        /// Add an actor to this world.
        /// </summary>
        /// <param name="actor"></param
        public void Add(Actor actor, string imageName, float x, float y)
        {
            actor.ImageName = imageName;
            actor.Position = new Vector2(x, y);
            actor.World = this;
            actorsToAdd.Add(actor);
        }
        /// <summary>
        /// Add actors to this world after call to Update.
        /// </summary>
        /// <param name="actor"></param>
        private void AddActor(Actor actor)
        {
            GameArt.Add(actor.ImageName);
            // LoadContent in EasyGame has been called and actor.Image is null. => Set image from GameArt.
            if (EasyGame.Instance.HasLoadedContent && actor.Image == null)
            {
                if (!GameArt.Contains(actor.ImageName))
                {                
                    // Load image to GameArt
                    GameArt.Add(actor.ImageName);
                    GameArt.Load();
                }
                actor.Image = GameArt.Get(actor.ImageName);
            }
            // if not has type => add type to dictionary and create list
            if (actors.TryGetValue(actor.GetType(), out List<Actor> actorsOfType))
            {
                actorsOfType.Add(actor);
                //actor.World = this;
            }
            else
            {
                List<Actor> actorsOfNewType = new List<Actor>();
                actorsOfNewType.Add(actor);
                actors.Add(actor.GetType(), actorsOfNewType);
                //actor.World = this;
            }

        }
        /// <summary>
        /// Get all actors of the specified type.
        /// </summary>
        /// <param name="actorType"></param>
        public List<Actor> GetActors(Type actorType)
        {
            
            if (actors.ContainsKey(actorType))
            {
                List<Actor> actorsOfTypeInWorld = new List<Actor>();
                foreach (var actor in actors[actorType])
                {
                    if (actor.World == this)
                    {
                        actorsOfTypeInWorld.Add(actor);
                    }
                        
                }
                if (actorsOfTypeInWorld.Count == 0)
                {
                    return null;
                }
                return actorsOfTypeInWorld;
            }
            return null;

        }
        /// <summary>
        /// Get the number of actors of the specified type in this world.
        /// </summary>
        /// <param name="actorType"></param>
        /// <returns></returns>
        public int NumberOfActors(Type actorType)
        {
            if (actors.ContainsKey(actorType))
            {
                int count = 0;
                foreach (var actor in actors[actorType])
                {
                    if (actor.World == this)
                    {
                        ++count;
                    }
                }
                return count;
            }
            return 0;
        }
        /// <summary>
        /// Show text at the specified coordinate. 
        /// 
        /// A new text at the same coordinate will replace the other text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>


        public void ShowText(string text, int x, int y, float size = 1f)
        {
            if (GameArt.GetFont() != null)
            { 
                Text textObject = new Text(
                    GameArt.GetFont(),
                    text,
                    new Vector2(x, y),
                    Color.White,
                    size);
                texts[new Vector2(x, y)] = textObject;
            }
        }
        /// </summary>
        /// 
        /// <summary>
        /// <param name="gameTime"></param>
        /// 

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            TileBackground(spriteBatch);

            foreach (var pair in actors)
            {
                foreach (var actor in pair.Value)
                {
                    actor.Draw(spriteBatch);
                }

            }
            foreach (var pair in texts)
            {
                pair.Value.Draw(spriteBatch);
            }
            


        }
        internal void LoadContent()
        {
            foreach (var pair in actors)
            {
                foreach (var actor in pair.Value)
                {
                    actor.Image = GameArt.Get(actor.ImageName);
                }

            }
        }
        /// <summary>
        /// Called once per frame. Override this method to add game logic.
        /// </summary>
        public virtual void Act()
        {
        }

        /// <summary>
        /// Called once per frame. Update the state of the world.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (var pair in actors)
            {
                foreach (var actor in pair.Value)
                {
                    if (actor.World == this)
                    {
                        actor.Update(gameTime);
                        // Keep actor inside of screen
                        if (isBounded)
                        {
                            // x-direciton
                            if (actor.X < 0)
                            {
                                actor.X = 0;
                            }
                            else if (this.Width < actor.X)
                            {
                                actor.X = this.Width;
                            }
                            // y-direction
                            if (actor.Y < 0)
                            {
                                actor.Y = 0;
                            }
                            else if (this.Height < actor.Y)
                            {
                                actor.Y = this.Height;
                            }
                        }
                    }
                }
                Act();

            }
            // Add actors that are marked for addition. 
            // Cannot add actor in the middle of the loop above, 
            // becase it will cause an Exception.
            foreach (Actor actor in actorsToAdd)
            {
                AddActor(actor);
            }
            actorsToAdd.Clear();
            // Remove actors that are marked for removal.
            foreach (Actor actor in actorsToRemove)
            {
                Remove(actor);
            }
            actorsToRemove.Clear();
        }



        private void TileBackground(SpriteBatch spriteBatch)
        {
            if (backgroundTileName == null)
            {
                return;
            }
            if (backgroundTile == null)
            {
                if (!GameArt.Contains(backgroundTileName))
                {
                    GameArt.Add(backgroundTileName);
                    GameArt.Load();
                }
                backgroundTile = GameArt.Get(backgroundTileName);
            }
            for (int x = 0; x < Width; x += backgroundTile.Width)
            {
                for (int y = 0; y < Height; y += backgroundTile.Height)
                {
                    spriteBatch.Draw(backgroundTile, new Vector2(x, y), Color.White);
                }
            }
        }
        /// <summary>
        /// Remove the specified actor from this world.
        /// </summary>
        /// <param name="actor"></param>

        public void RemoveActor(Actor actor)
        {
            actor.World = null;
            actorsToRemove.Add(actor);
        }
        private void Remove(Actor actor)
        {
            List<Actor> actorsOfType = actors[actor.GetType()];
            if (actorsOfType != null)
            {
                actorsOfType.Remove(actor);
            }
        }

    }
}
