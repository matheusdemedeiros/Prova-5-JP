using Prova_5_JP.ConsoleApp.Compartilhado;
using Prova_5_JP.ConsoleApp.Módulo_Contatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova_5_JP.ConsoleApp.Módulo_Compromissos
{
    public class Compromisso : EntidadeBase
    {
        #region Atributos

        private readonly string assunto;
        private readonly string local;
        private readonly DateTime horaInicio;
        private readonly DateTime horaTermino;
        private readonly Contato contato;

        #endregion

        #region Propriedades

        public bool Passou => horaTermino < DateTime.Now ? true : false;

        public string Periodo => "";

        #endregion


        #region Construtor

        public Compromisso(string assunto, string local, DateTime horaInicio, DateTime horaTermino, Contato contato)
        {
            this.assunto = assunto;
            this.local = local;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
            this.contato = contato;
        }

        #endregion


        #region Método públicos

        public override string ToString()
        {
            string retorno =
                "Assunto" + assunto +
                "\nLocal" + local +
                "\nData e o horário de início: " + horaInicio +
                "\nHorário de término: " + horaTermino +
                "\nContato: " + contato.Nome + "\n";
            if (!Passou)
                retorno += "Período: " + Periodo;

            return retorno;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(assunto))
                erros.Add("\nÉ necessário inserir um assunto válido para os compromissos!");

            if (string.IsNullOrEmpty(local))
                erros.Add("\nÉ necessário inserir um local válido para os compromissos!");

            //if (string.IsNullOrEmpty(cargo))
            //    erros.Add("\nÉ necessário inserir um cargo válido para os contatos!");

            return new ResultadoValidacao(erros);
        }

        #endregion
    }
}
