using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


namespace WindowsPhoneGame2
{
    public enum ESound
    {
        Lose,
        Win,
        Success,
        SelectButton
    }

    public enum ESong
    {
        Background
    }

    public static class SoundManager
    {
        #region Attributes

        // Check if song is repeated
        public static bool IsRepeating = true;

        // Songs
        private static Song bgSong;

        // Sound effects
        private static SoundEffect Lose;
        private static SoundEffect Win;
        private static SoundEffect Success;
        private static SoundEffect SelectButton;

        #endregion

        #region Initialize


        /// <summary>
        /// Load some songs and sound need for menu and other screens
        /// </summary>
        /// <param name="Content"></param>
        public static void LoadContent1(ContentManager Content)
        {
            //if (!MediaPlayer.GameHasControl)
            //{
            //    Engine.ShowDialog("");
            //}
            // Songs
            bgSong = Content.Load<Song>(@"Sounds\bgSong");

            // Sound effects
            Lose = Content.Load<SoundEffect>(@"Sounds\lose");
            Win = Content.Load<SoundEffect>(@"Sounds\win");
            Success = Content.Load<SoundEffect>(@"Sounds\success");
            SelectButton = Content.Load<SoundEffect>(@"Sounds\selectButton");


        }

        /// <summary>
        /// Load all songs and sound need for play game
        /// </summary>
        /// <param name="Content"></param>
        public static void LoadContent2(ContentManager Content)
        {

        }


        #endregion

        #region Public methods


        public static void PlaySound(ESound id)
        {
            // Check if sound option is OFF
            if (Global.SOUND)
            {
                switch (id)
                {
                    case ESound.Lose:
                        if (Lose != null)
                            Lose.Play();
                        break;
                    case ESound.Win:
                        if (Win != null)
                            Win.Play();
                        break;
                    case ESound.Success:
                        if (Success != null)
                            Success.Play();
                        break;
                    case ESound.SelectButton:
                        if (SelectButton != null)
                            SelectButton.Play();
                        break;
                }
            }
        }

        public static void PlaySong(ESong id)
        {
            MediaPlayer.IsRepeating = IsRepeating;

            // Check if sound option is OFF
            if (Global.SOUND)
            {
                switch (id)
                {
                    case ESong.Background:
                        if (bgSong != null)
                        {
                            MediaPlayer.Play(bgSong);
                            MediaPlayer.Volume = 2.0f;
                        }
                        break;
                }
            }
        }

        public static void PauseSong()
        {
            if (Global.SOUND)
            {
                MediaPlayer.Pause();
            }
        }

        public static void ResumeSong()
        {
            if (Global.SOUND)
            {
                MediaPlayer.Resume();
            }
        }

        public static void MuteUnmuteSong()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }

        public static void StopSongs()
        {
            MediaPlayer.Stop();
        }


        #endregion
    }
}
