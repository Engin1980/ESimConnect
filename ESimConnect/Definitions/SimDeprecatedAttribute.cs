using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Definitions
{
    /// <summary>
    /// Used to defined sim-definitions (events/simvars) that are deprecated in FS2020.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class SimDeprecatedAttribute : Attribute
    {
    }
}
