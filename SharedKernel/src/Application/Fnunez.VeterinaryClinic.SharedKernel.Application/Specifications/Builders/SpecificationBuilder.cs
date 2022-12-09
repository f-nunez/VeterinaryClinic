namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

public class SpecificationBuilder<T, TResult>
    : SpecificationBuilder<T>, ISpecificationBuilder<T, TResult>
{
    public new BaseSpecification<T, TResult> Specification { get; }

    public SpecificationBuilder(BaseSpecification<T, TResult> specification)
        : base(specification)
    {
        Specification = specification;
    }
}

public class SpecificationBuilder<T> : ISpecificationBuilder<T>
{
    public BaseSpecification<T> Specification { get; }

    public SpecificationBuilder(BaseSpecification<T> specification)
    {
        Specification = specification;
    }
}