using ESimConnect.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Xps.Serialization;

namespace ESimConnect.Extenders
{
  public class VerticalSpeedExtender : AbstractExtender, IDisposable
  {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct DataStruct
    {
      [DataDefinition(SimVars.Aircraft.Miscelaneous.PLANE_ALTITUDE, SimUnits.Length.FOOT)]
      public double altitude;

      [DataDefinition(SimVars.Miscellaneous.GROUND_ALTITUDE, SimUnits.Length.FOOT)]
      public double groundAltitude;

      [DataDefinition(SimVars.Miscellaneous.SIM_ON_GROUND)]
      public double simOnGround;
    }

    private class DataBuffer
    {
      private readonly double[] data;
      private int head;
      private int count;

      public DataBuffer(int size)
      {
        data = new double[size];
        Clear();
      }

      public void Clear()
      {
        head = 0;
        count = 0;
      }

      public void Add(double item)
      {
        data[head] = item;
        head = (head + 1) % data.Length;
        if (count < data.Length)
          count++;
      }

      public IEnumerable<double> GetData()
      {
        for (int i = 0; i < count; i++)
        {
          int index = (head + i) % data.Length;
          yield return data[index];
        }
      }
    }

    public class Options
    {
      public int BufferSize { get; set; } = 20;
    }

    private bool isRegistered;
    private int simOnGroundFlag = 0;
    private readonly TypeId dataTypeId;
    private readonly RequestId dataRequestId;
    private readonly DataBuffer groundAltitude;
    private readonly DataBuffer planeAltitude;
    private readonly Options options;
    private int? touchDownEvalDataIndexFlag = null;
    private int dataIndexFlag = 0;
    public event Action<VerticalSpeedExtender>? OnTouchdownDetected;
    public event Action<VerticalSpeedExtender, double>? OnTouchdownEvaluated;
    private int framesInCurrentSecond = 0;
    private int lastFramesPerSecond = 50;
    private readonly SimTimeExtender ste;

    public VerticalSpeedExtender(ESimConnect eSimConnect, Action<Options>? opts = null) : base(eSimConnect)
    {
      this.options = new();
      opts?.Invoke(this.options);

      this.groundAltitude = new(options.BufferSize);
      this.planeAltitude = new(options.BufferSize);

      ste = new(eSimConnect, true);
      ste.SimSecondElapsed += OnSimSecondElapsed;

      lock (this)
      {
        (this.dataTypeId, this.dataRequestId) = RegisterStruct();
        eSimCon.DataReceived += OnDataReceived;
        this.isRegistered = true;
      }
    }

    private void OnSimSecondElapsed()
    {
      Console.WriteLine("Tick length " + framesInCurrentSecond);
      lastFramesPerSecond = framesInCurrentSecond;
      framesInCurrentSecond = 0;
    }

    private void OnDataReceived(ESimConnect sender, ESimConnect.ESimConnectDataReceivedEventArgs e)
    {
      if (e.RequestId != this.dataRequestId) return;

      DataStruct data = (DataStruct)e.Data;
      framesInCurrentSecond++;

      dataIndexFlag = (dataIndexFlag + 1) % options.BufferSize;
      groundAltitude.Add(data.groundAltitude);
      planeAltitude.Add(data.altitude);
      if (simOnGroundFlag != data.simOnGround)
      {
        this.simOnGroundFlag = (int)data.simOnGround;
        if (this.simOnGroundFlag == 1) // is on ground
        {
          touchDownEvalDataIndexFlag = (dataIndexFlag + options.BufferSize / 2) % options.BufferSize;
          this.OnTouchdownDetected?.Invoke(this);
        }
      }
      if (dataIndexFlag == touchDownEvalDataIndexFlag)
      {
        double[] g = this.groundAltitude.GetData().ToArray();
        double[] p = this.planeAltitude.GetData().ToArray();

        double gvs = ConvertDataToVerticalSpeed(g);
        double pvs = ConvertDataToVerticalSpeed(p);
        double vs = pvs - gvs;
        this.OnTouchdownEvaluated?.Invoke(this, vs);

        touchDownEvalDataIndexFlag = null;
      }
    }

    private (TypeId, RequestId) RegisterStruct()
    {
      var typeId = base.eSimCon.Structs.Register<DataStruct>();
      var requestId = base.eSimCon.Structs.RequestRepeatedly(typeId, SimConnectPeriod.SIM_FRAME, false);

      return (typeId, requestId);
    }

    private void UnregisterStruct()
    {
      lock (this)
      {
        if (isRegistered)
        {
          base.eSimCon.Structs.RequestRepeatedly(this.dataRequestId, this.dataTypeId, SimConnectPeriod.NEVER);
          base.eSimCon.Structs.Unregister(this.dataTypeId);
          isRegistered = false;
        }
      }
    }

    public double GetGroundVerticalSpeed()
    {
      var data = groundAltitude.GetData();
      var vs = ConvertDataToVerticalSpeed(data.ToArray());
      return vs;
    }

    private double ConvertDataToVerticalSpeed(double[] data)
    {
      if (data.Length < 2) return double.NaN;

      double[] tmp = new double[data.Length - 1];
      double ret;

      for (int i = 0; i < data.Length - 1; i++)
      {
        tmp[i] = data[i + 1] - data[i];
        tmp[i] = tmp[i] * lastFramesPerSecond * 60; // convert to feet/min
      }
      ret = tmp.Average();
      return ret;
    }

    public double GetPlaneVerticalSpeed()
    {
      var data = planeAltitude.GetData().ToArray();
      var vs = ConvertDataToVerticalSpeed(data);
      return vs;
    }

    public void Dispose()
    {
      UnregisterStruct();
    }
  }
}
