namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

public class IncludableSpecificationBuilder<T, TProperty>
    : IIncludableSpecificationBuilder<T, TProperty> where T : class
{
    public BaseSpecification<T> Specification { get; }

    public IncludableSpecificationBuilder(BaseSpecification<T> specification)
    {
        this.Specification = specification;
    }
}