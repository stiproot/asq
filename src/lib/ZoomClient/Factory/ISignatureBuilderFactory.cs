using System;
using Microsoft.Extensions.Configuration;
using ZoomClient.Builder;

namespace ZoomClient.Factory
{
    public interface ISignatureBuilderFactory
    {
        ISignatureBuilder Create();
    }
}