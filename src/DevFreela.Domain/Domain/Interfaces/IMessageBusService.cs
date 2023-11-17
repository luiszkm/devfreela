

namespace DevFreela.Domain.Domain.Interfaces;
public interface IMessageBusService
{
    void PublishMessage(string queue, byte[] message);
}
