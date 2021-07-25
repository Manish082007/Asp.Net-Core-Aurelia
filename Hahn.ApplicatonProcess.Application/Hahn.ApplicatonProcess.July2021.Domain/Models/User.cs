using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
  /// <summary>
  /// User Model
  /// </summary>
  public class User
  {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the age.
    /// </summary>
    /// <value>
    /// The age.
    /// </value>
    public int Age { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>
    /// The first name.
    /// </value>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>
    /// The last name.
    /// </value>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>
    /// The address.
    /// </value>
    public Address Address { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>
    /// The email.
    /// </value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the assets.
    /// </summary>
    /// <value>
    /// The assets.
    /// </value>
    public IEnumerable<Asset> Assets { get; set; }
  }
}