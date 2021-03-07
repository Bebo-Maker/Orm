# Orm
A simple ORM, just for fun.

# Usage

#### Entity
```csharp
[Table("PersonTable")]
public class Person
{
  public string Name { get; set; }
  public int Age { get; set; }
  public string Address { get; set; }
}
```
#### Query
```csharp
var db = new Database("YourConnectionString");
var results = db.Query<Person>();
```
#### QueryAsync
```csharp
var db = new Database("YourConnectionString");
var results = await db.QueryAsync<Person>();
```
#### Filtering
Use expressions to add additional conditions (WHERE, ORDER BY, ...)
```csharp
var results = _db.Query<Person>(b => b.Where(p => p.Id > 1 && p.Age == 10).OrderBy(a => a.Id));
```
which will result in the following SQL Statement under the hood:
```sql
SELECT Name, Age, Address FROM PersonTable WHERE Id > 1 AND Age = 10 ORDER BY Id ASC
```

#### Raw SQL
You dont wanna use expressions or execute a complex query?
Just use query with a raw SQL statement.
```csharp
var results = _db.Query<Person>("SELECT Name, Age, Address FROM PersonTable WHERE Id > 1");
```

#### Immutability
Immutable classes are also supported.
```csharp
[Table("PersonTable")]
public class Person
{
  public string Name { get; }
  public int Age { get; }
  public string Address { get; }
  
  [DatabaseConstructor]
  public Person(string name, int age, string address)
  {
    Name = name;
    Age = age;
    Address = address;
  }
}

var db = new Database("YourConnectionString");
var results = db.Query<Person>();
```
