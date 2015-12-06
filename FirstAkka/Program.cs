using System;
using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using ColoredConsole;
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

            
            var userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = _movieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            
            new[]
            {
                new PlayMovieMessage(42, "Akka.NET, the movie"),
                new PlayMovieMessage(99, "Partial Recall"),
                new PlayMovieMessage(77, "Boolean Lies"),
                new PlayMovieMessage(1, "Codenan the Destroyer")
            }
            .ToList()
            .ForEach(m =>
            {
                Console.ReadLine();
                ColorConsole.WriteLine($"Sending a Playmovie message: {m.MovieTitle} / ID: {m.UserId}".Yellow());
                userActorRef.Tell(m);
            });

            Enumerable.Range(1,4)
                .ToList()
                .ForEach(i =>
                {
                    Console.ReadLine();
                    ColoredConsole.ColorConsole.WriteLine("Sending a stopmovie message");
                    userActorRef.Tell(new StopMovieMessage());
                });
            

            Console.ReadLine();
            _movieStreamingActorSystem.Shutdown();
            _movieStreamingActorSystem.AwaitTermination();
            Console.WriteLine("Actor System Shutdown");

            Console.ReadLine();
        }
    }
}
