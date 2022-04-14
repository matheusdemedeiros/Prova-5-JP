using Prova_5_JP.ConsoleApp.Compartilhado;
using Prova_5_JP.ConsoleApp.Módulo_Contatos;
using System;
using System.Collections.Generic;

namespace Prova_5_JP.ConsoleApp.Módulo_Compromissos
{
    public class TelaGerenciamentoCompromisso : TelaBase
    {
        #region Atributos

        private readonly Notificador notificador;
        private readonly RepositorioCompromisso repositorioCompromisso;
        private readonly RepositorioContato repositorioContato;
        private readonly TelaGerenciamentoContato telaGerenciamentoContato;

        #endregion

        #region Construtor

        public TelaGerenciamentoCompromisso(Notificador notificador,
            RepositorioCompromisso repositorioCompromisso,
            RepositorioContato repositorioContato,
            TelaGerenciamentoContato telaGerenciamentoContato) : base("Gerenciamento de compromissos")
        {
            this.notificador = notificador;
            this.repositorioCompromisso = repositorioCompromisso;
            this.repositorioContato = repositorioContato;
            this.telaGerenciamentoContato = telaGerenciamentoContato;
        }

        #endregion

        #region Métofos puúblicos


        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("\nDigite 1 para Inserir compromisso");
            Console.WriteLine("\nDigite 2 para Editar compromisso");
            Console.WriteLine("\nDigite 3 para Excluir compromisso");
            Console.WriteLine("\nDigite 4 para Visualizar compromissos passados");
            Console.WriteLine("\nDigite 5 para Visualizar compromissos futuros");
            Console.WriteLine("\nDigite 6 para Filtrar compromissos futuros por período");
            Console.WriteLine("\nDigite 7 para Visualizar Compromissos Semanais");
            Console.WriteLine("\nDigite 8 para Visualizar Compromissos Diários");
            Console.WriteLine("\nDigite s para Sair");
            Console.Write("\nSua escolha: ");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo Compromissso");

            Compromisso novoCompromisso = ObterCompromisso();

            string statusValidacao = repositorioCompromisso.Inserir(novoCompromisso);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Compromisso");

            bool temCompromissosCadastrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosCadastrados == false)
                return;

            int idCompromisso = ObterIdCompromisso();

            Compromisso compromissoAtualizado = ObterCompromisso();

            bool conseguiuEditar = repositorioCompromisso.Editar(x => x.id == idCompromisso, compromissoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Compromisso editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissosCadastrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosCadastrados == false)
                return;

            int idCompromisso = ObterIdCompromisso();

            if (repositorioCompromisso.Excluir(x => x.id == idCompromisso))
                notificador.ApresentarMensagem("Compromisso excluído com sucesso!!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Compromisso não excluído!", TipoMensagem.Atencao);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Compromissos");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosDiarios(string itpo)
        {
            if (itpo == "tela")
                MostrarTitulo("Visualização de compromissos diários");

            List<Compromisso> compromissosDiarios = repositorioCompromisso.SelecionarCompromissosDiarios();

            if (compromissosDiarios.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum compromisso na data de hoje.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissosDiarios)
                Console.WriteLine(compromisso);

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosSemanais(string tipo)
        {
            if (tipo == "tela")
                MostrarTitulo("Visualização de compromissos semanais");

            List<Compromisso> compromissosSemanais = repositorioCompromisso.SelecionarCompromissosSemanais();

            if (compromissosSemanais.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum compromisso esta semana!!", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissosSemanais)
                Console.WriteLine(compromisso);

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosPassados(string tipo)
        {
            if (tipo == "tela")
                MostrarTitulo("Visualização de compromissos passados");

            List<Compromisso> compromissosPassados = repositorioCompromisso.SelecionarCompromissosPassados();

            if (compromissosPassados.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum compromisso passado disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissosPassados)
                Console.WriteLine(compromisso);

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosFuturos(string tipo)
        {
            if (tipo == "tela")
                MostrarTitulo("Visualização de compromissos futuros");

            List<Compromisso> compromissosFuturos = repositorioCompromisso.SelecionarCompromissosFuturos();

            if (compromissosFuturos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum compromisso futuro disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissosFuturos)
                Console.WriteLine(compromisso);

            Console.ReadLine();
            return true;
        }

        public void VisualizarCompromissosPorPeriodo(string tipo)
        {
            if (tipo == "tela")
                MostrarTitulo("Visualização de compromissos FUTUROS por período");

            List<Compromisso> compromissosFuturos = repositorioCompromisso.SelecionarCompromissosFuturos();

            if (compromissosFuturos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há compromissos futuros!!", TipoMensagem.Atencao);
                return;
            }

            Console.Write("Informe a data inicial do perído: ");
            DateTime inicioPeriodo = DateTime.Parse(Console.ReadLine());

            Console.Write("\nInforme a data final do perído: ");
            DateTime fimPeriodo = DateTime.Parse(Console.ReadLine());

            int cont = 0;

            foreach (Compromisso item in compromissosFuturos)
            {
                if (item.DataInicio >= inicioPeriodo && item.DataInicio <= fimPeriodo)
                {
                    cont++;
                    Console.WriteLine(item);
                }
            }

            if (cont == 0)
                notificador.ApresentarMensagem("Não há compromissos neste período!!", TipoMensagem.Atencao);

            Console.ReadLine();
        }

        #endregion

        #region Métodos privados

        private int ObterIdCompromisso()
        {
            int idCompromisso;
            bool idCompromissoEncontrado;

            do
            {
                Console.Write("Digite o ID da compromisso que deseja selecionar: ");
                idCompromisso = Convert.ToInt32(Console.ReadLine());

                idCompromissoEncontrado = repositorioCompromisso.ExisteRegistro(x => x.id == idCompromisso);

                if (idCompromissoEncontrado == false)
                    notificador.ApresentarMensagem("ID de compromisso não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (idCompromissoEncontrado == false);
            return idCompromisso;
        }

        private Compromisso ObterCompromisso()
        {
            Console.Write("Digite o assunto: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite o local: ");
            string local = Console.ReadLine();

            Console.Write("Digite a data e o horário de início no formato => dd/MM/aaaa HH:mm :");
            string dataHoraInicio = Console.ReadLine();

            Console.Write("Digite o horário de término no formato => HH:mm :");
            string horarioFim = Console.ReadLine();

            Console.WriteLine("\nSelecione um contato da sua agenda!\n");

            Contato contato = ObterContato();

            if (contato == null)
                return null;

            Compromisso compromisso = new Compromisso(assunto, local, dataHoraInicio, horarioFim, contato);

            return compromisso;
        }

        private Contato ObterContato()
        {
            telaGerenciamentoContato.VisualizarRegistros("pesquisando");

            Console.Write("Digite o ID do contato que deseja selecionar: ");
            int idContato = int.Parse(Console.ReadLine());

            Contato contato = repositorioContato.SelecionarRegistro(idContato);

            if (contato == null)
                notificador.ApresentarMensagem("Id de contato não encontrado!", TipoMensagem.Atencao);

            return contato;
        }

        #endregion
    }
}
