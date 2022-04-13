using Prova_5_JP.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prova_5_JP.ConsoleApp.Módulo_Contatos
{
    public class TelaGerenciamentoContato : TelaBase, ITelaCadastravel
    {
        #region Atributos

        private readonly Notificador notificador;
        private readonly RepositorioContato repositorioContato;

        #endregion

        #region Construtor

        public TelaGerenciamentoContato(RepositorioContato repositorioContato, Notificador notificador)
            : base("Gerenciamento de Contatos")
        {
            this.repositorioContato = repositorioContato;
            this.notificador = notificador;
        }

        #endregion

        #region Métodos públicos

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("\nDigite 1 para Inserir");
            Console.WriteLine("\nDigite 2 para Editar");
            Console.WriteLine("\nDigite 3 para Excluir");
            Console.WriteLine("\nDigite 4 para Visualizar");
            Console.WriteLine("\nDigite 5 para Visualizar agrupados por cargo");
            Console.WriteLine("\nDigite s para Sair");
            Console.Write("\nSua escolha: ");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo Contato");

            Contato novoContato = ObterContato();

            string statusValidacao = repositorioContato.Inserir(novoContato);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Contato");

            bool temContatoscadastrados = VisualizarRegistros("Pesquisando");

            if (temContatoscadastrados == false)
                return;

            int idContato = ObterIdContato();

            Contato contatoAtualizado = ObterContato();

            bool conseguiuEditar = repositorioContato.Editar(x => x.id == idContato, contatoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato editada com sucesso", TipoMensagem.Sucesso);
        }

        public int ObterIdContato()
        {
            int idContato;
            bool idContatoEncontrado;

            do
            {
                Console.Write("Digite o ID do contato que deseja selecionar: ");
                idContato = Convert.ToInt32(Console.ReadLine());

                idContatoEncontrado = repositorioContato.ExisteRegistro(x => x.id == idContato);

                if (idContatoEncontrado == false)
                    notificador.ApresentarMensagem("Id de contato não encontrado, digite novomente", TipoMensagem.Atencao);

            } while (idContatoEncontrado == false);
            return idContato;
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatoscadastrados = VisualizarRegistros("Pesquisando");

            if (temContatoscadastrados == false)
                return;

            int idContato = ObterIdContato();

            repositorioContato.Excluir(x => x.id == idContato);

            notificador.ApresentarMensagem("Contato excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Contatos");

            List<Contato> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        public void VisualizarRegistrosAgrupadosPorCargo()
        {
            MostrarTitulo("Visualização de Contatos agrupados por cargo");

            List<Contato> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponível.", TipoMensagem.Atencao);
                return;
            }

            List<string> cargosExistentes = ObterCargos(contatos);

            foreach (string cargo in cargosExistentes)
            {
                Console.WriteLine("Agrupando pelo cargo: " + cargo);

                foreach (Contato contato in contatos)
                {
                    if (contato.Cargo == cargo)
                        Console.WriteLine(contato);

                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }
        
        public Contato ObterContato()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone no formato => DDD99999999: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite a empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, email, telefone, empresa, cargo);
        }

        #endregion

        #region Métodos privados

        private List<string> ObterCargos(List<Contato> contatos)
        {
            List<string> cargosCadastrados = new List<string>();

            foreach (Contato contato in contatos)
            {
                cargosCadastrados.Add(contato.Cargo);
            }

            return cargosCadastrados.Distinct().ToList();
        }

        #endregion
    }
}
