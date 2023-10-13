namespace URG.SCIP.Streaming
{
    using Cysharp.Threading.Tasks;

    public interface ISCIPListener
    {
        UniTask Listen(TCPClientProvider client);
    }
}
