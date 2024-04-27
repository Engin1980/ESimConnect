using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimConnect.Definitions
{
    public enum SimType
    {
        UNSPECIFIED,
        INVALID,
        INT32,
        INT64,
        FLOAT32,
        FLOAT64,
        STRING8,
        STRING32,
        STRING64,
        STRING128,
        STRING256,
        STRING260,
        STRINGV,
        INITPOSITION,
        MARKERSTATE,
        WAYPOINT,
        LATLONALT,
        XYZ,
        MAX
    }
}
