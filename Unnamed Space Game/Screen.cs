﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{
    class Screen
    {
        public SoundEffectInstance introMusic;
        public SoundEffectInstance music;
        protected MouseState mousy;
        public List<Keys> binds;
        protected bool nou;
        protected bool uno;
        public bool heldMouse;
        bool introDone;
        protected bool keysDown = false;
        protected KeyboardState Maryland;
        protected bool playMusic;
        public Screen()
            : this(null, null) { }
        public Screen(SoundEffect m)
            : this(m, null) { }
        public Screen(SoundEffect m, SoundEffect im)
        {
            playMusic = true;
            Maryland = new KeyboardState();
            mousy = new MouseState();
            nou = false;
            uno = false;
            music = null;
            introMusic = null;
            nou = false;
            if (m != null)
            {
                music = m.CreateInstance();
                music.IsLooped = true;
                if (im != null)
                {
                    introMusic = im.CreateInstance();
                    introDone = false;
                }
                else
                {
                    introDone = true;
                }
            }
            else
            {
                introDone = true;
            }
        }
        public virtual void changeBinds(List<Keys> newBinds, List<bool> bools)
        {
            playMusic = bools[0];
            if (!playMusic)
            {
                StopMusic();
            }
        }
        public virtual List<bool> GetBools()
        {
            return new List<bool>();
        }
        public void StopMusic()
        {
            if (music != null)
            {
                introDone = false;
                music.Stop();
                if (introMusic != null)
                {
                    introMusic.Stop();
                }
            }
        }
        public virtual void Start()
        {
            keysDown = true;
            heldMouse = true;
            if (playMusic)
            {
                if (introMusic == null)
                {
                    if (music != null && music.State != SoundState.Playing)
                    {
                        music.Play();
                    }
                    return;
                }
                if (music.State == SoundState.Playing)
                {
                    return;
                }
                introMusic.Play();
            }
            introDone = false;
        }
        public virtual void Update(GameTime time, Screenmanager manny)
        {
            Play(time);
            Maryland = Keyboard.GetState();
            if (Maryland.GetPressedKeyCount() == 0)
            {
                keysDown = false;
            }
            if (mousy.LeftButton == ButtonState.Pressed || mousy.RightButton == ButtonState.Pressed)
            {
                heldMouse = true;
            }
            mousy = Mouse.GetState();
            nou = false;
            if (mousy.LeftButton == ButtonState.Pressed)
            {
                nou = true;
            }
            else
            {
                heldMouse = false;
            }
            uno = false;
            if (mousy.RightButton == ButtonState.Pressed)
            {
                uno = true;
            }
        }

        public void PlayMusic()
        {
            if (!introDone && playMusic && (introMusic == null || introMusic.State == SoundState.Stopped))
            {
                music.Play();
                introDone = true;
            }
        }
        public virtual void Play(GameTime time)
        {
            if (music != null)
            {
                PlayMusic();
            }
        }

        public virtual void Transfer(int transfer)
        {

        }
        public virtual void Reset()
        {

        }
        public virtual void Draw(SpriteBatch batch)
        {

        }
    }
}
