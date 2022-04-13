using Prova_5_JP.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace Prova_5_JP.ConsoleApp.Módulo_Tarefa
{
    public class RepositorioTarefa : RepositorioBase<Tarefa>, IRepositorio<Tarefa>
    {
        #region Métodos públicos

        public List<Tarefa> SelecionarTarefasPendentes()
        {
            registros.Sort();
            return registros.FindAll(x => x.StatusTarefa == Status.pendente);
        }
        
        public List<Tarefa> SelecionarTarefasConcluidas()
        {
            registros.Sort();
            return registros.FindAll(x => x.StatusTarefa == Status.concluido);
        }

        #endregion
    }
}
