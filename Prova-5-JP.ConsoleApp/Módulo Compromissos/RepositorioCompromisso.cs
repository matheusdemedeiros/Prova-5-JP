using Prova_5_JP.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion
    }
}
