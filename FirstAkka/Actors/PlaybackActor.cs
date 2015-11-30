using System;
using Akka.Actor;
using ColoredConsole;
using FirstAkka.Messages;

namespace FirstAkka.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a playback actor");
            Receive<PlayMovieMessage>(m =>
                ColorConsole.WriteLine($"Received playmovie message.  Title: {m.MovieTitle}, ID: {m.UserId}".Yellow()));
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLine("Playback Actor Prestart".Green());
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLine("Playback Actor PostStop".Green());
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLine($"Playback Actor Prerestart because {message}".Green());
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLine($"Playback Actor PostRestart because {reason}".Green());
            base.PostRestart(reason);
        }
    }
}