namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
  /// <summary>
  /// Asset Model
  /// </summary>
  public class Asset
  {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public string Id
    {
      get 
      {
        return Name.ToLower().Replace(" ", "-").Replace(".", "-");
      }
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the symbol.
    /// </summary>
    /// <value>
    /// The symbol.
    /// </value>
    public string Symbol { get; set; }
  }
}
