using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraBet.Data.Models
{
    public class Bet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsLive { get; set; }

        public int MatchId { get; set; }

        public Match Match { get; set; }

        public ICollection<Odd> Odds { get; set; } = new HashSet<Odd>();
    }
}
