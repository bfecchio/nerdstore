using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Categoria : Entity
    {
        #region Public Properties

        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        #endregion

        #region Constructors

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }

        #endregion

        #region Public Methods

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome da Categoria não pode estar vazio.");
            Validacoes.ValidarSeIgual(Codigo, 0, "O campo Código da Categoria não pode ser zero.");
        }

        #endregion
    }
}
