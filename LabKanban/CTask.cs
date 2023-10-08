using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    
    public class CTask
    {
        public enum TaskStatus : byte
        {
            NoShared = 1,
            InQueue,
            InProgress,
            Сomplete,
            Agreed,
            Archived
        }

        public int Id { get; set; }

        public virtual Chemist Chemist { get; set; }                    //Присвоена химику
        public string ChemistFullname { get; set; }
        public virtual TaskSequence TaskSequence { get; set; }          //Входит в последовательность
        public virtual TaskList TaskList { get; set; }                  //Входит в список
        public string Theme { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string IntCustomer { get; set; }
        public string ExtCustomer { get; set; }
        public string Justification { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PlanedEndDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? AgreeDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public TaskStatus Status { get; set; }
        public bool IsPlanned { get; set; }
        public int Priority { get; set; }
        //public int? SerialTaskId { get; set; }
        //public int? ParallelTaskId { get; set; }


        public CTask()
        {
            CreateDate = DateTime.Now;
            this.Status = (TaskStatus)1;
        }

        public void ChangeStatus()
        {
            if ((int)Status < 5)
                Status = (TaskStatus)(Status + 1);
        }
        public void BackChangeStatus()
        {
            if ((int)Status > 2)
                Status = (TaskStatus)(Status - 1);
        }


        public override string ToString()
        {
            string plan;
            if (IsPlanned)
                plan = "Плановая";
            else
                plan = "Внеплановая";
            return $"Задача № {Id}:\nХимик: {ChemistFullname}\n{Theme}\n{Description}\nВнутренний заказчик: {IntCustomer}\n" +
                $"Внешний заказчик: {ExtCustomer}\nОбоснование: {Justification}\nСоздано: {CreateDate}\n" +
                $"Начато: {BeginDate}\nЗакончено: {EndDate}\nСдано в архив: {ArchiveDate}\n" +
                $"Статус: {(TaskStatus)Status}\n{plan}\nПриоритет: {Priority}";
        }



    }
}
