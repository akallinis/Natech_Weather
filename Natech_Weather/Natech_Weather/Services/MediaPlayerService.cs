using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.UI.Views;

namespace Natech_Weather.Services
{
    public class MediaPlayerService : IMediaPlayerService
    {
        ISimpleAudioPlayer _player;
        public MediaPlayerService()
        {
            _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        }

        public void PlayError()
        {
            _player.Load("error.mp3");
            _player.Play();
        }

        public void PlaySuccess()
        {
            _player.Load("success.mp3");
            _player.Play();
        }
    }
}
