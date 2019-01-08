using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.DTO
{
   public class TermAsResultDTO
    {

        public string Word { get; set; }
        public decimal Score { get; set; }
        public TermAsResultDTO( string Word , decimal score)
        {
            
            this.Word = Word;
            this.Score = score;
        }

    }
}
