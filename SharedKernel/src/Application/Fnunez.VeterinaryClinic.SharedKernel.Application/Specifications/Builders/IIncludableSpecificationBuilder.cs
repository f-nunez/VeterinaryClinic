namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

public interface IIncludableSpecificationBuilder<T, out TProperty>
    : ISpecificationBuilder<T> where T : class
{
}