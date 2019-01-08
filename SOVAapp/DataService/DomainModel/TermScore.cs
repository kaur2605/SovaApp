using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.DomainModel
{
    public class TermScore
    {

        public int Id { get; set; }

        public string Word { get; set; }
        public decimal Tf { get; set; }
        public double Idf { get; set; }
        public double TfIdf { get; set; }

    }
}
