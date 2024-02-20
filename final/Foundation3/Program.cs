using System;

class Event
{
    private string title;
    private string description;
    private string date;
    private string time;
    private Address address;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string Date
    {
        get { return date; }
        set { date = value; }
    }

    public string Time
    {
        get { return time; }
        set { time = value; }
    }

    public Address Address
    {
        get { return address; }
        set { address = value; }
    }

    public Event(string title, string description, string date, string time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public virtual string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date}\nTime: {time}\nAddress: {address}";
    }

    public virtual string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Event";
    }

    public virtual string GetShortDescription()
    {
        return $"Event: {title} on {date}";
    }
}

class Lecture : Event
{
    private string speaker;
    private int capacity;

    public string Speaker
    {
        get { return speaker; }
        set { speaker = value; }
    }

    public int Capacity
    {
        get { return capacity; }
        set { capacity = value; }
    }

    public Lecture(string title, string description, string date, string time, Address address, string speaker, int capacity) : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nSpeaker: {speaker}\nCapacity: {capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Lecture: {Title} on {Date}";
    }
}

class Reception : Event
{
    private string rsvpEmail;

    public string RsvpEmail
    {
        get { return rsvpEmail; }
        set { rsvpEmail = value; }
    }

    public Reception(string title, string description, string date, string time, Address address, string rsvpEmail) : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nRSVP Email: {rsvpEmail}";
    }

    public override string GetShortDescription()
    {
        return $"Reception: {Title} on {Date}";
    }
}

class OutdoorGathering : Event
{
    private string weather;

    public string Weather
    {
        get { return weather; }
        set { weather = value; }
    }

    public OutdoorGathering(string title, string description, string date, string time, Address address, string weather) : base(title, description, date, time, address)
    {
        this.weather = weather;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nWeather: {weather}";
    }

    public override string GetShortDescription()
    {
        return $"Outdoor Gathering: {Title} on {Date}";
    }
}

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public string Street
    {
        get { return street; }
        set { street = value; }
    }

    public string City
    {
        get { return city; }
        set { city = value; }
    }

    public string State
    {
        get { return state; }
        set { state = value; }
    }

    public string Country
    {
        get { return country; }
        set { country = value; }
    }

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public override string ToString()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("324 Madison Avenue", "New York", "NY", "USA");
        Address address2 = new Address("456 San Isidro", "Lima", "LMA", "Peru");
        Address address3 = new Address("700 Camacho Avenue", "La Paz", "LP", "Bolivia");

        Lecture lecture1 = new Lecture("Book of mormon 1", "A beginner's guide to the scriptures of LDS", "03/03/2024", "10:00 AM", address1, "Jon Skeet", 100);
        Reception reception1 = new Reception("New Years Party", "Celebrate the new year with us!", "12/31/2024", "11:00 PM", address2, "newyearpartyinlima@gmail.com");
        OutdoorGathering outdoorGathering1 = new OutdoorGathering("Picnic in the Park", "Enjoy a relaxing day in the nature", "03/04/2024", "12:00 PM", address3, "Instituto SUD de La Paz");

        List<Event> events = new List<Event>();
        events.Add(lecture1);
        events.Add(reception1);
        events.Add(outdoorGathering1);

        foreach (Event e in events)
        {
            Console.WriteLine(e.GetFullDetails());
            Console.WriteLine(e.GetShortDescription());
            Console.WriteLine("-------------------");
        }
    }
}
