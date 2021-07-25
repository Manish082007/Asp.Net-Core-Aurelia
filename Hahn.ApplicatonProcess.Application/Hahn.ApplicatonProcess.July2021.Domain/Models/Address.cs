using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
  /// <summary>
  /// Address Model
  /// </summary>
  public class Address
  {
    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    /// <value>
    /// The postal code.
    /// </value>
    public string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the house number.
    /// </summary>
    /// <value>
    /// The house number.
    /// </value>
    public string HouseNumber { get; set; }

    /// <summary>
    /// Gets or sets the street number.
    /// </summary>
    /// <value>
    /// The street number.
    /// </value>
    public string StreetNumber { get; set; }
  }
}