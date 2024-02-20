using System;
public class Product
{
    private string name;
    private int productId;
    private double price;
    private int quantity;

    public Product(string name, int productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }
    public double GetTotalCost()
    {
        return price * quantity;
    }

    public override string ToString()
    {
        return $"{name} ({productId})";
    }
}

public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }
    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    public override string ToString()
    {
        return $"{name}\n{address}";
    }
}
public class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country == "USA";
    }

    public override string ToString()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

public class Order
{
    private List<Product> products;
    private Customer customer;

    public List<Product> Products { get; set; }
    public Customer Customer { get; set; }

    public Order(List<Product> products, Customer customer)
    {
        this.products = products;
        this.customer = customer;
    }
    public double GetTotalPrice()
    {
        double totalPrice = 0;
        foreach (Product product in products)
        {
            totalPrice += product.GetTotalCost();
        }
        if (customer.IsInUSA())
        {
            totalPrice += 5; 
        }
        else
        {
            totalPrice += 35;
        }
        return totalPrice;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (Product product in products)
        {
            packingLabel += $"{product}\n";
        }
        return packingLabel;
    }
    public string GetShippingLabel()
{
    string shippingLabel = "Shipping Label:\n";
    shippingLabel += $"{customer}";
    if (customer.IsInUSA())
    {
        shippingLabel += "\nShipping Cost: $5";
    }
    else
    {
        shippingLabel += "\nShipping Cost: $35";
    }
        shippingLabel += "\nLives in USA: " + customer.IsInUSA();
    return shippingLabel;
}

}

class Program
{
    static void Main(string[] args)
    {
        Product product1 = new Product("Book of mormon", 101, 5, 20);
        Product product2 = new Product("Pen", 102, 3, 5);
        Product product3 = new Product("Shirt", 103, 3, 30);
        Product product4 = new Product("Hat", 104, 2, 10);
        Product product5 = new Product("Camera", 105, 1, 100);
        Product product6 = new Product("Watch", 106, 2, 40);

        Address address1 = new Address("Cantoria Ave, 324", "Miami", "FL", "USA");
        Address address2 = new Address("Illimani Ave, 700", "La Paz", "LP", "Bolivia");
        Address address3 = new Address("Maryland Ave, 3245", "Miami", "FL", "USA");
        Address address4 = new Address("Av. Brasil, 3738", "Sao Paulo", "SP", "Brazil");

        Customer customer1 = new Customer("Samir Condori", address1);
        Customer customer2 = new Customer("Camila Aguilar", address2);
        Customer customer3 = new Customer("Pierre Smith", address3);
        Customer customer4 = new Customer("João Da Silva", address4);

        List<Product> products1 = new List<Product>();
        products1.Add(product1);
        products1.Add(product2);
        Order order1 = new Order(products1, customer1);

        List<Product> products2 = new List<Product>();
        products2.Add(product3);
        products2.Add(product4);
        Order order2 = new Order(products2, customer2);

        List<Product> products3 = new List<Product>();
        products3.Add(product5);
        products3.Add(product6);
        Order order3 = new Order(products3, customer3);

        List<Product> products4 = new List<Product>();
        products4.Add(product1);
        products4.Add(product3);
        Order order4 = new Order(products4, customer4);

        List<Order> orders = new List<Order>();
        orders.Add(order1);
        orders.Add(order2);
        orders.Add(order3);
        orders.Add(order4);

        foreach (Order order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine("Total Price: $" + order.GetTotalPrice());
            Console.WriteLine("------------");
        }
    }
}
