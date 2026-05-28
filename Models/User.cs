namespace JsonExample.Models;

public class User
{
    // Auto-property: compiler generates a private backing field automatically.
    // Equivalent to PHP's private $name + getName()/setName() methods.
    public string Name { get; set; } = string.Empty;
    public int    Age  { get; set; }
    public string City { get; set; } = string.Empty;

    // Virtual method so derived classes can override with their own details.
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"  Name : {Name}");
        Console.WriteLine($"  Age  : {Age}");
        Console.WriteLine($"  City : {City}");
    }
}

// Specialised user type: Admin.
// Adds AdminLevel and Department on top of the base User fields.
public class Admin : User
{

    // Discriminator field used when deserialising user_types.json.
    public string UserType    { get; set; } = "Admin";

    // 1 - junior admin, 2 - senior/chief admin.
    public int    AdminLevel  { get; set; }
    public string Department  { get; set; } = string.Empty;

    // Overrides base DisplayInfo to also print admin-specific data.

    public override void DisplayInfo()
    {
        base.DisplayInfo();                          // reuse parent output
        Console.WriteLine($"  Role       : Administrator (Level {AdminLevel})");
        Console.WriteLine($"  Department : {Department}");
    }
}

/// Specialised user type: Regular User.

public class RegularUser : User
{
    public string UserType     { get; set; } = "RegularUser";

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"  Role         : Regular User");

    }
}
