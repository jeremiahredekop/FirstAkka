using System;
using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using FirstAkka.Actors;
using FirstAkka.Messages;

namespace FirstAkka
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem ;
        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            Props playbackActorProps = Props.Create<PlaybackActor>();

            IActorRef playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            new[]
            {
                new PlayMovieMessage(42, "Akka.NET, the movie"),
                new PlayMovieMessage(99, "Partial Recall"),
                new PlayMovieMessage(77, "Boolean Lies"),
                new PlayMovieMessage(1, "Codenan the Destroyer")
            }
            .ToList()
            .ForEach(playbackActorRef.Tell);

            playbackActorRef.Tell(PoisonPill.Instance);

            Console.ReadLine();
            _movieStreamingActorSystem.Shutdown();
            _movieStreamingActorSystem.AwaitTermination();
            Console.WriteLine("Actor System Shutdown");

            Console.ReadLine();
        }
    }
}
