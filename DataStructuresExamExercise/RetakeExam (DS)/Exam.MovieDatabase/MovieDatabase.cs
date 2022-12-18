using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MovieDatabase
{
    public class MovieDatabase : IMovieDatabase
    {
        private HashSet<Actor> actors = new HashSet<Actor>();
        private HashSet<Movie> movies = new HashSet<Movie>();
        private Dictionary<string, HashSet<Movie>> moviesByActor = new Dictionary<string, HashSet<Movie>>();

        public void AddActor(Actor actor)
        {
            this.actors.Add(actor);
            this.moviesByActor.Add(actor.Id, new HashSet<Movie>());
        }

        public void AddMovie(Actor actor, Movie movie)
        {
            if (this.actors.Contains(actor) == false) throw new ArgumentException();
            
            this.moviesByActor[actor.Id].Add(movie);
            this.movies.Add(movie);
        }

        public bool Contains(Actor actor) => this.actors.Contains(actor);

        public bool Contains(Movie movie) => this.movies.Contains(movie);

        public IEnumerable<Actor> GetActorsOrderedByMaxMovieBudgetThenByMoviesCount()
        {
            return this.actors.OrderByDescending(x => this.moviesByActor[x.Id].Max(x => x.Budget))
                .ThenByDescending(x => this.moviesByActor[x.Id].Count);
        }

        public IEnumerable<Movie> GetAllMovies() => this.movies;

        public IEnumerable<Movie> GetMoviesInRangeOfBudget(double lower, double upper)
        {
            return this.movies.OrderByDescending(x => x.Rating).Where(x => x.Budget >= lower && x.Budget <= upper);
        }

        public IEnumerable<Movie> GetMoviesOrderedByBudgetThenByRating()
        {
            return this.movies.OrderByDescending(x => x.Budget).ThenByDescending(x => x.Rating);
        }

        public IEnumerable<Actor> GetNewbieActors() => this.actors.Where(x => this.moviesByActor[x.Id].Count == 0);
    }
}
