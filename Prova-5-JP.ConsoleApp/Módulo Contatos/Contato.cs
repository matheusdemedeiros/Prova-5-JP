using Prova_5_JP.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace Prova_5_JP.ConsoleApp.Módulo_Contatos
{
    public class Contato : EntidadeBase
    {
        #region Atributos

        private readonly string nome;
        private readonly string email;
        private readonly string telefone;
        private readonly string empresa;
        private readonly string cargo;

        #endregion

        #region Propriedades

        public string Cargo => cargo;

        #endregion
        
        #region Construtor

        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        #endregion

        #region Métodos públicos

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(nome))
                erros.Add("\nÉ necessário inserir um nome válido para os contatos!");

            if (!ValidarEmail())
                erros.Add("\nÉ necessário inserir um email válido para os contatos (mínimo de 6 caracteres e contendo @ e . )!");

            if (!ValidarTelefone())
                erros.Add("\nÉ necessário inserir um telefone válido para os contatos (mínimo 10 números com o DDD)!");

            if (string.IsNullOrEmpty(empresa))
                erros.Add("\nÉ necessário inserir uma empresa válida para os contatos!");

            if (string.IsNullOrEmpty(cargo))
                erros.Add("\nÉ necessário inserir um cargo válido para os contatos!");

            return new ResultadoValidacao(erros);
        }

        public override string ToString()
        {
            string retorno =
          "ID: " + id +
          "\nNome: " + nome +
          "\nEmail:" + email +
          "\nTelefone: " + telefone +
          "\nEmpresa: " + empresa +
          "\nCargo: " + cargo + "\n";

            return retorno;
        }

        #endregion

        #region Métodos privados

        private bool ValidarTelefone()
        {
            if (!string.IsNullOrEmpty(telefone))
                if (telefone.Length >= 10)
                    if (long.TryParse(telefone, out long res))
                        return true;
            return false;
        }

        private bool ValidarEmail()
        {
            if (!string.IsNullOrEmpty(email))
                if (email.Length >= 6)
                    if (email.Contains("@") && email.Contains("."))
                        return true;
            return false;
        }

        #endregion
    }
}
