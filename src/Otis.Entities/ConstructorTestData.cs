using Otis.Attributes;
using System;

namespace Otis.Entities
{
  [Table("TestTable")]
  public class ConstructorTestData
  {
    public int Id { get; }
    public DateTime Datetime { get; }
    public int Number { get; }
    public string Name { get; }
    public string LongText { get; }

    [DatabaseConstructor]
    public ConstructorTestData(int id, DateTime datetime, int number, string name, string longText)
    {
      Id = id;
      Datetime = datetime;
      Number = number;
      Name = name;
      LongText = longText;
    }
  }
}
