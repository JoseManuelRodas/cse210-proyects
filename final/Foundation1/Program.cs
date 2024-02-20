using System;

public abstract class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public abstract int GetNumberOfComments();
    public abstract void Display();
}

public class SpecificVideo : Video
{
    public List<Comment> Comments { get; set; }

    public SpecificVideo(string title, string author, int length, List<Comment> comments)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = comments;
    }
    public override int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public override void Display()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (Comment comment in Comments)
        {
            Console.WriteLine($"- {comment.Name}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
 
class Program1
{
    static void Main(string[] args)
    {
        List<Comment> comments1 = new List<Comment>();
        comments1.Add(new Comment("Alice324", "Excellent video, the narration was very clear!"));
        comments1.Add(new Comment("@bob", "Everything looks great, very good editing"));
        comments1.Add(new Comment("charlielozano@gmail.com", "Where does all that information come from?"));

        List<Comment> comments2 = new List<Comment>();
        comments2.Add(new Comment("@koketaluv24", "Im not mormon but i love the meaning behind this story.... I too was cut down by god, only to grow stronger!"));
        comments2.Add(new Comment("ErickLopez-zj3vb", "My favorite video of all time… I wish everyone reading this the best in life no matter what you may be going through!"));
        comments2.Add(new Comment("chloeprit", "I always come back to this video. Right now I'm struggling to abide by God's will. Watching this video makes me feel better."));

        List<Comment> comments3 = new List<Comment>();
        comments3.Add(new Comment("kathleenrussell254", "My dear, gentle brother.  You are such an inspiration to me. thank you"));
        comments3.Add(new Comment("sydjames3113", "Thank goodness for living prophets, seers, and revelators in this dark world. May they guide us in the path that is lite  so that we can make it home again."));
        comments3.Add(new Comment("@9j9517 ", "We love you, President Eyring. We pray for you. Thank you for your inspired messages."));

        SpecificVideo video1 = new SpecificVideo("The history of the Latter-day Saints", "Josemarodas624", 350, comments1);
        SpecificVideo video2 = new SpecificVideo("The Will of God", "The Church of Jesus Christ of Latter-day Saints", 182, comments2);
        SpecificVideo video3 = new SpecificVideo("Finding Personal Peace | Henry B. Eyring | April 2023 General Conference", "General Conference", 954, comments3);

        List<Video> videos = new List<Video>();
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        foreach (Video video in videos)
        {
            video.Display();
        }
    }
}
