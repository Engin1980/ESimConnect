using ELogging;

namespace ESimConnect
{
  public partial class ESimConnect
  {
    public abstract class BaseHandler
    {
      protected readonly ESimConnect parent;
      protected readonly Logger logger;

      protected BaseHandler(ESimConnect parent)
      {
        this.parent = parent;
        this.logger = Logger.Create($"{nameof(ESimConnect)}+{this.GetType().Name[..^7]}");
      }
    }
  }
}
