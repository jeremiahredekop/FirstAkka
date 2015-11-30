namespace FirstAkka.Messages
{
    public class PlayMovieMessage
    {
        public PlayMovieMessage(int userId, string movieTitle)
        {
            UserId = userId;
            MovieTitle = movieTitle;
        }

        public int UserId { get; private set; }
        public string MovieTitle { get; private set; } 
    }
}