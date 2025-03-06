using ESystem.Asserting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ESimConnect.Extenders
{
  public abstract class AbstractExtender
  {
    protected readonly ESimConnect eSimCon;

    public AbstractExtender(ESimConnect eSimCon)
    {
      EAssert.Argument.IsNotNull(eSimCon, nameof(eSimCon));
      this.eSimCon = eSimCon;
    }
  }
}
