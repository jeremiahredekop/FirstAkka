using System;
using Akka.Actor;
using ColoredConsole;
using FirstAkka.Messages;

namespace FirstAkka.Actors
{
    public class UserActor : ReceiveActor
    {

        private string _currentlyWatching;

        public UserActor()
        {
            ColorConsole.WriteLine("Creating user actor");

            ColorConsole.WriteLine("Setting initial behaviour to stopped".Cyan());
            Stopped();          
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(m => ColorConsole.WriteLine("Error: cannot start playing another movie before stopping existing one".Red()));
            Receive<StopMovieMessage>(m => StopPlayingCurrentMovie());

        }

        private void StopPlayingCurrentMovie()
        {
            _currentlyWatching = null;
            ColorConsole.WriteLine("User no longer watching movie".Cyan());
            Become(Stopped);
        }

        private void Stopped()
        {
            Receive<StopMovieMessage>(m => ColorConsole.WriteLine("Error: Cannot stop movie if nothing is playing.".Red()));
            Receive<PlayMovieMessage>(m =>
            {
                StartPlayingMovie(m);
            });
        }

        private void StartPlayingMovie(PlayMovieMessage m)
        {
            _currentlyWatching = m.MovieTitle;
            ColorConsole.WriteLine($"User now watching: {m.MovieTitle}".Cyan());
            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLine("User Actor Prestart".Green());
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLine("User Actor PostStop".Green());
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLine($"User Actor Prerestart because {message}".Green());
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLine($"User Actor PostRestart because {reason}".Green());
            base.PostRestart(reason);
        }
    }
}