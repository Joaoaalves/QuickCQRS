using Joaoaalves.FastCQRS.Domain.DDD;

namespace Joaoaalves.FastCQRS.Domain.Tests.Fakes
{
    public class FakeTypedId(Guid value) : TypedIdValueBase(value)
    {
    }
}