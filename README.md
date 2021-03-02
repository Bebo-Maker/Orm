# Orm
A simple ORM, just for fun.

# Usage

```csharp
[Table("PersonTable")]
public class Person
{
  public string Name { get; set; }
  public int Age { get; set; }
  public string Address { get; set; }
}

var db = new Database("YourConnectionString");
var results = db.Query<Person>("SELECT Name, Age, Address FROM PersonTable");
```
or
```csharp
var db = new Database("YourConnectionString");
var results = await db.QueryAsync<Person>("SELECT Name, Age, Address FROM PersonTable");
```

Immutable classes are also supported.
```csharp
[Table("PersonTable")]
public class Person
{
  public string Name { get; }
  public int Age { get; }
  public string Address { get; }
  
  [Constructor]
  public Person(string name, int age, string address)
  {
    Name = name;
    Age = age;
    Address = address;
  }
}

var db = new Database("YourConnectionString");
var results = db.Query<Person>("SELECT Name, Age, Address FROM PersonTable");
```
