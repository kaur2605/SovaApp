using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.DTO
{
   public class CoOccurrenceDTO
    {
        public string Word { get; set; }
        public string Word2 { get; set; }
        public int Grade { get; set; }

        public CoOccurrenceDTO(string Word, string Word2, int Grade)
        {
            this.Word = Word;
            this.Word2 = Word2;
            this.Grade = Grade;

        }

    }
}
