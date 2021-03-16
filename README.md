# Orm
A simple ORM, just for fun.

# Usage
### Entity
```csharp
[Table("PersonTable")]
public class Person
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int Age { get; set; }
  public string Address { get; set; }
}
```
### Running queries
You can run queries by creating a Database object.
This database object will take care of opening and closing connections, running your queries etc.
It is intended to be shared across the application.
For example:
```csharp
var db = new Database(new SqlConnection("YourConnectionString"));
//Run your queries here.
```
### Query
```csharp
var results = db.Query<Person>();
```
### QueryAsync
```csharp
var results = await db.QueryAsync<Person>();
```
### Filtering
Use expressions to add additional conditions (WHERE, ORDER BY, ...)
```csharp
var results = db.Query<Person>(b => b.Where(p => p.Id > 1 && p.Age == 10).OrderBy(a => a.Id));
```
which will result in the following SQL Statement under the hood:
```sql
SELECT Name, Age, Address FROM PersonTable WHERE Id > 1 AND Age = 10 ORDER BY Id ASC
```

### Raw SQL
You dont wanna use expressions or execute a complex query?
Just use query with a raw SQL statement.
```csharp
var results = db.Query<Person>("SELECT Name, Age, Address FROM PersonTable WHERE Id > 1");
```

### Insert
```csharp
var person = new Person { Name = "Alex", Age = 32, Address = "Address" };
int rowsAffected = db.Insert(person);
```

### Update
```csharp
var person = db.Query<Person>(q => q.Where(p => p.Id == 4)).FirstOrDefault();
person.Age = 44;
int rowsAffected = db.Update(person, q => q.Where(p => p.Id == 4));
```

### Immutability
Immutable classes are also supported.
```csharp
[Table("PersonTable")]
public class Person
{
  public int Id { get; set; }
  public string Name { get; }
  public int Age { get; }
  public string Address { get; }
  
  [DatabaseConstructor]
  public Person(int id, string name, int age, string address)
  {
    Id = id,
    Name = name;
    Age = age;
    Address = address;
  }
}

var results = db.Query<Person>();
```
