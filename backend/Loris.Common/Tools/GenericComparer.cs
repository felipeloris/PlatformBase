using System;
using System.Collections.Generic;
using System.Linq;

namespace Loris.Common.Tools
{
    class GenericComparer<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> MethodEquals { get; }

        public Func<T, int> MethodGetHashCode { get; }

        private GenericComparer(
            Func<T, T, bool> metodoEquals,
            Func<T, int> metodoGetHashCode)
        {
            this.MethodEquals = metodoEquals;
            this.MethodGetHashCode = metodoGetHashCode;
        }

        public static GenericComparer<T> Create(
            Func<T, T, bool> metodoEquals, Func<T, int> metodoGetHashCode)
            => new GenericComparer<T>(
                        metodoEquals,
                        metodoGetHashCode
                    );

        public bool Equals(T x, T y) => MethodEquals(x, y);

        public int GetHashCode(T obj) => MethodGetHashCode(obj);
    }

    public static class DistinctExtension
    {
        public static IEnumerable<TSource> Distinct<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource, bool> metodoEquals,
            Func<TSource, int> metodoGetHashCode)
            => source.Distinct(GenericComparer<TSource>.Create(
                metodoEquals,
                metodoGetHashCode));
    }

    /******************************************************************
     * https://gabrielschade.github.io/2017/09/10/linq-distinct.html
     * 
     * Exemplo de uso:
     ******************************************************************
    List<Produto> produtos = new List<Produto>();
    produtos.Add(new Produto() { Id = 1, Nome = "Caneta" });
    produtos.Add(new Produto() { Id = 2, Nome = "Lápis" });
    produtos.Add(new Produto() { Id = 3, Nome = "Computador" });
    produtos.Add(new Produto() { Id = 1, Nome = "Caneta" });
    produtos.Add(new Produto() { Id = 3, Nome = "Computador" });

    IEnumerable<Produto> produtosSemRepeticaoPorId = 
        produtos.Distinct(
            (produto1, produto2) => produto1.Id == produto2.Id,
            produto => produto.Id.GetHashCode()
        );

    IEnumerable<Produto> produtosSemRepeticaoPorNome = 
        produtos.Distinct(
            (produto1, produto2) => 
                produto1.Nome == produto2.Nome,
            produto => produto.Nome.GetHashCode()
        );
     */
}
