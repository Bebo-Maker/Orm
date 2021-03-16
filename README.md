# Otis
A simple ORM, just for fun.

# Usage
### Entity
There are two approaches for mapping your Entities.
#### Attribute based approach
```csharp
using Orm.Attributes;

[Table("PersonTable")]
public class Person
{
  [PrimaryKey] // Use this property as the primary key of the table.
  public int Id { get; set; }
  
  public string Name { get; set; }
  
  public int Age { get; set; }
 
  [Column("AddressAlias")] // Defines a alias for the property.
  public string Address { get; set; }
  
  [Ignore] // This property will be ignored.
  public bool IsAlive { get; set; }
}
```
#### Configuration based approach
```csharp
public class Person
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int Age { get; set; }
  public string Address { get; set; }
  public bool IsAlive { get; set; }
}

// ...

using Orm.Configuration;

public class PersonMapping : EntityMap<Person> 
{
  public PersonMapping() 
  {
    Alias("PersonTable") // Set an alias for the table
    
    Map(p => p.Id).AsPrimaryKey(); // Use this property as the primary key.
    Map(p => p.Name);
    Map(p => p.Age).WithAlias("AddressAlias"); // Defines a alias for this property.
    Map(p => p.Name);
  }
}
```
Pass your mappings in the database constructor
```csharp
var maps = new EntityMap[] { new PersonMapping() };
var db = new Database(new SqlConnection("YourConnectionString"), maps);
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
### Select
```csharp
var results = db.Select<Person>();
```
### SelectAsync
```csharp
var results = await db.SelectAsync<Person>();
```
### Filtering
Use expressions to add additional conditions (WHERE, ORDER BY, ...)
```csharp
var results = db.Select<Person>(b => b.Where(p => p.Id > 1 && p.Age == 10).OrderBy(a => a.Id));
```
which will result in the following SQL Statement under the hood:
```sql
SELECT Name, Age, Address FROM PersonTable WHERE Id > 1 AND Age = 10 ORDER BY Id ASC
```

### Raw SQL
You dont wanna use expressions or execute a complex query?
Just use query with a raw SQL statement.
```csharp
var results = db.ExecuteReader<Person>("SELECT Name, Age, Address FROM PersonTable WHERE Id > 1");
```

### Insert
```csharp
var person = new Person { Name = "Alex", Age = 32, Address = "Address" };
int rowsAffected = db.Insert(person);
```

### Update
```csharp
var person = db.Select<Person>(q => q.Where(p => p.Id == 4)).FirstOrDefault();
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

var results = db.Select<Person>();
```
### Available methods
```csharp

List<T> ExecuteReader<T>(string sql);
Task<List<T>> ExecuteReaderAsync<T>(string sql);

List<T> Select<T>(Action<IQueryBuilder<T>> action = null);
Task<List<T>> SelectAsync<T>(Action<IQueryBuilder<T>> action = null);

List<T> SelectDistinct<T>(Action<IQueryBuilder<T>> action = null);
Task<List<T>> SelectDistinctAsync<T>(Action<IQueryBuilder<T>> action = null);

int Insert<T>(T entity, Action<IQueryBuilder<T>> action = null);
Task<int> InsertAsync<T>(T entity, Action<IQueryBuilder<T>> action = null);

int Update<T>(T entity, Action<IQueryBuilder<T>> action = null);
Task<int> UpdateAsync<T>(T entity, Action<IQueryBuilder<T>> action = null);

int Delete<T>(Action<IQueryBuilder<T>> action = null);
Task<int> DeleteAsync<T>(Action<IQueryBuilder<T>> action = null);

```
