namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

public class OrderedSpecificationBuilder<T> : IOrderedSpecificationBuilder<T>
{
    public BaseSpecification<T> Specification { get; }

    public OrderedSpecificationBuilder(BaseSpecification<T> specification)
    {
        Specification = specification;
    }
}