# Orm
A simple ORM, just for fun.

# Usage

```csharp
var db = Database.Connect("YourConnectionString");
var results = db.Query<Person>("SELECT Name, Age, Address FROM Persons");
```
or
```csharp
var db = Database.Connect("YourConnectionString");
var results = await db.QueryAsync<Person>("SELECT Name, Age, Address FROM Persons");
```
