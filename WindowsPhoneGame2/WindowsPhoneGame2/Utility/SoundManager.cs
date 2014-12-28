using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;

namespace WindowsPhoneGame2
{
    public enum ESound
    {
        TouchType1,
        TouchType2,
        TouchType3,
        TouchType4,
        TouchType5,
        TouchType6,
        TouchType7,
        TouchType8,
        Fly,
        CompleteCell,
        FailCell,
        LevelComplete,
        ClassicComplete,
        TimeUp,
        SelectButton,
    }

    public enum ESong
    {
        Background
    }

    public class SoundManager
    {
        #region Attributes

        // Check if song is repeated
        public bool IsRepeating = true;
        bool Sound = true;
        // Songs
        Song bgSong;

        // Sound effects
        public SoundEffect SelectButton;
        public SoundEffect TouchType1;
        public SoundEffect TouchType2;
        public SoundEffect TouchType3;
        public SoundEffect TouchType4;
        public SoundEffect TouchType5;
        public SoundEffect TouchType6;
        public SoundEffect TouchType7;
        public SoundEffect TouchType8;

        public SoundEffect Fly;
        public SoundEffect CompleteCell;
        public SoundEffect FailCell;

        public SoundEffect LevelComplete;
        public SoundEffect ClassicComplete;
        public SoundEffect TimeUp;
        #endregion

        static SoundManager s_pInstance = null; // singleton

        #region Initialize
        public static SoundManager GetInstance()
        {
            if (s_pInstance == null)
            {
                s_pInstance = new SoundManager();
                return s_pInstance;
            }
            else
                return s_pInstance;
        }

        public void LoadContent(ContentManager Content)
        {
            if (!MediaPlayer.GameHasControl)
            {
                MediaPlayer.Pause();
                Guide.BeginShowMessageBox("There is already music playing", "would you want to turn it off?",
                    new List<string> { "Yes", "No" }, 0, MessageBoxIcon.Warning,
                    new AsyncCallback(OnMessageBoxAction), null);
            }
            else Global.SOUND = true;

            bgSong = Content.Load<Song>(@"Sounds\bgSong");

            // Sound effects
            SelectButton = Content.Load<SoundEffect>(@"Sounds\Effects\sfx_button");

            TouchType1 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type1");
            TouchType2 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type2");
            TouchType3 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type3");
            TouchType4 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type4");
            TouchType5 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type5");
            TouchType6 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type6");
            TouchType7 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type7");
            TouchType8 = Content.Load<SoundEffect>(@"Sounds\Touch_Type\sfx_Type8");

            Fly = Content.Load<SoundEffect>(@"Sounds\Effects\sfx_join2");
            CompleteCell = Content.Load<SoundEffect>(@"Sounds\Effects\sfx_joker2");
            FailCell = Content.Load<SoundEffect>(@"Sounds\Effects\sfx_join1");
            LevelComplete = Content.Load<SoundEffect>(@"Sounds\Effects\sfx_levelcomplete");
            ClassicComplete = Content.Load<SoundEffect>(@"Sounds\Effects\sfx_gameover");
            TimeUp = Content.Load<SoundEffect>(@"Sounds\Effects\sfx_timeup");
        }

        public void OnMessageBoxAction(IAsyncResult ar)
        {
            int? selectedButton = Guide.EndShowMessageBox(ar);
            switch (selectedButton)
            {
                case 0:
                    Global.SOUND = true;
                    SoundManager.GetInstance().PlaySong(ESong.Background);
                    break;
                case 1:
                    Global.SOUND = false;
                    MediaPlayer.Resume();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Public methods

        public void PlaySound(ESound id)
        {
            // Check if sound option is OFF
            if (Sound)
            {
                switch (id)
                {
                    case ESound.SelectButton:
                        if (SelectButton != null)
                            SelectButton.Play();
                        break;
                    case ESound.TouchType1:
                        if (TouchType1 != null)
                            TouchType1.Play();
                        break;
                    case ESound.TouchType2:
                        if (TouchType2 != null)
                            TouchType2.Play();
                        break;
                    case ESound.TouchType3:
                        if (TouchType3 != null)
                            TouchType3.Play();
                        break;
                    case ESound.TouchType4:
                        if (TouchType4 != null)
                            TouchType4.Play();
                        break;
                    case ESound.TouchType5:
                        if (TouchType5 != null)
                            TouchType5.Play();
                        break;
                    case ESound.TouchType6:
                        if (TouchType6 != null)
                            TouchType6.Play();
                        break;
                    case ESound.TouchType7:
                        if (TouchType7 != null)
                            TouchType7.Play();
                        break;
                    case ESound.TouchType8:
                        if (TouchType8 != null)
                            TouchType8.Play();
                        break;
                    case ESound.Fly:
                        if (Fly != null)
                            Fly.Play();
                        break;
                    case ESound.CompleteCell:
                        if (CompleteCell != null)
                            CompleteCell.Play();
                        break;
                    case ESound.FailCell:
                        if (FailCell != null)
                            FailCell.Play(); 
                        break;
                    case ESound.LevelComplete:
                        if (LevelComplete != null)
                            LevelComplete.Play();
                        break;
                    case ESound.ClassicComplete:
                        if (ClassicComplete != null)
                            ClassicComplete.Play();
                        break;
                    case ESound.TimeUp:
                        if (TimeUp != null)
                            TimeUp.Play();
                        break; 
                }
            }
        }

        public void PlaySong(ESong id)
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

        public void PauseSong()
        {
            if (Global.SOUND)
            {
                MediaPlayer.Pause();
            }
        }

        public void ResumeSong()
        {
            if (Global.SOUND)
            {
                MediaPlayer.Resume();
            }
        }

        public void MuteUnmuteSong()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }

        public void StopSongs()
        {
            MediaPlayer.Stop();
        }
        #endregion
    }
}
