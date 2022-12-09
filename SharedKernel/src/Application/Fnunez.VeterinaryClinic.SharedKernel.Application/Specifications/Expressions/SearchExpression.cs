using System.Linq.Expressions;

namespace Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Expressions;

public class SearchExpression<T>
{
    /// <summary>
    /// The property to apply the SQL LIKE against.
    /// </summary>
    public Expression<Func<T, string>> Selector { get; }

    /// <summary>
    /// The value to use for the SQL LIKE.
    /// </summary>
    public string SearchTerm { get; }

    /// <summary>
    /// The index used to group sets of Selectors and SearchTerms together.
    /// </summary>
    public int SearchGroup { get; }

    /// <summary>
    /// Creates instance of <see cref="SearchExpression{T}" />.
    /// </summary>
    /// <param name="selector">The property to apply the SQL LIKE against.</param>
    /// <param name="searchTerm">The value to use for the SQL LIKE.</param>
    /// <param name="searchGroup">The index used to group sets of Selectors and SearchTerms together.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="selector"/> is null.</exception>
    /// <exception cref="ArgumentException">If <paramref name="searchTerm"/> is null or empty.</exception>
    public SearchExpression(Expression<Func<T, string>> selector, string searchTerm, int searchGroup = 1)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        if (string.IsNullOrEmpty(searchTerm))
            throw new ArgumentException(nameof(searchTerm));

        Selector = selector;
        SearchTerm = searchTerm;
        SearchGroup = searchGroup;
    }
}