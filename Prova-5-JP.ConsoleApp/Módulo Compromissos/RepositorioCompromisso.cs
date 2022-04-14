using Prova_5_JP.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace Prova_5_JP.ConsoleApp.Módulo_Compromissos
{
    public class RepositorioCompromisso : RepositorioBase<Compromisso>, IRepositorio<Compromisso>
    {
        #region Métodos públicos

        public List<Compromisso> SelecionarCompromissosPassados()
        {
            return registros.FindAll(x => x.Passou == true);
        }

        public List<Compromisso> SelecionarCompromissosFuturos()
        {
            return registros.FindAll(x => x.Passou == false);
        }

        public List<Compromisso> SelecionarCompromissosDiarios()
        {
            return registros.FindAll(x => x.DataInicio.Date == DateTime.Today.Date);
        }

        public List<Compromisso> SelecionarCompromissosSemanais()
        {
            DiaDaSemana hoje = (DiaDaSemana)DateTime.Now.DayOfWeek;
            return registros.FindAll(x => (DiaDaSemana)x.DataInicio.DayOfWeek >= (DiaDaSemana)hoje);
        }

        #endregion
    }
}
