using System;
using System.Collections.Generic;
using Prova_5_JP.ConsoleApp.Compartilhado;

namespace Prova_5_JP.ConsoleApp.Módulo_Tarefa
{
    public class Tarefa : EntidadeBase, IComparable<Tarefa>
    {
        #region Atributos

        private static int contadorIdItem = 0;
        private readonly string titulo;
        private readonly int prioridade;
        private Status statusTarefa;
        private DateTime dataConclusao;
        public DateTime dataCriacao;
        public List<Item> itensPendentes = new List<Item>();
        public List<Item> itensConcluidos = new List<Item>();

        #endregion

        #region Propriedades

        public int QuantidadeDeItensTotais => itensPendentes.Count + itensConcluidos.Count;

        public string DataConclusao => StatusTarefa == Status.concluido ? dataConclusao.ToShortDateString() : "Indeterminado";

        public int QuantidadeDeItensPendentes => itensPendentes.Count;

        public int QuantidadeDeItensConcluidos => itensConcluidos.Count;

        public string PercentualConclusao
        {
            get
            {
                if (QuantidadeDeItensTotais > 0)
                    return ((100 * QuantidadeDeItensConcluidos) / QuantidadeDeItensTotais).ToString("N2") + " %";
                else
                    return "Indeterminado";
            }
        }

        public Status StatusTarefa { get => statusTarefa; }

        public string PrioridadeTarefa
        {
            get
            {
                string retorno = "";
                if (prioridade == 1)
                    retorno = "Baixa";
                else if (prioridade == 2)
                    retorno = "Normal";
                else if (prioridade == 3)
                    retorno = "Alta";

                return retorno;
            }
        }

        #endregion

        #region Construtores

        public Tarefa(string tituloTarefa, string dataCriacao, int prioridade)
        {
            this.titulo = tituloTarefa;
            this.dataCriacao = DateTime.TryParse(dataCriacao, out DateTime data) ? data : new DateTime(1, 1, 1);
            this.prioridade = prioridade;

        }

        public Tarefa(string tituloTarefa, int prioridade)
        {
            this.titulo = tituloTarefa;
            this.prioridade = prioridade;
        }

        #endregion

        #region Métodos públicos

        public Item RetornarItemDaListaDePendentes(int id)
        {
            return itensPendentes.Find(x => x.id == id);
        }

        public void AtualizarItensConcluidos()
        {

            itensConcluidos.AddRange(itensPendentes.FindAll(x => !x.EstaPendente));
            itensPendentes.RemoveAll(x => !x.EstaPendente);

            //foreach (Item item in itensPendentes)
            //{
            //    if (!item.EstaPendente)
            //    {
            //        itensConcluidos.Add(item);
            //        itensPendentes.Remove(item);
            //    }
            //}
        }

        public void AtualizarTarefa()
        {
            AtualizarItensConcluidos();

            if (PercentualConclusao == "100,00 %" && QuantidadeDeItensPendentes == 0 && QuantidadeDeItensConcluidos > 0)
                ConcluirTarefa();
        }

        public void AdicionarItem(Item item)
        {
            item.id = ++contadorIdItem;

            itensPendentes.Add(item);
        }

        public void MostrarItensPendentes()
        {
            foreach (Item item in itensPendentes)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }

        public void MostrarItensConcluidos()
        {
            foreach (Item item in itensConcluidos)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }
        
        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(titulo))
                erros.Add("É necessário inserir um título para as tarefas!");

            if (dataCriacao.Date == new DateTime(1, 1, 1))
                erros.Add("É necessário inserir uma data de criação válida para as tarefas!");

            if (prioridade == 0)
                erros.Add("É necessário inserir uma prioridade válida (Alta, Normal ou Baixa) para as tarefas!");

            return new ResultadoValidacao(erros);
        }
        
        public override string ToString()
        {
            return
            "ID: " + id + Environment.NewLine +
            "Título: " + titulo + Environment.NewLine +
            "Data de criação: " + dataCriacao.ToShortDateString() + Environment.NewLine +
            "Data de conclusão: " + DataConclusao + Environment.NewLine +
            "Prioridade: " + PrioridadeTarefa + Environment.NewLine +
            "QTD de itens: " + QuantidadeDeItensTotais + Environment.NewLine +
            "QTD de itens pendentes: " + QuantidadeDeItensPendentes + Environment.NewLine +
            "QTD de itens concluídos: " + QuantidadeDeItensConcluidos + Environment.NewLine +
            "Percentual de conclusao: " + PercentualConclusao + Environment.NewLine;
        }
        
        public int CompareTo(Tarefa other)
        {
            if (prioridade < other.prioridade)
                return 1;
            else if (prioridade > other.prioridade)
                return -1;
            else
                return 0;
        }
        
        #endregion

        #region Métodos privados

        private void ConcluirTarefa()
        {
            dataConclusao = DateTime.Now;
            statusTarefa = Status.concluido;
        }

        #endregion
    }
}
