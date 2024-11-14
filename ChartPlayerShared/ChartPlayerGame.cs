﻿using System;
using Microsoft.Xna.Framework;
using UILayout;

namespace ChartPlayer
{
    public class ChartPlayerGame : MonoGameLayout
    {
        public static ChartPlayerGame Instance { get; private set; }

        public ChartPlayerPlugin Plugin { get; set; }
        public Scene3D Scene3D { get; set; }

        public ChartPlayerGame()
        {
            Instance = this;
        }

        public override void SetHost(Game host)
        {
            base.SetHost(host);

            Host.InactiveSleepTime = TimeSpan.Zero;

            LoadImageManifest("ImageManifest.xml");

            GraphicsContext.SingleWhitePixelImage = GetImage("SingleWhitePixel");

            DefaultFont = GetFont("MainFont");
            DefaultFont.SpriteFont.EmptyLinePercent = 0.5f;

            DefaultForegroundColor = UIColor.White;

            DefaultOutlineNinePatch = GetImage("PopupBackground");

            DefaultPressedNinePatch = GetImage("ButtonPressed");
            DefaultUnpressedNinePatch = GetImage("ButtonUnpressed");

            InputManager.AddMapping("PreviousPage", new KeyMapping(InputKey.PageUp) { DoRepeat = true });
            InputManager.AddMapping("NextPage", new KeyMapping(InputKey.PageDown) { DoRepeat = true });
            InputManager.AddMapping("NextItem", new KeyMapping(InputKey.Down) { DoRepeat = true });
            InputManager.AddMapping("PreviousItem", new KeyMapping(InputKey.Up) { DoRepeat = true });
            InputManager.AddMapping("FirstItem", new KeyMapping(InputKey.Home));
            InputManager.AddMapping("LastItem", new KeyMapping(InputKey.End));
            InputManager.AddMapping("PlayCurrent", new KeyMapping(InputKey.Enter));
            InputManager.AddMapping("PreciseClick", new KeyMapping(InputKey.LeftShift, InputKey.RightShift));
            InputManager.AddMapping("FastForward", new KeyMapping(InputKey.Right) {  DoRepeat = true });
            InputManager.AddMapping("Rewind", new KeyMapping(InputKey.Left) {  DoRepeat = true });
            InputManager.AddMapping("ToggleFavorite", new KeyMapping(InputKey.D8) { Modifier = InputKey.LeftShift });
            InputManager.AddMapping("ToggleFavorite", new KeyMapping(InputKey.D8) { Modifier = InputKey.RightShift });
            InputManager.AddMapping("ToggleFavorite", new KeyMapping(InputKey.Multiply));


            InputManager.AddMapping("PauseGame", new KeyMapping(InputKey.Space));

            RootUIElement = new SongPlayerInterface();
        }

        public override void Draw()
        {
            if (Scene3D != null)
            {
                Scene3D.Camera.ViewportWidth = (int)Layout.Current.Bounds.Width;
                Scene3D.Camera.ViewportHeight = (int)Layout.Current.Bounds.Height;

                Scene3D.Draw();
            }

            base.Draw();
        }

        public override void Exiting()
        {
            SongPlayerInterface.Instance.Exit();

            base.Exiting();
        }
    }
}