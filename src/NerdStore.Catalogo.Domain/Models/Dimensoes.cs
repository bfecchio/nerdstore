using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Models
{
    public class Dimensoes
    {
        #region Public Properties

        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        #endregion

        #region Constructors

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorQue(altura, 1, "O campo Altura não pode ser menor ou igual a zero.");
            Validacoes.ValidarSeMenorQue(largura, 1, "O campo Largura não pode ser menor ou igual a zero.");
            Validacoes.ValidarSeMenorQue(profundidade, 1, "O campo Profundidade não pode ser menor ou igual a zero.");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return DescricaoFormatada();
        }

        #endregion

        #region Public Methods

        public string DescricaoFormatada()
        {
            return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
        }

        #endregion
    }
}
