namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

public interface ISpecificationBuilder<T, TResult> : ISpecificationBuilder<T>
{
    new BaseSpecification<T, TResult> Specification { get; }
}

public interface ISpecificationBuilder<T>
{
    BaseSpecification<T> Specification { get; }
}