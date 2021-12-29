using AutoMapper;
using F1Predictions.Core.AutoMapper;

namespace F1Predictions.Tests.Base;

public abstract class MapperTests
{
    protected readonly IMapper Mapper;

    protected MapperTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ConfigProfile>();
        });

        Mapper = mapperConfig.CreateMapper();
    }
}