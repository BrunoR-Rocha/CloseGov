using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoDis.ProjectClasses.Occurrences
{
    public abstract class Ocurrence
    {
        //este é o template Method
        public void Occurrence_Template()
        {
            ConfirmOccurrence();
            SaveOccurrenceDb();
            GetOccurrence();
            UpdateOccurrence();
            SendOccurrence();
        }

        //verifica se os dados do formulario estao corretos
        public bool ConfirmOccurrence()
        {
            return false;
        }

        //guarda uma ocorrencia na base de dados
        public abstract void SaveOccurrenceDb();

        //procura a ocorrencia criada
        public virtual void GetOccurrence()
        {
            //default - guardar o pedido na camara
        }

        //atualiza os valores da ocorrencia na base de dados
        public virtual void UpdateOccurrence()
        {
        }

        //elimina uma das ocorrencias criadas
        public virtual void DeleteOccurrence()
        {
            // default - eliminar o pedido da db da camara
        }
        protected abstract void SendOccurrence();
    }
}