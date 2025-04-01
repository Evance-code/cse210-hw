using System;
using System.Collections.Generic;

namespace YouTubeVideoProgram
{
    // Comment class to store commenter's name and the comment text
    public class Comment
    {
        public string Name { get; set; }
        public string Text { get; set; }

        // Constructor to initialize the commenter's name and text
        public Comment(string name, string text)
        {
            Name = name;
            Text = text;
        }

        // Override ToString to display the comment in a readable format
        public override string ToString()
        {
            return $"{Name}: {Text}";
        }
    }

    // Video class to store video details and associated comments
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }  // Length in seconds
        public List<Comment> Comments { get; set; }

        // Constructor to initialize the video details and the comments list
        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            Comments = new List<Comment>();
        }

        // Method to add a comment to the video
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        // Method to get the number of comments
        public int GetCommentCount()
        {
            return Comments.Count;
        }

        // Method to display all comments for the video
        public void DisplayComments()
        {
            if (Comments.Count > 0)
            {
                foreach (var comment in Comments)
                {
                    Console.WriteLine(comment);
                }
            }
            else
            {
                Console.WriteLine("No comments yet.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to store video objects
            List<Video> videos = new List<Video>();

            // Create Video objects and add comments to them
            Video video1 = new Video("How to Program in C#", "Alice", 300);
            video1.AddComment(new Comment("John", "Great tutorial, very helpful!"));
            video1.AddComment(new Comment("Sarah", "I learned a lot from this video!"));
            video1.AddComment(new Comment("Mike", "Could you explain more about loops?"));

            Video video2 = new Video("Introduction to Data Science", "Bob", 600);
            video2.AddComment(new Comment("Emily", "Data science is fascinating!"));
            video2.AddComment(new Comment("David", "Looking forward to part 2!"));

            Video video3 = new Video("Exploring the Universe", "Charlie", 1200);
            video3.AddComment(new Comment("Lucas", "This is mind-blowing!"));
            video3.AddComment(new Comment("Sophia", "I need to know more about space!"));
            video3.AddComment(new Comment("Olivia", "Great video, love the visuals!"));

            // Add the video objects to the list
            videos.Add(video1);
            videos.Add(video2);
            videos.Add(video3);

            // Iterate through the list of videos and display their details
            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.Length} seconds");
                Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
                Console.WriteLine("Comments:");
                video.DisplayComments();
                Console.WriteLine("\n" + new string('-', 40) + "\n");
            }
        }
    }
}
