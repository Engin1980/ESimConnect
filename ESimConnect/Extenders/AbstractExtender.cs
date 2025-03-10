using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ESimConnect.Extenders
{
  /// <summary>
  /// Abstract class for extenders.
  /// </summary>
  public abstract class AbstractExtender
  {
    protected readonly ESimConnect eSimCon;

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="eSimConnect">ESimConnect instance, cannot be null.</param>
    public AbstractExtender(ESimConnect eSimConnect)
    {
      EAssert.Argument.IsNotNull(eSimConnect, nameof(eSimConnect));
      this.eSimCon = eSimConnect;
    }
  }
}
