using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.ViTube
{
    public class ViTubeRepository : IViTubeRepository
    {
        private HashSet<User> users = new HashSet<User>();
        private HashSet<Video> videos = new HashSet<Video>();
        private Dictionary<string, HashSet<Video>> watched = new Dictionary<string, HashSet<Video>>();
        private Dictionary<string, HashSet<Video>> activity = new Dictionary<string, HashSet<Video>>();
        public bool Contains(User user) => this.users.Contains(user);

        public bool Contains(Video video) => this.videos.Contains(video);

        public void DislikeVideo(User user, Video video)
        {
            if (this.Contains(user) == false || this.Contains(video) == false) throw new ArgumentException();
            
            video.Dislikes++;
            activity[user.Id].Add(video);
        }

        public IEnumerable<User> GetPassiveUsers() => this.users.Where(x => this.activity[x.Id].Count == 0 && this.watched[x.Id].Count == 0);

        public IEnumerable<User> GetUsersByActivityThenByName() => this.users.OrderByDescending(x => this.watched[x.Id].Count)
            .ThenByDescending(x => this.activity[x.Id].Count).ThenBy(x => x.Username);

        public IEnumerable<Video> GetVideos() => this.videos;

        public IEnumerable<Video> GetVideosOrderedByViewsThenByLikesThenByDislikes()
        {
            return this.videos.OrderByDescending(x => x.Views).ThenByDescending(x => x.Likes).ThenBy(x => x.Dislikes);
        }

        public void LikeVideo(User user, Video video)
        {
            if (this.Contains(user) == false || this.Contains(video) == false) throw new ArgumentException();
            
            video.Likes++;
            activity[user.Id].Add(video);
        }

        public void PostVideo(Video video)
        {
            this.videos.Add(video);
        }

        public void RegisterUser(User user)
        {
            this.users.Add(user);
            this.activity.Add(user.Id, new HashSet<Video>());
            this.watched.Add(user.Id, new HashSet<Video>());
        }

        public void WatchVideo(User user, Video video)
        {
            if (this.Contains(user) == false || this.Contains(video) == false) throw new ArgumentException();

            this.watched[user.Id].Add(video);
            video.Views++;
        }
    }
}
