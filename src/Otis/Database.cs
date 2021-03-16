using Otis.Core;
using Otis.Factories;
using Otis.ObjectCreator;

namespace Otis
{
  public partial class Database : IDatabase
  {
    private readonly Engine _engine;
    private readonly IDatabaseProvider _provider;
    private readonly ITableFactory _factory;
    private readonly DatabaseCommandFactory _commandFactory;
    private readonly BuilderFactory _builderFactory;

    public Database(IDatabaseProvider provider, IDatabaseConfiguration configuration = null)
    {
      _provider = provider;
      _factory = new TableFactory(configuration);
      _commandFactory = new DatabaseCommandFactory(_factory, _provider);
      _engine = new Engine(new ActivatorObjectCreator(), _factory);
      _builderFactory = new BuilderFactory(_factory);
    }
  }
}
