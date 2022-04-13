using System;
using System.Collections.Generic;
using Prova_5_JP.ConsoleApp.Compartilhado;

namespace Prova_5_JP.ConsoleApp.Módulo_Tarefa
{
    public class TelaGerenciamentoTarefa : TelaBase
    {
        #region Atributos

        private readonly Notificador notificador;
        private readonly RepositorioTarefa repositorioTarefa;

        #endregion

        public TelaGerenciamentoTarefa(RepositorioTarefa repositorioTarefa, Notificador notificador) : base("Gerenciamento de Tarefas")
        {
            this.repositorioTarefa = repositorioTarefa;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("\nDigite 1 para Inserir tarefa");
            Console.WriteLine("\nDigite 2 para Editar tarefa");
            Console.WriteLine("\nDigite 3 para Excluir tarefa");
            Console.WriteLine("\nDigite 4 para Visualizar Tarefas pendentes");
            Console.WriteLine("\nDigite 5 para Visualizar Tarefas concluídas");
            Console.WriteLine("\nDigite 6 para Adicionar um item em uma tarefa");
            Console.WriteLine("\nDigite 7 para Concluir um item em uma tarefa");
            Console.WriteLine("\nDigite 8 para Visualizar itens pendentes em uma tarefa");
            Console.WriteLine("\nDigite 9 para Visualizar itens concluídos em uma tarefa");
            Console.WriteLine("\nDigite s para Sair");
            Console.Write("\nSua escolha: ");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region Métodos das tarefas

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova Tarefa");

            Tarefa novaTarefa = ObterTarefa("inserção");

            string statusValidacao = repositorioTarefa.Inserir(novaTarefa);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Tarefa cadastrada com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
                return;

            int idTarefa = ObterIdTarefa();

            DateTime dataCriacao = repositorioTarefa.SelecionarRegistro(idTarefa).dataCriacao;

            Tarefa tarefaAtualizada = ObterTarefa("edição");

            tarefaAtualizada.dataCriacao = dataCriacao;

            bool conseguiuEditar = repositorioTarefa.Editar(x => x.id == idTarefa, tarefaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
                return;

            int idTarefa = ObterIdTarefa();

            if (repositorioTarefa.SelecionarRegistro(idTarefa).StatusTarefa == Status.concluido)
            {
                repositorioTarefa.Excluir(x => x.id == idTarefa);
                notificador.ApresentarMensagem("Tarefa excluída com sucesso!!", TipoMensagem.Sucesso);
            }
            else
                notificador.ApresentarMensagem("Tarefa não excluída pois está pendente", TipoMensagem.Atencao);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarTarefasPendentes(string tipo)
        {
            if (tipo == "tela")
                MostrarTitulo("Visualização de Tarefas Pendentes");

            List<Tarefa> tarefasPendentes = repositorioTarefa.SelecionarTarefasPendentes();

            if (tarefasPendentes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa pendente.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefasPendentes)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarTarefasConcluidas(string tipo)
        {
            if (tipo == "tela")
                MostrarTitulo("Visualização de Tarefas concluídas!!");

            List<Tarefa> tarefasConcluidas = repositorioTarefa.SelecionarTarefasConcluidas();

            if (tarefasConcluidas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa concluída.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefasConcluidas)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        #endregion

        #region Métodos de Item

        public void AdicionarItemNaTarefa()
        {
            MostrarTitulo("Adicionando item na tarefa");

            Console.WriteLine("Estas são as tarefas pendentes!!\n");

            bool temTarefasPendentes = VisualizarTarefasPendentes("Pesquisando");

            if (temTarefasPendentes == false)
                return;

            Tarefa tarefaSelecionada = ObtemTarefa();

            if (tarefaSelecionada == null)
                return;

            Item item = ObterItem();

            ResultadoValidacao validacao = item.Validar();

            if (validacao.Status != StatusValidacao.Ok)
            {
                notificador.ApresentarMensagem("É necessário adicionar uma descrição para o item!", TipoMensagem.Erro);
                return;
            }

            tarefaSelecionada.AdicionarItem(item);

            bool conseguiuAdicionar = repositorioTarefa.Editar(tarefaSelecionada.id, tarefaSelecionada);

            if (!conseguiuAdicionar)
                notificador.ApresentarMensagem("Não foi possível adicionar item.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Item adicionado com sucesso", TipoMensagem.Sucesso);
        }

        public void ConcluirItemDaTarefa()
        {
            MostrarTitulo("Concluindo um item da tarefa");

            Console.WriteLine("Estas são as tarefas\n ");

            bool temTarefasPendentes = VisualizarTarefasPendentes("Pesquisando");

            if (temTarefasPendentes == false)
                return;

            Tarefa tarefaSelecionada = ObtemTarefa();

            if (tarefaSelecionada == null)
                return;

            if (tarefaSelecionada.QuantidadeDeItensPendentes > 0)
            {
                tarefaSelecionada.MostrarItensPendentes();

                Console.Write("Informe o ID do item que deseja concluir: ");
                int idSelecionado = int.Parse(Console.ReadLine());

                Item itemSelecionado = tarefaSelecionada.RetornarItemDaListaDePendentes(idSelecionado);

                itemSelecionado.Concluir();

                tarefaSelecionada.AtualizarTarefa();

                bool conseguiuEditar = repositorioTarefa.Editar(tarefaSelecionada.id, tarefaSelecionada);

                if (conseguiuEditar)
                    notificador.ApresentarMensagem("Item concluído com sucesso!", TipoMensagem.Sucesso);
                else
                    notificador.ApresentarMensagem("Não foi possível concluir o item!", TipoMensagem.Erro);
            }
            else
                notificador.ApresentarMensagem("Não há itens pendentes na tarefa!", TipoMensagem.Atencao);
        }

        public void VisualizarItensPendentesDaTarefa(string tipo)
        {
            if (tipo == "tela")
                MostrarTitulo("Visualizando itens pendentes da tarefa");

            Console.WriteLine("Estas são as tarefas pendentes!!\n");

            bool temTarefasPendentes = VisualizarTarefasPendentes("Pesquisando");

            if (temTarefasPendentes == false)
                return;

            Tarefa tarefaSelecionada = ObtemTarefa();

            if (tarefaSelecionada == null)
                return;

            if (tarefaSelecionada.QuantidadeDeItensPendentes > 0)
            {
                tarefaSelecionada.MostrarItensPendentes();
                Console.ReadLine();
            }
            else
                notificador.ApresentarMensagem("Não há itens pendentes na tarefa selecionada!", TipoMensagem.Atencao);
        }

        public void VisualizarItensConcluidosDaTarefa()
        {
            MostrarTitulo("Visualizando itens concluídos da tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
                return;

            Tarefa tarefaSelecionada = ObtemTarefa();

            if (tarefaSelecionada == null)
                return;

            if (tarefaSelecionada.QuantidadeDeItensConcluidos > 0)
            {
                tarefaSelecionada.MostrarItensConcluidos();
                Console.ReadLine();
            }
            else
                notificador.ApresentarMensagem("Não há itens concluídos na tarefa selecionada!", TipoMensagem.Atencao);

        }

        #endregion

        #region Métodos privados

        private int ObterIdTarefa()
        {
            int idTarefa;
            bool idTarefaEncontrado;

            do
            {
                Console.Write("Digite o ID da tarefa que deseja selecionar: ");
                idTarefa = Convert.ToInt32(Console.ReadLine());

                idTarefaEncontrado = repositorioTarefa.ExisteRegistro(x => x.id == idTarefa);

                if (idTarefaEncontrado == false)
                    notificador.ApresentarMensagem("ID de Tarefa não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (idTarefaEncontrado == false);
            return idTarefa;
        }

        private Tarefa ObterTarefa(string tipo)
        {
            Console.Write("Digite o título: ");
            string tituloTarefa = Console.ReadLine();

            Console.Write("Digite a Prioridade da tarefa (3 para ALTA, 2 para NORMAL ou 1 para BAIXA): ");
            bool prioridadeValida = int.TryParse(Console.ReadLine(), out int prioridade);

            if (prioridadeValida == false)
                prioridade = 0;

            Tarefa tarefa;

            string dataCriacao;

            if (tipo != "edição")
            {
                Console.Write("Digite a data de criação: ");

                dataCriacao = Console.ReadLine();

                tarefa = new Tarefa(tituloTarefa, dataCriacao, prioridade);
            }
            else
                tarefa = new Tarefa(tituloTarefa, prioridade);

            return tarefa;
        }

        private Tarefa ObtemTarefa()
        {
            int idSelecionado = ObterIdTarefa();

            if (idSelecionado == 0)
                return null;

            Tarefa tarefaSelecionada = repositorioTarefa.SelecionarRegistro(x => x.id == idSelecionado);

            return tarefaSelecionada;
        }

        private Item ObterItem()
        {
            Console.Write("Digite a descrição do item: ");
            string descricao = Console.ReadLine();

            Item item = new Item(descricao);

            return item;
        }

        #endregion

    }
}

