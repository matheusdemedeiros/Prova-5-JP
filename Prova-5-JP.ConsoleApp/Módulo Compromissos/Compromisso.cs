using Prova_5_JP.ConsoleApp.Compartilhado;
using Prova_5_JP.ConsoleApp.Módulo_Contatos;
using System;
using System.Collections.Generic;

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

        public bool Passou => DataInicio < DateTime.Now ? true : false;

        public DateTime DataInicio => horaInicio;

        public DateTime HoraInicio => horaTermino;

        #endregion

        #region Construtores

        public Compromisso(string assunto, string local, string horaInicio, string horaTermino, Contato contato)
        {
            this.assunto = assunto;
            this.local = local;
            this.horaInicio = DateTime.TryParse(horaInicio, out DateTime inicio) ? inicio : new DateTime(1, 1, 1);
            this.horaTermino = DateTime.TryParse(horaTermino, out DateTime termino) ? termino : new DateTime(1, 1, 1);
            this.contato = contato;
        }

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
                "Assunto: " + assunto +
                "\nLocal: " + local +
                "\nData e o horário de início: " + horaInicio +
                "\nHorário de término: " + horaTermino +
                "\nContato: " + contato.Nome + "\n";
        
            return retorno;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(assunto))
                erros.Add("\nÉ necessário inserir um assunto válido para os compromissos!");

            if (string.IsNullOrEmpty(local))
                erros.Add("\nÉ necessário inserir um local válido para os compromissos!");

            if (horaInicio == new DateTime(1, 1, 1))
                erros.Add("\nÉ necessário inserir uma data e hora de início válida para os compromissos!");
            
            if (horaTermino == new DateTime(1, 1, 1))
                erros.Add("\nÉ necessário inserir um horário de término válido para os compromissos!");
            
            if (contato == null)
                erros.Add("\nÉ necessário inserir um contato válido para os compromissos!");

            return new ResultadoValidacao(erros);
        }

        #endregion
    }
}
