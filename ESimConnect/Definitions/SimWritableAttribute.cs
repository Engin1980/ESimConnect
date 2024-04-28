using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Definitions
{
    /// <summary>
    /// Mark SimVar as writable in FS2020.
    /// </summary>
    /// <remarks>
    /// Writable SimVars may write a value into the sim.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SimWritableAttribute : Attribute
    {
    }
}
